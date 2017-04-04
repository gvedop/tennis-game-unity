using UnityEngine;

namespace TennisGame
{
    public class Player: Platform
    {
        protected override float GetHorizontalAxis()
        {
            return Input.GetAxis("Horizontal");
        }
    }
}
