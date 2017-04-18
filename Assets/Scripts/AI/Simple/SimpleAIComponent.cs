using UnityEngine;
using TennisGame.Actors;

namespace TennisGame.AI.Simple
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(AdversaryComponent))]
    public class SimpleAIComponent: MonoBehaviour
    {
        private AdversaryComponent adversary;
        [SerializeField]
        private AIQuality aiQuality = AIQuality.High;

        private void Awake()
        {
            adversary = GetComponent<AdversaryComponent>();
            if (!adversary)
                throw new UnassignedReferenceException("AdversaryComponent doesn't set.");
        }

        private void Update()
        {
            Think(3, 0);
        }

        private void Think(int deep, int current)
        {
            if (current >= adversary.GameController.Ball.Hits.Length)
                return;

            Debug.Log(string.Format("{0}: Think deep {1}", gameObject.name, current));

            var hit = adversary.GameController.Ball.Hits[current];
            if (hit.collider)
            {
                if (adversary.GameController.IsObjectSelf(gameObject, hit.collider.gameObject))
                {

                }
                else if (adversary.GameController.IsOppositeWall(gameObject, hit.collider.gameObject))
                {

                }
                else if (current < deep)
                {
                    Think(deep, current + 1);
                }
                else
                {
                    StopMove();
                }
            }
            else
            {
                StopMove();
            }
        }

        private void StopMove()
        {

        }
    }
}
