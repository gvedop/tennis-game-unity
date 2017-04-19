using UnityEngine;
using TennisGame.Actors;

namespace TennisGame.Game
{
    public interface IGameController
    {
        BallComponent Ball { get; }
        bool IsItSelf(GameObject obj, GameObject target);
        bool IsOppositeWall(GameObject obj, GameObject target);
    }
}
