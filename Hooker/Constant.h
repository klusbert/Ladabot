#include <windows.h>
namespace Consts {

	extern DWORD clientVersion;

	/* Displaying Text Stuff */
	extern DWORD ptrPrintName;
	extern DWORD ptrPrintFPS;
	extern DWORD ptrShowFPS;
	extern DWORD ptrNopFPS;
	extern DWORD ReciveStream;
	extern DWORD GetNextPacketFunc;
	extern DWORD prtOnClickContextMenuVf;
	extern DWORD ptrAddContextMenu;
	extern DWORD ptrOnClickContextMenu;
	extern DWORD ptrSetOutfitContextMenu;
	extern DWORD ptrPartyActionContextMenu;
	extern DWORD ptrCopyNameContextMenu;
	extern DWORD ptrTradeWithContextMenu;
	extern DWORD ptrLookContextMenu;

	extern DWORD ptrRecv;
	extern DWORD ptrSend;
	extern DWORD ptrSocket;
	extern DWORD ptrParseFunction;
	extern DWORD ptrEventTrigger;
	extern DWORD OUTGOINGDATASTREAM;
	extern DWORD OUTGOINGDATALEN;
	extern DWORD SENDOUTGOINGPACKET;
	extern DWORD CreatePacket;
	extern DWORD TibiaPacketAddByte;
	extern DWORD Peek;
	extern DWORD WalkFunc;
	extern DWORD Connection;
	extern DWORD InSideSendPacket;

}

struct NormalText
{
	char* text;
	int r, g, b;
	int x, y;
	int font;
	char *TextName;
};
extern DWORD OrigRecvAddress;
extern DWORD OrigSendAddress;
extern DWORD OldPrintName;


