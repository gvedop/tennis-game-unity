using UnityEngine;

namespace TennisGame.Actors
{
    public class PlayerComponent: PlatformComponent
    {
        public override float GetHorizontalAxis()
        {
            return Input.GetAxis("Horizontal");
        }
    }
}
