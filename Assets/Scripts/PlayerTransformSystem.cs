using System;
using UnityEngine;

public static class PlayerTransformSystem
{
    public static void Run(GameObject[] players)
    {

        var activeGame = Data.SequenceData;
        var playerTransforms = activeGame.PlayerTransforms;

        var time = Data.HighlightTime;

        int sequenceLength = Data.SequenceMetaData.TotalSteps;
        float progress = time / sequenceLength;
        progress = Math.Min(1, progress);
        int length = sequenceLength - 1;
        float stepIndexFloat = progress * length;
        int step1Index = (int)(stepIndexFloat);
        int step2Index = Math.Min(length, step1Index + 1);
        float stepProgress = stepIndexFloat - step1Index;

        for (int i = 0; i < players.Length; i++)
        {

            var transform = playerTransforms.Get(step1Index, i);
            var transform2 = playerTransforms.Get(step2Index, i);
            var pos1 = transform.Position;
            var pos2 = transform2.Position;
            var dir1 = transform.Direction;
            var dir2 = transform2.Direction;

            Vector4 temp1 = new(pos1.x, pos1.y, dir1.x, dir1.y);
            Vector4 temp2 = new(pos2.x, pos2.y, dir2.x, dir2.y);

            Vector4 temp = Vector4.Lerp(temp1, temp2, stepProgress);
            Vector3 newPos = new(temp.x, players[i].transform.position.y, temp.y);
            newPos = Vector3.Scale(Data.PlayerScale, newPos);
            Vector3 newDir = new(temp.z, 0, temp.w);
            players[i].transform.position = newPos;
            players[i].transform.rotation = Quaternion.LookRotation(newDir);

        }
    }
}
