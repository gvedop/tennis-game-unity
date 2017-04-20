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
        private ISimpleAIQuality simpleAIQuality;
        private float itselfStep = 0f;
        private float currentGenerationItselfStepTime = 0f;

        private void Awake()
        {
            adversary = GetComponent<AdversaryComponent>();
            if (!adversary)
                throw new UnassignedReferenceException("AdversaryComponent doesn't set.");
            simpleAIQuality = aiQuality.GetSimpleAIQuality();
        }

        private void Update()
        {
            Think(simpleAIQuality.Deep, 0);
        }

        private void OnValidate()
        {
            simpleAIQuality = aiQuality.GetSimpleAIQuality();
        }

        private void Think(int deep, int current)
        {
            if (current >= adversary.GameController.Hits.Length)
                return;

            //Debug.Log(string.Format("{0}: Think deep {1}", gameObject.name, current));

            var hit = adversary.GameController.Hits[current];
            if (hit.collider)
            {
                if (adversary.GameController.IsItSelf(gameObject, hit.collider.gameObject))
                {
                    GenerateItselfStep(hit.point.x);
                    MoveByItSelf(hit.point.x);
                }
                else if (adversary.GameController.IsSelfWall(gameObject, hit.collider.gameObject))
                {
                    GenerateItselfStep(hit.point.x);
                    MoveBySelfWall(hit.point.x);
                }
                else if (adversary.GameController.IsOppositeWall(gameObject, hit.collider.gameObject))
                {
                    MoveByOppositeWall(hit.point.x);
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

        private void GenerateItselfStep(float posX)
        {
            currentGenerationItselfStepTime += Time.deltaTime;
            if (currentGenerationItselfStepTime <= simpleAIQuality.GenerationItselfStepTime)
                return;
            currentGenerationItselfStepTime = 0f;
            adversary.OffAdditionalForce();
            var rand = Random.Range(-10, 10);
            if (rand >= 2)
            {
                GenerateSelfStepByStrategy1(posX);
            }
            else if (rand >= -3 && rand < 2)
            {
                GenerateSelfStepByStrategy2(posX);
            }
            else
            {
                GenerateSelfStepByStrategy3(posX);
            }
        }

        private void GenerateSelfStepByStrategy3(float posX)
        {
            if (simpleAIQuality.IsApproveOnAdditionalForce)
                adversary.OnAdditionalForce();
            var size = adversary.SelfCollider.size.x / 2f;
            var mSize = size / 2f;
            if (adversary.GameController.GetOppositeAdversaryPositionX(gameObject) > posX)
                itselfStep = Random.Range(-size, -mSize);
            else
                itselfStep = Random.Range(mSize, size);
        }

        private void GenerateSelfStepByStrategy2(float posX)
        {
            if (simpleAIQuality.IsOnAdditionalForce)
                adversary.OnAdditionalForce();
            itselfStep = 0f;
        }

        private void GenerateSelfStepByStrategy1(float posX)
        {
            if (simpleAIQuality.IsOnAdditionalForce)
                adversary.OnAdditionalForce();
            var size = adversary.SelfCollider.size.x / 2f;
            itselfStep = Random.Range(-size, size);
        }

        private void MoveByItSelf(float posX)
        {
            MoveBy(posX, itselfStep);
        }

        private void MoveBySelfWall(float posX)
        {
            MoveBy(posX, 0f);
        }

        private void MoveByOppositeWall(float posX)
        {
            MoveBy(posX, 0f);
        }

        private void MoveBy(float posX, float step)
        {
            if (posX > adversary.SelfRigidbody.position.x + step)
                MoveRight();
            else if (posX < adversary.SelfRigidbody.position.x + step)
                MoveLeft();
        }

        private void MoveLeft()
        {
            adversary.SetHorizontalAxis(Mathf.Clamp(adversary.GetHorizontalAxis() - simpleAIQuality.MoveStep, -1f, 1f));
        }

        private void MoveRight()
        {
            adversary.SetHorizontalAxis(Mathf.Clamp(adversary.GetHorizontalAxis() + simpleAIQuality.MoveStep, -1f, 1f));
        }

        private void StopMove()
        {
            adversary.SetHorizontalAxis(0f);
        }
    }
}
