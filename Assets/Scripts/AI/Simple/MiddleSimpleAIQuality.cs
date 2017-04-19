
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
    }
}
