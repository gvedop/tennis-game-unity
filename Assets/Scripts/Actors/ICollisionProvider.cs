using UnityEngine;

namespace TennisGame.Actors
{
    public interface ICollisionProvider
    {
        Vector2 GetCollisionDirection(Vector2 position);
        float GetCollisionAdditionalForce();
    }
}
