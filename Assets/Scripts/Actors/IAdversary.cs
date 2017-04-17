using TennisGame.Game;

namespace TennisGame.Actors
{
    public interface IAdversary: IActor
    {
        IGameController GameController { get; }
    }
}
