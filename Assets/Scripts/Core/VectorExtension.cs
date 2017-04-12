using UnityEngine;

namespace TennisGame.Core
{
    public static class VectorExtension
    {
        public static Vector2 WithAddX(this Vector2 obj, float value)
        {
            return new Vector2(obj.x + value, obj.y);
        }

        public static Vector2 WithSubX(this Vector2 obj, float value)
        {
            return new Vector2(obj.x - value, obj.y);
        }

        public static Vector2 WithAddY(this Vector2 obj, float value)
        {
            return new Vector2(obj.x, obj.y + value);
        }

        public static Vector2 WithSubY(this Vector2 obj, float value)
        {
            return new Vector2(obj.x, obj.y - value);
        }
    }
}
