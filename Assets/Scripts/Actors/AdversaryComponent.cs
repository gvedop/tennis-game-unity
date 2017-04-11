using UnityEngine;

namespace TennisGame.Actors
{
    public class AdversaryComponent: PlatformComponent
    {
        private float horizontal = 0f;

        public void SetHorizontalAxis(float value)
        {
            horizontal = value;
        }

        protected override float GetHorizontalAxis()
        {
            return horizontal;
        }
    }
}
