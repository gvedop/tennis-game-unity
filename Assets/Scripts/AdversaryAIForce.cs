using UnityEngine;

namespace TennisGame.Assets.Scripts
{
    public class AdversaryAIForce
    {
        private AdversaryAIType _adversaryAIType;

        public AdversaryAIForce(AdversaryAIType adversaryAIType)
        {
            _adversaryAIType = adversaryAIType;
        }

        public int GetMaxStepThink()
        {
            switch (_adversaryAIType)
            {
                case AdversaryAIType.LOW:
                    return Random.Range(0, 2);
                case AdversaryAIType.MIDDLE:
                    return Random.Range(0, 3);
                case AdversaryAIType.HIGH:
                    return 3;
                default:
                    return 1;
            }
        }

        public float GetMoveForceStep()
        {
            switch (_adversaryAIType)
            {
                case AdversaryAIType.LOW:
                    return 0.1f;
                case AdversaryAIType.MIDDLE:
                    return 0.15f;
                case AdversaryAIType.HIGH:
                    return 0.2f;
                default:
                    return 0.1f;
            }
        }

        public bool GetAdditiveForceEnabled()
        {
            switch(_adversaryAIType)
            {
                case AdversaryAIType.LOW:
                    return Random.Range(-10, 3) >= 0;
                case AdversaryAIType.MIDDLE:
                    return Random.Range(-10, 10) >= 0;
                case AdversaryAIType.HIGH:
                    return Random.Range(-3, 10) >= 0;
                default:
                    return false;
            }
        }

        public bool GetAdditiveForceEnabledByOne()
        {
            switch (_adversaryAIType)
            {
                case AdversaryAIType.LOW:
                    return Random.Range(-10, 10) >= 0;
                case AdversaryAIType.MIDDLE:
                    return Random.Range(-5, 10) >= 0;
                case AdversaryAIType.HIGH:
                    return true;
                default:
                    return false;
            }
        }

        public float GetMaxTimeChangeStrategy()
        {
            switch (_adversaryAIType)
            {
                case AdversaryAIType.LOW:
                    return 2f;
                case AdversaryAIType.MIDDLE:
                    return 1.5f;
                case AdversaryAIType.HIGH:
                    return 1f;
                default:
                    return 2f;
            }
        }
    }
}
