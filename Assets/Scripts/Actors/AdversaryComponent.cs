using UnityEngine;
using TennisGame.Game;

namespace TennisGame.Actors
{
    public class AdversaryComponent: PlatformComponent, IAdversary
    {
        private float horizontal = 0f;
        private float currentAdditionalForce = 0f;

        public IGameController GameController
        {
            get { return gameController; }
        }

        public void SetHorizontalAxis(float value)
        {
            horizontal = value;
        }

        public override float GetHorizontalAxis()
        {
            return horizontal;
        }

        public override float GetCollisionAdditionalForce()
        {
            return currentAdditionalForce;
        }

        public void OnAdditionalForce()
        {
            currentAdditionalForce = additionalForce;
        }

        public void OffAdditionalForce()
        {
            currentAdditionalForce = 0f;
        }
    }
}
