
namespace TennisGame.AI.Simple
{
    public interface ISimpleAIQuality
    {
        int Deep { get; }
        float MoveStep { get; }
        float GenerationItselfStepTime { get; }
        bool IsApproveOnAdditionalForce { get; }
        bool IsOnAdditionalForce { get; }
    }
}
