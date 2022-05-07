using System;
using System.Runtime.InteropServices;
using UnityEngine;

public struct PlayerTransform
{
    public Vector2 Position;
    public Vector2 Direction;
}

public struct BallTransform
{
    public Vector3 Position;
    public Vector3 Direction;
    public Vector3 Rotation;
}

[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct ReplaySequenceMetaData
{
    public int TotalSteps;//0
    public float TotalTime;//4
    public bool LeftTeamScored;//8
    public bool RightTeamScored;//12
    public int ActingPlayerIndex; //16 
    public int ActingTeamIndex;//20
    public Highlight highlighType;//24
    public ushort RemovablePlayersLeftTeamMask; //28
    public ushort RemovablePlayersRightTeamMask; //30
    public ushort RedCardLeftTeamBitMask;//32
    public ushort RedCardRightTeamBitMask;//34
    public ushort YellowCardLeftTeamBitMask;//36
    public ushort YellowCardRightTeamBitMask;//38
                                                //Size 40
}

[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct SequenceStartTime
{
    public long ServerTimeUtcTicks;
    public long GamesStartTimeUtcTicks;
}

[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct GameData
{
    public int LeftTeamScore;//0
    public int RightTeamScore;//4
    public ushort RedCardLeftTeamBitMask;
    public ushort RedCardRightTeamBitMask;
    public ushort YellowCardLeftTeamBitMask;
    public ushort YellowCardRightTeamBitMask;
    public int NumberOfHighlights;

}

public struct PlayerActions
{
    public byte LeftTeamAction;
    public byte RightTeamAction;
    public sbyte LeftTeamActivePlayerIndex;
    public sbyte RightTeamActivePlayerIndex;
}

public struct BallOwner
{
    public sbyte BallOwnedPlayer;
    public sbyte BallOwnedTeam;
}

public struct SequenceData
{
    public PlayerTransform[] PlayerTransforms; //1d representation of a 2d array, all player transforms for both teams[numberOfSteps, numberOfPlayers * numberOfTeams]
    public BallTransform[] BallTransforms;
    public PlayerActions[] PlayerActions;
    public BallOwner[] BallOwners; //Seems to not always be properly set 
    public byte[] LeftTeamRoles;
    public byte[] RightTeamRoles;
}

public enum Action
{
    Idle = 0,
    Left = 1,
    TopLeft = 2,
    Top = 3,
    TopRight = 4,
    Right = 5,
    BottomRight = 6,
    Bottom = 7,
    BottomLeft = 8,

    LongPass = 9,
    HighPass = 10,
    ShortPass = 11,
    Shot = 12,

    Sprint = 13,
    ReleaseDirection = 14,
    ReleaseSprint = 15,
    Sliding = 16,
    Dribble = 17,
    ReleaseDribble = 18,
    None = 19

}

public enum GameMode
{
    Normal = 0,
    KickOff = 1,
    GoalKick = 2,
    FreeKick = 3,
    Corner = 4,
    ThrowIn = 5,
    Penalty = 6,
    Unset = 7
}

public enum Role
{
    GoalKeeper = 0,
    CentreBack = 1,
    LeftBack = 2,
    RightBack = 3,
    DefenceMidfield = 4,
    CentralMidfield = 5,
    LeftMidfield = 6,
    RightMidfield = 7,
    AttackMidfield = 8,
    CentralFront = 9,
}

public struct BinaryReplayMetaData
{
    public int TotalSteps;
    public float TotalTime;
}

public enum Highlight
{
    //Left or Right is the acting team in the highlight and not based on the left or right half of the field
    //TopLeft, TopRight, BottomLeft, BottomRight refers to the actual corners on teh field
    Undefined,
    FreeKickLeft,
    FreeKickRight,
    CornerTopLeft,
    CornerTopRight,
    CornerBottomLeft,
    CornerBottomRight,
    ThrowInLeft,
    ThrowInRight,
    PenaltyLeft,
    PenaltyRight,
    PrePenaltyLeft,
    PrePenaltyRight,
    GoalLeft,
    GoalRight,
    PostHitLeft,
    PostHitRight,
    CrossbarHitLeft,
    CrossbarHitRight,
    YellowCardLeft,
    YellowCardRight,
    RedCardLeft,
    RedCardRight,
    SaveLeft,
    SaveRight,
    KickOff,
    FoulLeft,
    FoulRight,
    OffsideLeft,
    OffsideRight,
    OutsideGoalLeft,
    OutsideGoalRight,
    Length
}