using UnityEngine;

namespace TennisGame.Game
{
    public interface IGameController
    {
        RaycastHit2D[] Hits { get; }
        bool IsItSelf(GameObject obj, GameObject target);
        bool IsOppositeWall(GameObject obj, GameObject target);
    }
}
