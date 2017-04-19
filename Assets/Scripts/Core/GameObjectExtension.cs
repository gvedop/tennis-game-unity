using UnityEngine;

namespace TennisGame.Core
{
    public static class GameObjectExtension
    {
        public static int GetTransformInstanceID(this GameObject obj)
        {
            return obj.transform.GetInstanceID();
        }
    }
}
