using UnityEngine;

namespace TennisGame.Actors
{
    public class PlayerComponent: PlatformComponent
    {
        protected override float GetHorizontalAxis()
        {
            return Input.GetAxis("Horizontal");
        }
    }
}
