using UnityEngine;

namespace TennisGame.AI.Simple
{
    public class HighSimpleAIQuality: ISimpleAIQuality
    {
        public int Deep
        {
            get { return 2; }
        }

        public float MoveStep
        {
            get { return 0.2f; }
        }

        public float GenerationItselfStepTime
        {
            get { return 1f; }
        }

        public bool IsApproveOnAdditionalForce
        {
            get { return true; }
        }

        public bool IsOnAdditionalForce
        {
            get { return Random.Range(-3, 10) >= 0; }
        }
    }
}
