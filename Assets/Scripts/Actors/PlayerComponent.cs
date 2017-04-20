using UnityEngine;

namespace TennisGame.Actors
{
    public class PlayerComponent: PlatformComponent
    {
        public override float GetHorizontalAxis()
        {
            return Input.GetAxis("Horizontal") * 1.2f;
        }

        public override float GetCollisionAdditionalForce()
        {
            return Input.GetKey(KeyCode.UpArrow) ? additionalForce : 0f;
        }
    }
}
