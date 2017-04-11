using UnityEngine;

namespace TennisGame.Actors
{
    public class Adversary: Platform
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
