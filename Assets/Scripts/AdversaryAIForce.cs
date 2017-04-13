using UnityEngine;
using TennisGame.AI;

namespace TennisGame.Assets.Scripts
{
    public class AdversaryAIForce
    {
        private AIQuality _adversaryAIType;

        public AdversaryAIForce(AIQuality adversaryAIType)
        {
            _adversaryAIType = adversaryAIType;
        }

        public int GetMaxStepThink()
        {
            switch (_adversaryAIType)
            {
                case AIQuality.Low:
                    return Random.Range(0, 2);
                case AIQuality.Middle:
                    return Random.Range(0, 3);
                case AIQuality.High:
                    return 3;
                default:
                    return 1;
            }
        }

        public float GetMoveForceStep()
        {
            switch (_adversaryAIType)
            {
                case AIQuality.Low:
                    return 0.1f;
                case AIQuality.Middle:
                    return 0.15f;
                case AIQuality.High:
                    return 0.2f;
                default:
                    return 0.1f;
            }
        }

        public bool GetAdditiveForceEnabled()
        {
            switch(_adversaryAIType)
            {
                case AIQuality.Low:
                    return Random.Range(-10, 3) >= 0;
                case AIQuality.Middle:
                    return Random.Range(-10, 10) >= 0;
                case AIQuality.High:
                    return Random.Range(-3, 10) >= 0;
                default:
                    return false;
            }
        }

        public bool GetAdditiveForceEnabledByOne()
        {
            switch (_adversaryAIType)
            {
                case AIQuality.Low:
                    return Random.Range(-10, 10) >= 0;
                case AIQuality.Middle:
                    return Random.Range(-5, 10) >= 0;
                case AIQuality.High:
                    return true;
                default:
                    return false;
            }
        }

        public float GetMaxTimeChangeStrategy()
        {
            switch (_adversaryAIType)
            {
                case AIQuality.Low:
                    return 2f;
                case AIQuality.Middle:
                    return 1.5f;
                case AIQuality.High:
                    return 1f;
                default:
                    return 2f;
            }
        }
    }
}
