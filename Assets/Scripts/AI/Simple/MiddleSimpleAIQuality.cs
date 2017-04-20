using UnityEngine;

namespace TennisGame.AI.Simple
{
    public class MiddleSimpleAIQuality: ISimpleAIQuality
    {
        public int Deep
        {
            get { return 1; }
        }

        public float MoveStep
        {
            get { return 0.15f; }
        }

        public float GenerationItselfStepTime
        {
            get { return 1.5f; }
        }

        public bool IsApproveOnAdditionalForce
        {
            get { return Random.Range(-5, 10) >= 0; }
        }

        public bool IsOnAdditionalForce
        {
            get { return Random.Range(-10, 10) >= 0; }
        }
    }
}
