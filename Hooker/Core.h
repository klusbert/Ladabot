#include <windows.h>
#include <string>
#include <sstream>
#include <list>
#include <assert.h>
#include "packet.h"
inline void PipeOnRead(BYTE* Buffer);

void PipeWriteProc(LPVOID lpParameter);
inline void PipeWrite(Packet* p);
extern DWORD OldGetNextPacket;

typedef void _PrintText(int nSurface, int nX, int nY, int nFont, int nRed, int nGreen, int nBlue, int nAlign);
typedef int TF_GETNEXTPACKET();
typedef void TF_PARSER();
TF_PARSER *TfParser = 0;
TF_GETNEXTPACKET *TfGetNextPacket = NULL;
extern HANDLE process;

void SendOutgoingPacket();
static _PrintText *PrintText = 0;

DWORD InlineHookCall(DWORD dwCallAddress, DWORD dwNewAddress, LPDWORD pOldAddress);
DWORD HookCall(DWORD dwAddress, DWORD dwFunction);
void UnhookCall(DWORD dwAddress, DWORD dwOldCall);
BYTE* Nop(DWORD dwAddress, int size);
void UnNop(DWORD dwAddress, BYTE* OldBytes, int size);
void MyPrintFps(int nSurface, int nX, int nY, int nFont, int nRed, int nGreen, int nBlue, int nAlign);
void MyPrintFpsWork();
void MySendPacketFunc();
void InjectWinsockHook();
void UnInjectWinsockHook();
void SendPacket();
int OnGetNextPacketfunc();

void SendPacketToServer(BYTE *Buffer, int position);
bool WINAPI MyPeekMessageA(LPMSG pMsg, HWND hwnd, UINT wMsgFilterMin, UINT wMsgFilterMax, UINT wRemoveMsg);
typedef bool (WINAPI *_OldPeekMessageA)(LPMSG pMsg, HWND hwnd, UINT wMsgFilterMin, UINT wMsgFilterMax, UINT wRemoveMsg);
_OldPeekMessageA oldPeek;
wchar_t pipeServerName[32];
wchar_t pipeClientName[32];
DWORD WINAPI InstanceThread(LPVOID lpvParam);
DWORD jmpBackAddy =0;
bool DetourHook(void * toHook, void* ourFunct, int len);
void Test();

//SendFunction









