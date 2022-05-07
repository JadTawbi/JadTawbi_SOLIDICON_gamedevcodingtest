using UnityEngine;

public static class Data
{

    public static void Init()
    {
        SequenceData = default(SequenceData);
        SequenceMetaData = default(ReplaySequenceMetaData);
        HighlightTime = 0;
    }

    public const int TotalPlayers = 22;
    public const int StepsPerSecond = 5;

    public static readonly Vector3 PlayerScale = new Vector3(50, 1, 50);
    public static readonly Vector3 BallScale = new Vector3(50, 10, 50);

    public static SequenceData SequenceData;
    public static ReplaySequenceMetaData SequenceMetaData;
    public static float HighlightTime;
}
