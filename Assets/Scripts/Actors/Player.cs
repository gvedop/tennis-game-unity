using UnityEngine;

namespace TennisGame.Actors
{
    public class Player: Platform
    {
        protected override float GetHorizontalAxis()
        {
            return Input.GetAxis("Horizontal");
        }
    }
}
