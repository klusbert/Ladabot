using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KonjoBot.Constants
{
    public enum PacketDestination : byte
    {
        Client,
        Server,
        Pipe,
        None,
    }
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
        test = 27,
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
        Shield = 5,
        Weapon = 6,
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
    public enum IncomingPacketType : byte
    {
        InitGame = 10, // 0x0A
        GMActions = 11, // 0x0B
        LoginError = 20, // 0x14
        LoginAdvice = 21, // 0x15
        LoginWait = 22, // 0x16
        Ping = 29, // 0x1D
        PingBack = 30, // 0x1E
        Challenge = 31, // 0x1F
        Death = 40, // 0x28
        FullMap = 100, // 0x64
        TopRow = 101, // 0x65
        RightRow = 102, // 0x66
        BottomRow = 103, // 0x67
        LeftRow = 104, // 0x68
        UpdateTile = 105, // 0x69
        CreateOnMap = 106, // 0x6A
        ChangeOnMap = 107, // 0x6B
        DeleteOnMap = 108, // 0x6C
        MoveCreature = 109, // 0x6D
        OpenContainer = 110, // 0x6E
        CloseContainer = 111, // 0x6F
        CreateContainer = 112, // 0x70
        ChangeInContainer = 113, // 0x71
        DeleteInContainer = 114, // 0x72
        SetInventory = 120, // 0x78
        DeleteInventory = 121, // 0x79
        OpenNPCTrade = 122, // 0x7A
        PlayerGoods = 123, // 0x7B
        CloseNPCTrade = 124, // 0x7C
        OwnTrade = 125, // 0x7D
        CounterTrade = 126, // 0x7E
        CloseTrade = 127, // 0x7F
        Ambient = 130, // 0x82
        GraphicalEffect = 131, // 0x83
        TextEffect = 132, // 0x84
        MissileEffect = 133, // 0x85
        MarkCreature = 134, // 0x86
        Trappers = 135, // 0x87
        CreatureHealth = 140, // 0x8C
        CreatureLight = 141, // 0x8D
        CreatureOutfit = 142, // 0x8E
        CreatureSpeed = 143, // 0x8F
        CreatureSkull = 144, // 0x90
        CreatureParty = 145, // 0x91
        CreatureUnpass = 146, // 0x92
        EditText = 150, // 0x96
        EditList = 151, // 0x97
        PlayerDataBasic = 159, // 0x9F
        PlayerData = 160, // 0xA0
        PlayerSkills = 161, // 0xA1
        PlayerState = 162, // 0xA2
        ClearTarget = 163, // 0xA3
        SpellDelay = 164, // 0xA4
        SpellGroupDelay = 165, // 0xA5
        MultiUseDelay = 166, // 0xA6
        Talk = 170, // 0xAA
        Channels = 171, // 0xAB
        OpenChannel = 172, // 0xAC
        OpenPrivateChannel = 173, // 0xAD
        RuleViolationChannel = 174, // 0xAE
        RuleViolationRemove = 175, // 0xAF
        RuleViolationCancel = 176, // 0xB0
        RuleViolationLock = 177, // 0xB1
        OpenOwnChannel = 178, // 0xB2
        CloseChannel = 179, // 0xB3
        TextMessage = 180, // 0xB4
        CancelWalk = 181, // 0xB5
        WalkWait = 182, // 0xB6
        FloorChangeUp = 190, // 0xBE
        FloorChangeDown = 191, // 0xBF
        ChooseOutfit = 200, // 0xC8
        VipAdd = 210, // 0xD2
        VipLogin = 211, // 0xD3
        VipLogout = 212, // 0xD4
        TutorialHint = 220, // 0xDC
        AutomapFlag = 221, // 0xDD
        QuestLog = 240, // 0xF0
        QuestLine = 241, // 0xF1
        ChannelEvent = 243, // 0xF3
        ItemInfo = 244, // 0xF4
        PlayerInventory = 245, // 0xF5
        MarketEnter = 246, // 0xF6
        MarketLeave = 247, // 0xF7
        MarketDetail = 248, // 0xF8
        MarketBrowse = 249, // 0xF9
        ShowModalDialog = 250, // 0xFA
    }
    public enum ProjectileType : byte
    {
        Spear = 1,
        Bolt = 2,
        Arrow = 3,
        Fire = 4,
        Energy = 5,
        PoisonArrow = 6,
        BurstArrow = 7,
        ThrowingStar = 8,
        ThrowingKnife = 9,
        SmallStone = 10, // 0x0A
        Skull = 11, // 0x0B
        BigStone = 12, // 0x0C
        SnowBall = 13, // 0x0D
        PowerBolt = 14, // 0x0E
        SmallPoison = 15, // 0x0F
        InfernalBolt = 16, // 0x10
        HuntingSpear = 17, // 0x11
        EnchantedSpear = 18, // 0x12
        AssassinStar = 19, // 0x13
        ViperStar = 20, // 0x14
        RoyalSpear = 21, // 0x15
        SniperArrow = 22, // 0x16
        OnyxArrow = 23, // 0x17
        EarthArrow = 24, // 0x18
        NormalSword = 25, // 0x19
        NormalAxe = 26, // 0x1A
        NormalClub = 27, // 0x1B
        EtherealSpear = 28, // 0x1C
        Ice = 29, // 0x1D
        Earth = 30, // 0x1E
        Holy = 31, // 0x1F
        Death = 32, // 0x20
        FlashArrow = 33, // 0x21
        FlamingArrow = 34, // 0x22
        ShiverArrow = 35, // 0x23
        EnergySmall = 36, // 0x24
        IceSmall = 37, // 0x25
        HolySmall = 38, // 0x26
        EarthSmall = 39, // 0x27
        EarthArrow2 = 40, // 0x28
        Explosion = 41, // 0x29
        Cake = 42, // 0x2A
    }

}
