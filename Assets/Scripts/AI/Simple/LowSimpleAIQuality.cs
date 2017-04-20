using UnityEngine;

namespace TennisGame.AI.Simple
{
    public class LowSimpleAIQuality: ISimpleAIQuality
    {
        public int Deep
        {
            get { return 0; }
        }

        public float MoveStep
        {
            get { return 0.1f; }
        }

        public float GenerationItselfStepTime
        {
            get { return 2f; }
        }

        public bool IsApproveOnAdditionalForce
        {
            get { return Random.Range(-10, 10) >= 0; }
        }

        public bool IsOnAdditionalForce
        {
            get { return Random.Range(-15, 10) >= 0; }
        }
    }
}
