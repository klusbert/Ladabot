#include <windows.h>
#include <string>
#include <sstream>
#include <list>
#include <assert.h>
#include "Core.h"
#include "packet.h"
#include "Constant.h"
#include<iostream>
#include <memory>
#include <thread>
using namespace std;
#define DESIRED_ACCESS PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ
list<NormalText> DisplayTexts;
list<int> HasExtraByteList;

//new
bool stopPIPE = false;
HANDLE pipeServerhThread;
HANDLE pipeServer = 0;
HANDLE pipeClient = 0;
DWORD dwThreadId;
bool fConnected = false;
struct pipePack
{
	BYTE data[1024];
	int size;
};

list<pipePack> pipePackets;
bool HookInjected = false;

struct TPacketStream
{
	LPVOID pBuffer;
	DWORD dwSize;
	DWORD dwPos;
};

struct ToClientPacket
{
	int Size;
	BYTE Data[1024];
};

TPacketStream * pRecvStream = 0;
DWORD OldGetNextPacket = 0;

HINSTANCE hMod = 0;

BOOL fSendingToClient = FALSE;
bool Connected = false;
HANDLE WorkingThread = 0;
DWORD tibiaPid = 0;
HWND hwndTibia = 0;
std::string PipeName;
HANDLE pipe;

BYTE Buffer[1024] = { 0 };
OVERLAPPED overlapped = { 0 };
DWORD errorStatus = ERROR_SUCCESS;
DWORD OldPrintFPS = 0;
DWORD DatPointerAdress = 0;
BYTE* OldNopFPS = 0;
bool UnloadMe = false;
//HANDLE process =0;
DWORD OrigPeekAddress = 0;

CRITICAL_SECTION QueuePacketCriticalSection;
CRITICAL_SECTION NormalTextCriticalSection;
CRITICAL_SECTION PipeWriteCriticalSection;


void EnableHooks()
{

	DWORD dwOldProtect, dwNewProtect, funcAddress;


	OldPrintFPS = HookCall(Consts::ptrPrintFPS, (DWORD)&MyPrintFps);
	OldNopFPS = Nop(Consts::ptrNopFPS, 6);


	OldGetNextPacket = InlineHookCall(Consts::GetNextPacketFunc, (DWORD)&OnGetNextPacketfunc, (LPDWORD)&TfGetNextPacket);

	OrigPeekAddress = (DWORD)GetProcAddress(GetModuleHandleA("User32.dll"), "PeekMessageA");
	oldPeek = (_OldPeekMessageA)OrigPeekAddress;
	funcAddress = (DWORD)&MyPeekMessageA;
	VirtualProtect((LPVOID)Consts::Peek, 4, PAGE_READWRITE, &dwOldProtect);
	memcpy((LPVOID)Consts::Peek, &funcAddress, 4);
	VirtualProtect((LPVOID)Consts::Peek, 4, dwOldProtect, &dwNewProtect);


	DWORD hookAddy = Consts::SENDOUTGOINGPACKET;
	int hookLen = 5;
	jmpBackAddy = hookAddy + hookLen;
	DetourHook((void*)hookAddy, MySendPacketFunc, hookLen);
	HookInjected = true;


}

void DissableHooks()
{
	DWORD dwOldProtect, dwNewProtect, funcAddress;
	UnhookCall(Consts::ptrPrintFPS, OldPrintFPS);

	UnNop(Consts::ptrNopFPS, OldNopFPS, 6);
	UnhookCall(Consts::GetNextPacketFunc, OldGetNextPacket);

	VirtualProtect((LPVOID)Consts::Peek, 4, PAGE_READWRITE, &dwOldProtect);
	memcpy((LPVOID)Consts::Peek, &OrigPeekAddress, 4);
	VirtualProtect((LPVOID)Consts::Peek, 4, dwOldProtect, &dwNewProtect);


	BYTE RestoreBytes[5] = { 0x55, 0x8B, 0xEC, 0x6A, 0xFF };
	VirtualProtect((LPVOID)(Consts::SENDOUTGOINGPACKET), 5, PAGE_READWRITE, &dwOldProtect);
	memcpy((LPVOID)(Consts::SENDOUTGOINGPACKET), &RestoreBytes, 5);
	VirtualProtect((LPVOID)(Consts::SENDOUTGOINGPACKET), 5, dwOldProtect, &dwNewProtect);


}

void __declspec(naked) MySendPacketFunc()
{

	_asm {
		mov edx, [Consts::Connection]
			cmp[edx], 11
			jnz Skip
			CALL Test
		Skip :
		PUSH EBP
			MOV EBP, ESP
			PUSH - 0x1
			jmp[jmpBackAddy]
	}

}
void Test()
{
	int Position = 0;
	//Packet* packet;
	std::unique_ptr<Packet> packet(new Packet);
	DWORD dataLen = (*(DWORD*)Consts::OUTGOINGDATALEN);
	//packet = new Packet;//Packet to bot
	packet->AddByte(2);
	//got full packet
	for (DWORD i = 8; i < dataLen; i++)
	{
		DWORD Adr = Consts::OUTGOINGDATASTREAM + i;
		BYTE bytedat = (*(BYTE*)Adr);
		packet->AddByte(bytedat);

	}
	SendPipePacket(packet->GetPacket(), packet->GetSize());
}


void __declspec(naked) MyPrintFps(int nSurface, int nX, int nY, int nFont, int nRed, int nGreen, int nBlue, int nAlign)
{
	__asm
	{
		pop edx

			mov edx, [Consts::ptrShowFPS]
			cmp[edx], 0
			je skipPrintText
			call PrintText

		skipPrintText :

		pushad
			pushfd

			call MyPrintFpsWork

			popfd
			popad

			mov edx, [Consts::ptrPrintFPS]
			add edx, 5
			jmp edx
	}
}
void MyPrintFpsWork()
{
	char* someText;

	int x, y, font, r, g, b, width, height, guiID, count, itemID, bSize;

	EnterCriticalSection(&NormalTextCriticalSection);
	list<NormalText>::iterator ntIT;
	for (ntIT = DisplayTexts.begin(); ntIT != DisplayTexts.end(); ++ntIT)
	{
		someText = ntIT->text;
		x = ntIT->x;
		y = ntIT->y;
		font = ntIT->font;
		r = ntIT->r;
		g = ntIT->g;
		b = ntIT->b;
		_asm
		{
			push 0
				push someText
				push b
				push g
				push r
				push font
				push y
				mov edx, x
				mov ecx, 1
				call PrintText
				add esp, 0x1c
		}
	}
	LeaveCriticalSection(&NormalTextCriticalSection);
}
bool WINAPI MyPeekMessageA(LPMSG pMsg, HWND hwnd, UINT wMsgFilterMin, UINT wMsgFilterMax, UINT wRemoveMsg)
{

	EnterCriticalSection(&QueuePacketCriticalSection);
	list<pipePack>::iterator it;
	for (it = pipePackets.begin(); it != pipePackets.end(); ++it)
	{

		PipeOnRead(it->data);
	}
	pipePackets.clear();
	LeaveCriticalSection(&QueuePacketCriticalSection);

	return oldPeek(pMsg, hwnd, wMsgFilterMin, wMsgFilterMax, wRemoveMsg);
}

int OnGetNextPacketfunc()
{
	if ((*(DWORD*)Consts::Connection) != 11)
	{
		return TfGetNextPacket();
	}
	
	if (fSendingToClient)
	{
		if (pRecvStream->dwPos < pRecvStream->dwSize)
		{
			BYTE bNextCmd = *((LPBYTE)pRecvStream->pBuffer + pRecvStream->dwPos);;
			pRecvStream->dwPos++;
			return (int)bNextCmd;
		}
		else return -1;
	}



	int iCmd = -1;
	int Position = 0;
	//Packet* packet;


	iCmd = TfGetNextPacket();

	if (pRecvStream->dwPos - 1 == 8)
	{
		std::unique_ptr<Packet> packet(new Packet);
		
		
		Position = pRecvStream->dwPos;
		//packet = new Packet;//Packet to bot
		packet->AddByte(1);
		//got full packet
		//packet->AddWord((WORD)Position);
		packet->AddByte((BYTE)iCmd);
		while (Position < pRecvStream->dwSize)
		{
			packet->AddByte(Packet::ReadByte((BYTE*)pRecvStream->pBuffer, &Position));
		}
		SendPipePacket(packet->GetPacket(), packet->GetSize());
		
	}
	


	if (iCmd == 0x6A)
	{
		std::unique_ptr<Packet> packet(new Packet);
		Position = pRecvStream->dwPos;
	//	packet = new Packet;//Packet to bot
		packet->AddByte(0xF2);
		packet->AddByte(0x6A);//Add packet type (Tile add thing)
		packet->AddWord(Packet::ReadWord((BYTE*)pRecvStream->pBuffer, &Position)); //x
		packet->AddWord(Packet::ReadWord((BYTE*)pRecvStream->pBuffer, &Position)); //y
		packet->AddByte(Packet::ReadByte((BYTE*)pRecvStream->pBuffer, &Position)); //z
		packet->AddByte(Packet::ReadByte((BYTE*)pRecvStream->pBuffer, &Position)); //stack

		int ThingId = Packet::ReadWord((BYTE*)pRecvStream->pBuffer, &Position);
		packet->AddWord(ThingId);
		SendPipePacket(packet->GetPacket(), packet->GetSize());
	}
	if (iCmd == 0xB4)
	{

		Position = pRecvStream->dwPos;

		BYTE Mode = Packet::ReadByte((BYTE*)pRecvStream->pBuffer, &Position);
		if (Mode == 22)
		{
			std::unique_ptr<Packet> packet(new Packet);
		//	packet = new Packet;//Packet to bot
			packet->AddByte(0xF2);
			packet->AddByte(0xB4);//Add packet type (Tile add thing)
			packet->AddByte(Mode);
			packet->AddString(Packet::ReadString((BYTE*)pRecvStream->pBuffer, &Position));
			SendPipePacket(packet->GetPacket(), packet->GetSize());
			pRecvStream->dwPos = Position;
			return -1;
		}

	}
	if (iCmd == 0x6D)
	{
		std::unique_ptr<Packet> packet(new Packet);
		Position = pRecvStream->dwPos;
	//	packet = new Packet;//Packet to bot
		packet->AddByte(0xF2);
		packet->AddByte(0x6D);
		packet->AddWord(Packet::ReadWord((BYTE*)pRecvStream->pBuffer, &Position)); //fromx
		packet->AddWord(Packet::ReadWord((BYTE*)pRecvStream->pBuffer, &Position)); //fromy
		packet->AddByte(Packet::ReadByte((BYTE*)pRecvStream->pBuffer, &Position)); //fromz
		packet->AddByte(Packet::ReadByte((BYTE*)pRecvStream->pBuffer, &Position)); //stack	
		packet->AddWord(Packet::ReadWord((BYTE*)pRecvStream->pBuffer, &Position)); //Tox
		packet->AddWord(Packet::ReadWord((BYTE*)pRecvStream->pBuffer, &Position)); //toy
		packet->AddByte(Packet::ReadByte((BYTE*)pRecvStream->pBuffer, &Position)); //toz
		SendPipePacket(packet->GetPacket(), packet->GetSize());
	}

	if (iCmd == 0x84)
	{
		std::unique_ptr<Packet> packet(new Packet);
		Position = pRecvStream->dwPos;
	//	packet = new Packet;//Packet to bot
		packet->AddByte(0xF2);
		packet->AddByte(0x84);
		packet->AddWord(Packet::ReadWord((BYTE*)pRecvStream->pBuffer, &Position)); //fromx
		packet->AddWord(Packet::ReadWord((BYTE*)pRecvStream->pBuffer, &Position)); //fromy
		packet->AddByte(Packet::ReadByte((BYTE*)pRecvStream->pBuffer, &Position)); //fromz
		packet->AddByte(Packet::ReadByte((BYTE*)pRecvStream->pBuffer, &Position)); //Color
		packet->AddString(Packet::ReadString((BYTE*)pRecvStream->pBuffer, &Position));//message
		SendPipePacket(packet->GetPacket(), packet->GetSize());
	
	}
	if (iCmd == 0x8D) 
	{
		std::unique_ptr<Packet> packet(new Packet);
		Position = pRecvStream->dwPos;
		//	packet = new Packet;//Packet to bot
		packet->AddByte(0xF2);
		packet->AddByte(0x8D);

		packet->AddDWord(Packet::ReadDWord((BYTE*)pRecvStream->pBuffer, &Position)); //fromy
		packet->AddByte(Packet::ReadByte((BYTE*)pRecvStream->pBuffer, &Position)); //fromz
		packet->AddByte(Packet::ReadByte((BYTE*)pRecvStream->pBuffer, &Position)); //Color
		SendPipePacket(packet->GetPacket(), packet->GetSize());
	}
	if (iCmd == 0xA0) {
		std::unique_ptr<Packet> packet(new Packet);
		Position = pRecvStream->dwPos;
		packet->AddByte(0xF2);
		packet->AddByte(0xA0);
	
		packet->AddWord(Packet::ReadWord((BYTE*)pRecvStream->pBuffer, &Position)); //health

		packet->AddWord(Packet::ReadWord((BYTE*)pRecvStream->pBuffer, &Position)); //healthmax
		packet->AddDWord(Packet::ReadDWord((BYTE*)pRecvStream->pBuffer, &Position));//cap
		packet->AddDWord(Packet::ReadDWord((BYTE*)pRecvStream->pBuffer, &Position));//captot
		packet->AddUint64(Packet::ReadUint64((BYTE*)pRecvStream->pBuffer, &Position));//EXP

		packet->AddWord(Packet::ReadWord((BYTE*)pRecvStream->pBuffer, &Position)); //level	
		packet->AddByte(Packet::ReadByte((BYTE*)pRecvStream->pBuffer, &Position));//levelpercent

		Position += 10;
		packet->AddWord(Packet::ReadWord((BYTE*)pRecvStream->pBuffer, &Position)); //mana	
		packet->AddWord(Packet::ReadWord((BYTE*)pRecvStream->pBuffer, &Position)); //manamax

		packet->AddByte(Packet::ReadByte((BYTE*)pRecvStream->pBuffer, &Position)); //maglevel
		packet->AddByte(Packet::ReadByte((BYTE*)pRecvStream->pBuffer, &Position)); //basemaglev
		packet->AddByte(Packet::ReadByte((BYTE*)pRecvStream->pBuffer, &Position)); //magiclevelperc

		packet->AddByte(Packet::ReadByte((BYTE*)pRecvStream->pBuffer, &Position)); //soul

		packet->AddWord(Packet::ReadWord((BYTE*)pRecvStream->pBuffer, &Position)); //stamina



		packet->AddWord(Packet::ReadWord((BYTE*)pRecvStream->pBuffer, &Position)); //baseSpeed
		packet->AddWord(Packet::ReadWord((BYTE*)pRecvStream->pBuffer, &Position)); //Regeneration
		packet->AddWord(Packet::ReadWord((BYTE*)pRecvStream->pBuffer, &Position)); //OfflineTrainingTime
		SendPipePacket(packet->GetPacket(), packet->GetSize());
		
	
		
	}
	



	return iCmd;//We are done parsing what we want, return what remains to the client
}
void SendPipePacket(BYTE* data, int size)
{
	std::thread t(threadCallback, data, size);
	t.join();	
	
	
}
void threadCallback(BYTE* data, int size)
{
	WriteFileEx(pipeClient, data, size, &overlapped, NULL);

}

void SendToClient(LPBYTE pBuffer, DWORD dwSize)
{
	fSendingToClient = TRUE;

	TPacketStream StreamHolder = *pRecvStream;

	pRecvStream->pBuffer = pBuffer;
	pRecvStream->dwSize = dwSize;
	pRecvStream->dwPos = 0;

	TfParser();
	*pRecvStream = StreamHolder;

	fSendingToClient = FALSE;
}
BYTE* Nop(DWORD dwAddress, int size)
{
	DWORD dwOldProtect, dwNewProtect;
	BYTE* OldBytes;
	VirtualProtect((LPVOID)(dwAddress), size, PAGE_READWRITE, &dwOldProtect);
	OldBytes = new BYTE[size];
	memcpy(OldBytes, (LPVOID)(dwAddress), size);
	memset((LPVOID)(dwAddress), 0x90, size);
	VirtualProtect((LPVOID)(dwAddress), size, dwOldProtect, &dwNewProtect);

	return OldBytes;
}

void UnNop(DWORD dwAddress, BYTE* OldBytes, int size)
{
	DWORD dwOldProtect, dwNewProtect;
	VirtualProtect((LPVOID)(dwAddress), size, PAGE_READWRITE, &dwOldProtect);
	memcpy((LPVOID)(dwAddress), OldBytes, size);
	VirtualProtect((LPVOID)(dwAddress), size, dwOldProtect, &dwNewProtect);

	delete[] OldBytes;
	OldBytes = 0;
}
DWORD HookCall(DWORD dwAddress, DWORD dwFunction)
{

	DWORD dwOldProtect, dwNewProtect, dwOldCall, dwNewCall;
	//CALL opcode = 0xE8 <4 byte for distance>
	BYTE callByte[5] = { 0xE8, 0x00, 0x00, 0x00, 0x00 };

	//Calculate the distance
	dwNewCall = dwFunction - dwAddress - 5;
	memcpy(&callByte[1], &dwNewCall, 4);

	VirtualProtect((LPVOID)(dwAddress), 5, PAGE_READWRITE, &dwOldProtect); //Gain access to read/write
	memcpy(&dwOldCall, (LPVOID)(dwAddress + 1), 4); //Get the old function address for unhooking
	memcpy((LPVOID)(dwAddress), &callByte, 5); //Hook the function
	VirtualProtect((LPVOID)(dwAddress), 5, dwOldProtect, &dwNewProtect); //Restore access

	return dwOldCall;
}
bool DetourHook(void * toHook, void* ourFunct, int len)
{
	if (len < 5){
		return false;
	}
	DWORD curProtection;
	VirtualProtect(toHook, len, PAGE_EXECUTE_READWRITE, &curProtection);
	memset(toHook, 0x90, len);
	DWORD relativeAddress = ((DWORD)ourFunct - (DWORD)toHook) - 5;
	*(BYTE*)toHook = 0xE9;
	*(DWORD*)((DWORD)toHook + 1) = relativeAddress;
	DWORD temp;
	VirtualProtect(toHook, len, curProtection, &temp);
	return true;

}
DWORD InlineHookCall(DWORD dwCallAddress, DWORD dwNewAddress, LPDWORD pOldAddress)
{
	DWORD dwOldProtect, dwNewProtect, dwOldCall, dwNewCall;
	BYTE call[5] = { 0xE8, 0x00, 0x00, 0x00, 0x00 };

	dwNewCall = dwNewAddress - dwCallAddress - 5;
	memcpy(&call[1], &dwNewCall, 4);

	VirtualProtectEx(GetCurrentProcess(), (LPVOID)(dwCallAddress), 5, PAGE_READWRITE, &dwOldProtect);
	if (pOldAddress)
	{
		memcpy(&dwOldCall, (LPVOID)(dwCallAddress + 1), 4);
		*pOldAddress = dwCallAddress + dwOldCall + 5;
	}
	memcpy((LPVOID)(dwCallAddress), &call, 5);
	VirtualProtectEx(GetCurrentProcess(), (LPVOID)(dwCallAddress), 5, dwOldProtect, &dwNewProtect);

	return dwOldCall;
}

void UnhookCall(DWORD dwAddress, DWORD dwOldCall)
{
	DWORD dwOldProtect, dwNewProtect;
	BYTE callByte[5] = { 0xE8, 0x00, 0x00, 0x00, 0x00 };

	memcpy(&callByte[1], &dwOldCall, 4);

	VirtualProtect((LPVOID)(dwAddress), 5, PAGE_READWRITE, &dwOldProtect);
	memcpy((LPVOID)(dwAddress), &callByte, 5);
	VirtualProtect((LPVOID)(dwAddress), 5, dwOldProtect, &dwNewProtect);
}
void __declspec(noreturn) UninjectSelf()
{
	DWORD ExitCode = 0;
	__asm
	{
		push hMod;
		push ExitCode;
		jmp dword ptr[FreeLibraryAndExitThread];
	}
}

BOOL CALLBACK EnumWindowsProc(HWND hwnds, LPARAM lParam)
{
	DWORD PID;
	DWORD threadID;
	threadID = GetWindowThreadProcessId(hwnds, &PID);
	if (PID == tibiaPid)
	{
		hwndTibia = hwnds;
		//::MessageBoxA(0,"found","",0);
	}
	return hwndTibia ? 0 : 1;
}

void UninjectMe(LPCSTR str)
{


	DissableHooks();
	TerminateThread(WorkingThread, 0);
	TerminateThread(pipeServerhThread, 0);
	CloseHandle(pipeServer);
	CloseHandle(pipeClient);
	DeleteCriticalSection(&NormalTextCriticalSection);
	DeleteCriticalSection(&QueuePacketCriticalSection);
	DeleteCriticalSection(&PipeWriteCriticalSection);

	CreateThread(NULL, NULL, (LPTHREAD_START_ROUTINE)UninjectSelf, hMod, NULL, NULL);
}



void ParseDisplayText(BYTE *Buffer, int position)
{
	string TextName = Packet::ReadString(Buffer, &position);
	int PosX = Packet::ReadWord(Buffer, &position);
	int PosY = Packet::ReadWord(Buffer, &position);
	int ColorRed = Packet::ReadWord(Buffer, &position);
	int ColorGreen = Packet::ReadWord(Buffer, &position);
	int ColorBlue = Packet::ReadWord(Buffer, &position);
	int Font = Packet::ReadWord(Buffer, &position);
	string Text = Packet::ReadString(Buffer, &position);

	list<NormalText>::iterator ntIT;
	EnterCriticalSection(&NormalTextCriticalSection);

	for (ntIT = DisplayTexts.begin(); ntIT != DisplayTexts.end();)
	{
		if (ntIT->TextName == TextName)
		{
			delete[] ntIT->TextName;
			delete[] ntIT->text;
			ntIT = DisplayTexts.erase(ntIT);
		}
		else
			++ntIT;
	}

	LeaveCriticalSection(&NormalTextCriticalSection);

	NormalText NewText;
	NewText.b = ColorBlue;
	NewText.g = ColorGreen;
	NewText.r = ColorRed;
	NewText.x = PosX;
	NewText.y = PosY;
	NewText.font = Font;

	NewText.TextName = new char[TextName.size() + 1];
	NewText.text = new char[Text.size() + 1];

	memcpy(NewText.TextName, TextName.c_str(), TextName.size() + 1);
	memcpy(NewText.text, Text.c_str(), Text.size() + 1);

	EnterCriticalSection(&NormalTextCriticalSection);

	DisplayTexts.push_back(NewText);

	LeaveCriticalSection(&NormalTextCriticalSection);
}
void ParseRemoveText(BYTE *Buffer, int position)
{
	string RemovalTextName = Packet::ReadString(Buffer, &position);
	list<NormalText>::iterator ntIT;
	EnterCriticalSection(&NormalTextCriticalSection);

	for (ntIT = DisplayTexts.begin(); ntIT != DisplayTexts.end();)
	{
		if (ntIT->TextName == RemovalTextName)
		{
			delete[] ntIT->TextName;
			delete[] ntIT->text;
			ntIT = DisplayTexts.erase(ntIT);
		}
		else
			++ntIT;
	}

	LeaveCriticalSection(&NormalTextCriticalSection);

}

void ParseHookSendToClient(BYTE *Buffer, int position)
{
	bool Sending = (bool)Packet::ReadByte(Buffer, &position);

	fSendingToClient = Sending;
}

void ParseSetAddress(BYTE *Buffer, int position)
{

	BYTE type = Packet::ReadByte(Buffer, &position);

	DWORD Adr = Packet::ReadDWord(Buffer, &position);
	switch (type)
	{
	case 0:
		PrintText = (_PrintText*)Adr;
		break;
	case 1:
		Consts::ptrRecv = Adr;
		break;
	case 2:
		Consts::ptrSend = Adr;
		break;
	case 3:
		Consts::ptrPrintName = Adr;
		break;
	case 4:
		Consts::ptrPrintFPS = Adr;
		break;
	case 5:
		Consts::ptrShowFPS = Adr;
		break;
	case 6:
		Consts::ptrNopFPS = Adr;
		break;
	case 7:
		Consts::ReciveStream = Adr;
		pRecvStream = (TPacketStream*)Consts::ReciveStream;
		break;
	case 8:
		Consts::GetNextPacketFunc = Adr;
		break;
	case 9:
		Consts::ptrParseFunction = Adr;
		TfParser = (TF_PARSER*)Consts::ptrParseFunction;
		break;
	case 12:
		Consts::SENDOUTGOINGPACKET = Adr;
		break;
	case 11:
		Consts::OUTGOINGDATALEN = Adr;
		break;
	case 13:
		Consts::OUTGOINGDATASTREAM = Adr;
		break;
	case 14:
		Consts::CreatePacket = Adr;
		break;
	case 15:
		Consts::TibiaPacketAddByte = Adr;
		break;
	case 16:
		Consts::Peek = Adr;
		break;
	case 17:
		Consts::WalkFunc = Adr;
		break;
	case 18:
		Consts::Connection = Adr;
		break;


	}

}
inline void PipeOnRead(BYTE* Buffer)
{
	int position = 0;

	//	WORD len = Packet::ReadWord(Buffer, &position);
	BYTE PacketID = Packet::ReadByte(Buffer, &position);

	switch (PacketID)
	{
	case 0:
		ParseSetAddress(Buffer, position);
		break;
	case 3:
		EnableHooks();
		break;
	case 4:
		DissableHooks();
		break;
	case 5:
		//unload
		break;
	case 6:
		ParseDisplayText(Buffer, position);
		break;
	case 7:
		ParseRemoveText(Buffer, position);
		break;
	case 8:
		ParseHookSendToClient(Buffer, position);
		ToClientPacket NewPacket;
		NewPacket.Size = Packet::ReadWord(Buffer, &position);

		for (int i = 0; i<NewPacket.Size; i++)
			NewPacket.Data[i] = Packet::ReadByte(Buffer, &position);

		SendToClient(NewPacket.Data, NewPacket.Size);
		break;
	case 9: // s�nd packet to server				
		SendPacketToServer(Buffer, position);
		break;
	case 10:
		int Diagonal = Packet::ReadByte(Buffer, &position);
		int y = Packet::ReadShort(Buffer, &position);
		int x = Packet::ReadShort(Buffer, &position);
		_asm
		{
			push Diagonal
				push y
				push x
				call Consts::WalkFunc

		}
		break;


	}
}




void SendPacketToServer(BYTE *Buffer, int position)
{
	int SendBufferLen = Packet::ReadWord(Buffer, &position);
	int PacketType = Packet::ReadByte(Buffer, &position);

	_asm
	{
		mov ecx, PacketType
			call Consts::CreatePacket

	}
	for (int i = 0; i <SendBufferLen - 1; i++)//skip packetType
	{
		int val = Packet::ReadByte(Buffer, &position);
		_asm
		{
			mov ecx, val
				call Consts::TibiaPacketAddByte

		}
	}

	_asm
	{
		mov cl, 1
			call Consts::SENDOUTGOINGPACKET


	}


}
int Initialize()
{
	while (!stopPIPE)
	{
		pipeServer = CreateNamedPipeW(
			pipeServerName,					// pipe name 
			PIPE_ACCESS_DUPLEX,       // read/write access 
			PIPE_TYPE_MESSAGE |       // message type pipe 
			PIPE_READMODE_MESSAGE |   // message-read mode 
			PIPE_WAIT,                // blocking mode 
			PIPE_UNLIMITED_INSTANCES, // max. instances  
			1024,                  // output buffer size 
			1024,                  // input buffer size 
			0,                        // client time-out 
			NULL);                    // default security attribute 

		if (pipeServer == INVALID_HANDLE_VALUE)
		{
			const DWORD last_error = GetLastError();

			if (ERROR_NO_DATA == last_error)
			{
				MessageBoxW(NULL, L"An error has occurred attempting to create PIPE server. \nError code: ERROR_NO_DATA", L"exTibia", MB_OK);
			}
			else if (ERROR_PIPE_CONNECTED == last_error)
			{
				MessageBoxW(NULL, L"An error has occurred attempting to create PIPE server. \nError code: ERROR_PIPE_CONNECTED.", L"exTibia", MB_OK);
			}
			else if (ERROR_PIPE_LISTENING != last_error)
			{
				MessageBoxW(NULL, L"An error has occurred attempting to create PIPE server. \nError code: ERROR_PIPE_LISTENING", L"exTibia", MB_OK);
			}

			return -1;
		}

		fConnected = ConnectNamedPipe(pipeServer, NULL) ? TRUE : (GetLastError() == ERROR_PIPE_CONNECTED);

		if (fConnected)
		{
			pipeServerhThread = CreateThread(
				NULL,              // no security attribute 
				0,                 // default stack size 
				InstanceThread,    // thread proc
				(LPVOID)pipeServer,    // thread parameter 
				0,                 // not suspended 
				&dwThreadId);      // returns thread ID 

			if (pipeServerhThread == NULL)
			{
				MessageBoxW(NULL, L"An error has occurred attempting to create PIPE server. \nError code: hThread is NULL", L"exTibia", MB_OK);
				return -1;
			}
			else CloseHandle(pipeServerhThread);
		}
		else
			CloseHandle(pipeServer);
	}
	return 0;
}
DWORD WINAPI InstanceThread(LPVOID lpvParam)
{
	DWORD cbBytesRead = 0, cbReplyBytes = 0, cbWritten = 0;
	BOOL fSuccess = FALSE;
	HANDLE hPipe = NULL;

	if (lpvParam == NULL)
	{
		return (DWORD)-1;
	}

	hPipe = (HANDLE)lpvParam;

	while (hPipe != INVALID_HANDLE_VALUE)
	{
		fSuccess = ReadFile(
			hPipe,        // handle to pipe 
			Buffer,    // buffer to receive data 
			sizeof(Buffer), // size of buffer 
			&cbBytesRead, // number of bytes read 
			NULL);        // not overlapped I/O 

		if (!fSuccess || cbBytesRead == 0) //
		{
			if (GetLastError() == ERROR_BROKEN_PIPE)
			{
				FlushFileBuffers(hPipe);
				DisconnectNamedPipe(hPipe);
				CloseHandle(hPipe);
				UninjectMe("1");

				return 1;
			}
			else
			{
				FlushFileBuffers(hPipe);
				DisconnectNamedPipe(hPipe);
				CloseHandle(hPipe);
				UninjectMe("2");

				return 1;
			}
		}


		if (!HookInjected)
			PipeOnRead(Buffer);
		else
		{
		    EnterCriticalSection(&QueuePacketCriticalSection);
			pipePack p;
			for (int i = 0; i < sizeof(Buffer); i++)
				p.data[i] = Buffer[i];
			p.size = sizeof(Buffer);
			pipePackets.push_back(p);
			LeaveCriticalSection(&QueuePacketCriticalSection);
		}

	}

	FlushFileBuffers(hPipe);
	DisconnectNamedPipe(hPipe);
	CloseHandle(hPipe);

	return 1;
}

extern "C" bool APIENTRY DllMain(HMODULE hModule, DWORD reason, LPVOID reserved)
{
	switch (reason)
	{
	case DLL_PROCESS_ATTACH:
	{

							   //

							   hMod = hModule;
							   tibiaPid = GetCurrentProcessId();
							   EnumWindows(EnumWindowsProc, 0);

							   int result1 = swprintf_s(pipeServerName, sizeof(pipeServerName) / sizeof(wchar_t), L"\\\\.\\pipe\\InjectServer%d", tibiaPid);
							   int result2 = swprintf_s(pipeClientName, sizeof(pipeClientName) / sizeof(wchar_t), L"\\\\.\\pipe\\InjectClient%d", tibiaPid);

							   //   process = OpenProcess(MAXIMUM_ALLOWED, false, tibiaPid);

							   InitializeCriticalSection(&NormalTextCriticalSection);
							   InitializeCriticalSection(&QueuePacketCriticalSection);
							   InitializeCriticalSection(&PipeWriteCriticalSection);
							   WorkingThread = CreateThread(NULL, NULL, (LPTHREAD_START_ROUTINE)Initialize, hMod, NULL, NULL);

							   if (WaitNamedPipeW(pipeClientName, NMPWAIT_WAIT_FOREVER))
							   {
								   pipeClient = CreateFileW(pipeClientName, GENERIC_READ | GENERIC_WRITE, 0, NULL, OPEN_EXISTING, NULL, NULL);

								   if (pipeClient == INVALID_HANDLE_VALUE)
								   {
									   DWORD MsgID = GetLastError();

									   char *TextSize;
									   FormatMessage(FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS, 0, MsgID, MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT), (LPTSTR)&TextSize, 0, 0);

									   MessageBoxA(0, "ERROR_CREATE_PIPE", "hook.dll", 0);
									   if (GetLastError() == ERROR_PIPE_BUSY)
									   {
										   MessageBoxA(0, "ERROR_PIPE_BUSY", "hook.dll", 0);
									   }
								   }
							   }

							   break;
	}
	case DLL_PROCESS_DETACH:
	{

							   TerminateThread(WorkingThread, 0);
							   TerminateThread(pipeServerhThread, 0);

							   DeleteCriticalSection(&NormalTextCriticalSection);
							   DeleteCriticalSection(&QueuePacketCriticalSection);
							   DeleteCriticalSection(&PipeWriteCriticalSection);
							   break;
	}
	}
	return true;
}
