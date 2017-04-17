using UnityEngine;
using TennisGame.Actors;

namespace TennisGame.Assets.Scripts.AI.Simple
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(AdversaryComponent))]
    public class SimpleAIComponent: MonoBehaviour
    {
        private AdversaryComponent adversary;

        private void Awake()
        {
            adversary = GetComponent<AdversaryComponent>();
            if (!adversary)
                throw new UnassignedReferenceException("AdversaryComponent doesn't set.");
        }

        private void Update()
        {
            Think();
        }

        private void Think()
        {
            
        }
    }
}
