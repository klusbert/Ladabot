using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KonjoBot.Constants
{
    public enum Direction
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3,
        UpRight = 5,
        DownRight = 6,
        DownLeft = 7,
        UpLeft = 8
    }
    public enum LootDestination : byte
    {
        Container = 1,
        Arrow = 2,
        LeftHand = 3,
        RightHand = 4,
        Ground = 5
    }
    public enum CreatureType : byte
    {
        const_2 = 2,
        Player = 0,
        Summon = 3,
        Target = 1
    }
    public enum PipePacketType : byte
    {
        SetAddress = 0,
        RecivedIncommingPacket = 1,
        RecivedOutgoingPacket = 2,
        EnableHook = 3,
        DisableHook = 4,
        Unload = 5,
        DisplayTextPipe = 6,
        RemoveDisplayText = 7,
        ParsedPacket = 0xF2,
        FullIncommingPacket = 0xF1,
        AutoWalk = 0xF3,
        SendPacketToClient = 8,
        SendPacketToServer = 9,
        Walk = 10,
        ConnectionStatusChanged = 11,
    }
    public enum SpellCategory
    {
        Attack,
        Healing,
        Summon,
        Supply,
        Support
    }

    public enum SpellType
    {
        Instant,
        ItemTransformation,
        Creation
    }

    public enum OutgoingPacketType : byte
    {
        LoginServerRequest = 1,
        EnterGame = 10,
        QuitGame = 20,
        Ping = 29,
        PingBack = 30,

        AutoWalk = 100,
        WalkNorth = 101,
        WalkEast = 102,
        WalkSouth = 103,
        WalkWest = 104,
        Stop = 105,
        WalkNorthEast = 106,
        WalkSouthEast = 107,
        WalkSouthWest = 108,
        WalkNorthWest = 109,
        TurnNorth = 111,
        TurnEast = 112,
        TurnSouth = 113,
        TurnWest = 114,
        EquipItem = 119,
        Move = 120,
        InspectNPCTrade = 121,
        BuyItem = 122,
        SellItem = 123,
        CloseNPCTrade = 124,
        RequestTrade = 125,
        InspectTrade = 126,
        AcceptTrade = 127,
        RejectTrade = 128,
        UseItem = 130,
        UseItemWith = 131,
        UseOnCreature = 132,
        RotateItem = 133,
        CloseContainer = 135,
        UpContainer = 136,
        EditText = 137,
        EditList = 138,
        Look = 140,
        LookCreature = 141,
        Talk = 150,
        RequestChannels = 151,
        JoinChannel = 152,
        LeaveChannel = 153,
        OpenPrivateChannel = 154,
        CloseNPCChannel = 158,
        ChangeFightModes = 160,
        Attack = 161,
        Follow = 162,
        InviteToParty = 163,
        JoinParty = 164,
        RevokeInvitation = 165,
        PassLeadership = 166,
        LeaveParty = 167,
        ShareExperience = 168,
        DisbandParty = 169, //deprecated?
        OpenOwnChannel = 170,
        InviteToOwnChannel = 171,
        ExcludeFromOwnChannel = 172,
        CancelAttackAndFollow = 190,
        TileUpdate = 201, //missing in otclient, deprecated?
        RefreshContainer = 202,
        RequestOutfit = 210,
        ChangeOutfit = 211,
        Mount = 212,
        AddVip = 220,
        RemoveVip = 221,
        EditVip = 222,
        BugReport = 230,
        RuleViolation = 231,
        DebugReport = 232,
        RequestQuestLog = 240,
        RequestQuestLine = 241,
        NewRuleViolation = 242,
        RequestItemInfo = 243,
        MarketLeave = 244,
        MarketBrowse = 245,
        MarketCreate = 246,
        MarketCancel = 247,
        MarketAccept = 248,
        AnswerModalDialog = 249
    }
    public enum PipeType : byte
    {
        HookReceivedPacket = 1,
        SetAddres = 2,
        EnableHooks = 3,
        SendPacketToClient = 4,

    }
    public enum SetAddressType : byte
    {
        RecPointer = 1,
        SendPointer = 2,
        Parsefunction = 3,
        GetNextPacket = 4,
        ReciveStream = 5,
        PrintFps = 6,
        ShowFps = 7,
        PrintName = 8,
        DatPointer = 9,
    }
    public enum SetAddressType1 : byte
    {
        SetTextfunc = 0,
        SetRecvPointer = 1,
        SetSendPointer = 2,
        SetPrintName = 3,
        SetPrintFps = 4,
        SetShowFps = 5,
        SetNopFps = 6,
        SetReciveStream = 7,
        SetGetNextPacket = 8,
        SetParserFunction = 9,
        SetDatPointr = 10,
        SetOUTGOINGDATALEN = 11,
        SetSENDOUTGOINGPACKET = 12,
        SetOutgoingDATASTREAM = 13,
        SetCreatePacket = 14,
        SetAddByteFunc = 15,
        PeekMessage = 16,
        WalkFunction = 17,
        Connection = 18,
        AttackFunfion = 19,
        ItemMove = 20,
        SpeakFunction = 21,
        ItemUseFunction = 22
        
    }

    public enum SlotNumber
    {
        None = 0,
        Head = 1,
        Necklace = 2,
        Backpack = 3,
        Armor = 4,
        Right = 5,
        Left = 6,
        Legs = 7,
        Feet = 8,
        Ring = 9,
        Ammo = 10,
       
    }
    public enum WalkDirection :byte 
    {
        WalkNorth = 101,
        WalkEast = 102,
        WalkSouth = 103,
        WalkWest = 104,
        Stop = 105,
        WalkNorthEast = 106,
        WalkSouthEast = 107,
        WalkSouthWest = 108,
        WalkNorthWest = 109,
    }
    public enum LoginStatus : byte
    {
        LoggedOut = 0,
        NotLoggedIn = LoggedOut,
        LoggingIn = 9,
        LoggedIn = 11,
    }
    public enum ItemLocationType
    {
        Container =0,
        Slot = 1,
        Ground = 2,
    }
    public enum ChatChannel
    {
        Chat =0,
        Npc = 1,
        Private =2,
    }
    public enum WaypointType : byte
    {
        Walk = 1,
        UpUse = 2,
        DownUse = 3,
        Rope = 4,
        Shovel = 5,
        Corpse = 6,
        Machete = 7,
        Door = 8,
        DoorWithKey = 9
    }
    public enum FollowType : byte
    {
        Reach = 1,
        Distance = 2,
        Stand = 3
    }
    public enum HotkeyObjectUseType
    {
        WithCrosshairs = 0,
        UseOnTarget = 1,
        UseOnSelf = 2
    }
    public enum BotActionType
    {
        Loot =0,
        EatFood = 1,
        Walk = 2,
        OpenDeadBody = 3,

    }
   
 
}
