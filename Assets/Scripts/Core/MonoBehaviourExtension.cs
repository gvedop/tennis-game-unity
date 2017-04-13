using UnityEngine;

namespace TennisGame.Core
{
    public static class MonoBehaviourExtension
    {
        public static void MulLocalScale(this MonoBehaviour obj, float value)
        {
            obj.transform.localScale *= value;
        }

        public static void SetLocalPosition(this MonoBehaviour obj, Vector2 pos)
        {
            obj.transform.localPosition = pos;
        }
    }
}
