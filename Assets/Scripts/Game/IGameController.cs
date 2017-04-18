using UnityEngine;
using TennisGame.Actors;

namespace TennisGame.Game
{
    public interface IGameController
    {
        BallComponent Ball { get; }
        bool IsObjectSelf(GameObject self, GameObject target);
        bool IsOppositeWall(GameObject self, GameObject target);
    }
}
