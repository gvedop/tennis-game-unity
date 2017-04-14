using UnityEngine;

namespace TennisGame.Actors
{
    public interface ICollisionProvider
    {
        float GetCollisionHitFactor(Vector2 position);
        Vector2 GetCollisionDirection(Vector2 position);
        Vector2 GetCollisionDirection(float hitFactor);
        float GetCollisionAdditionalForce();
    }
}
