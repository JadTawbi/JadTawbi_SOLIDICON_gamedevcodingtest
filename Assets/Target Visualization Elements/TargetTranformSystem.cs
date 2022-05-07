using System;
using UnityEngine;

public static class TargetTranformSystem
{
    public static void Run(GameObject target)
    {
        var time = Data.HighlightTime;
        var activeGame = Data.SequenceData;
        var ballTransforms = activeGame.BallTransforms;
        var playerTransforms = activeGame.PlayerTransforms;
        int sequenceLength = Data.SequenceMetaData.TotalSteps;
        float progress = time / sequenceLength;
        progress = Math.Min(1, progress);
        int length = sequenceLength - 1;
        float stepIndexFloat = progress * length;
        int step1Index = (int)(stepIndexFloat);

        //Visualizing target and Trajectory

        var lineRenderer = target.GetComponent<LineRenderer>();

        Vector2 stepPos, nextPos, currentMovingDirection, previoustMovingDirection, stepPlayerPos;
        Vector3 pos2;
        float angle, distanceBetweenPos2AndCurrentTargetPos, distanceBetweenStepPlayerPosAndStepPos;
        int targetIndex;
        bool playerTouched;

        if (step1Index < length)
        {
            stepPos = new Vector2(ballTransforms[step1Index].Position.x, ballTransforms[step1Index].Position.z);
            nextPos = new Vector2(ballTransforms[step1Index + 1].Position.x, ballTransforms[step1Index + 1].Position.z);
            previoustMovingDirection = (nextPos - stepPos).normalized;

            for (int i = step1Index + 1; i < length; i++)
            {
                stepPos = new Vector2(ballTransforms[i].Position.x, ballTransforms[i].Position.z);               //position of the ball on the current index
                nextPos = new Vector2(ballTransforms[i + 1].Position.x, ballTransforms[i + 1].Position.z);       //postion of the ball on the next index
                currentMovingDirection = (nextPos - stepPos).normalized;                                         //direction vector

                angle = Vector3.Angle(currentMovingDirection, previoustMovingDirection);                         //angle difference between current direction vector and previous direction vector
                
                playerTouched = false;                                                                           //boolean to check if any player is close enough to the ball
                for (int p = 0; p < Data.TotalPlayers; p++)                                                      //going through the player list
                {
                    var transform = playerTransforms.Get(i, p);                                                  //getting the transform of the player at index[p] at the time of index[i]
                    stepPlayerPos = new Vector2(transform.Position.x, transform.Position.y);                     //geting current position of the player
                    distanceBetweenStepPlayerPosAndStepPos = Vector2.Distance(stepPos, stepPlayerPos);           //caculating the distance between the player and the ball
                    if (distanceBetweenStepPlayerPosAndStepPos < 0.02f)                                          //checking if close enough to be considered touching
                    {
                        playerTouched = true;                                                                    //player has touched the ball
                    }
                }

                if (angle >= 2.0f && playerTouched)                                                              //if the angle is higher than the possible curving angle of the ball (the player has touched it) made more accurate by checking for collision with all 22 players (can be tweeked))
                {
                    pos2 = new Vector3(stepPos.x, target.transform.position.y, stepPos.y);                       //position where the red target should be placed
                    pos2 = Vector3.Scale(Data.PlayerScale, pos2);                                                //scaling the vector
                    distanceBetweenPos2AndCurrentTargetPos = Vector3.Distance(pos2, target.transform.position);  //distance between current target pos and the new target position

                    if (distanceBetweenPos2AndCurrentTargetPos > 0.0f)                                           //this value (0.0f) can be increased to avoid showing repetitive and near touches of the ball 
                    {
                        targetIndex = i;                                                                         //index of the current target
                        //target.SetActive(true);
                        target.transform.position = pos2;                                                        //updating the position of the target
                        for (int y = step1Index; y <= targetIndex; y++)                                          //drawing the path of the ball between the current target and the next target
                        {
                            lineRenderer.positionCount = targetIndex - step1Index + 1;
                            for (int index = 0; index < lineRenderer.positionCount; index++)
                            {
                                lineRenderer.SetPosition(index, Vector3.Scale(Data.PlayerScale, ballTransforms[step1Index + index].Position));
                            }
                        }
                    }
                    break;
                }

                previoustMovingDirection = currentMovingDirection;
            }
        }

        /*
        logic for visualization of point of intersection:
        
        First we iterate through all the elements of the ballTransforms array.
        I wanted to show where the player would be standing when he meets the ball.
        to do so, we cast all the ball's Vector3 positions into Vector2 positions. We remove the Y element in order to create a strictly X,Z Vector ignoring altitude (this is easily changeable if we want to see where they meet in the air).
        That is because the next thing we want to do is figure out when the ball changes the direction in which it is moving (in 2D ignoring altitude). That is when it has been touched by a player.
        To do that, we iterate through each two consecutive transforms for the ball and do: nextStepTransform.Position - stepTransform.Position.
        This gives us the flat direction vector the ball is going in. We then check when that direction changes, that would be when the ball had been touched.
        We take the previous position and place a marker there. It would be in the same place the player would have touched it.
        */
    }
}
