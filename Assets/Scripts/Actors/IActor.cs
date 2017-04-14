using TennisGame.Game;

namespace TennisGame.Actors
{
    public interface IActor: IGameControllerSubscriber
    {
        string ActorName { get; }
    }
}
