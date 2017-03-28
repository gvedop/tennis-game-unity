
namespace TennisGame.Assets.Scripts
{
    public interface IForceProvider
    {
        float YForce { get; }
        float XPosition { get; }
        float XColliderSize { get; }
        float AdditionalForce { get; }
    }
}
