
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
    }
}
