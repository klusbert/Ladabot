#include <windows.h>
#include "Constant.h"

DWORD OrigSendAddress = 0;
DWORD OrigRecvAddress = 0;

namespace Consts {
	DWORD clientVersion = 0;

	/* Displaying Text Stuff */
	DWORD ptrPrintName = 0;
	DWORD ptrPrintFPS = 0;
	DWORD ptrShowFPS = 0;
	DWORD ptrNopFPS = 0;
	DWORD ReciveStream = 0;
	DWORD GetNextPacketFunc =0;
	/* Context Menu Stuff */
	DWORD ptrAddContextMenu = 0;
	DWORD ptrOnClickContextMenu = 0;
	DWORD ptrSetOutfitContextMenu = 0;
	DWORD ptrPartyActionContextMenu = 0;
	DWORD ptrCopyNameContextMenu = 0;
	DWORD ptrTradeWithContextMenu = 0;
	DWORD ptrLookContextMenu = 0;
	DWORD prtOnClickContextMenuVf = 0;

	/* Socket Stuff */
	DWORD ptrRecv = 0;
	DWORD ptrSend = 0;
	DWORD ptrSocket = 0;
	DWORD ptrParseFunction = 0;
	DWORD OUTGOINGDATASTREAM =0x7BDDD8;
	DWORD OUTGOINGDATALEN =0x9DA608;
	DWORD SENDOUTGOINGPACKET =0x51B4F0;
	DWORD CreatePacket = 0x540AA0;
	DWORD TibiaPacketAddByte = 0x540AA0;
	DWORD Peek = 0;
	DWORD WalkFunc = 0;
	DWORD Connection = 0;
	DWORD InSideSendPacket = 0x5417E1;
	DWORD AttackFunction = 0x421050;
	DWORD ItemMoveFunc = 0x41CF20;
	DWORD TalkFunc = 0x41F200;
	DWORD ItemUseFunc = 0x41DF80;
		/* Event Trigger Stuff */
	DWORD ptrEventTrigger = 0;
}
