using UnityEngine;

namespace TennisGame
{
    public class Adversary: Platform
    {
        private float horizontal = 0f;

        protected override float GetHorizontalAxis()
        {
            return horizontal;
        }
    }
}
