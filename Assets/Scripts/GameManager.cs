using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private GameObject ballPrefab;
    [SerializeField]
    private GameObject targetPrefab;

    private GameObject ball;
    private GameObject[] players;
    private GameObject target;

    void Start()
    {
        Data.Init();
        var bytesMetaData = Resources.Load<TextAsset>("4239236_highlight_CornerTopRight_4_meta").bytes;
        var metaData = ByteConverter.GetStruct<ReplaySequenceMetaData>(bytesMetaData);
        var bytes = Resources.Load<TextAsset>("4239236_highlight_CornerTopRight_4").bytes;
        Data.SequenceData = ByteConverter.GetSequenceData(bytes, metaData.TotalSteps);
        Data.SequenceMetaData = metaData;
        ball = Instantiate(ballPrefab);
        players = new GameObject[Data.TotalPlayers];
        for (int i = 0; i < players.Length; i++)
        {
            players[i] = Instantiate(playerPrefab);
        }
        target = Instantiate(targetPrefab);
    }

    private void Update()
    {
        Data.HighlightTime += Time.deltaTime * Data.StepsPerSecond;
        BallTransformSystem.Run(ball);
        PlayerTransformSystem.Run(players);
        TargetTranformSystem.Run(target);
    }

}
