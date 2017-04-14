
namespace TennisGame.Game
{
    public interface IGameControllerSubscriber
    {
        void RegisterGameController(IGameController controller);
        void UnregisterGameController();
    }
}
