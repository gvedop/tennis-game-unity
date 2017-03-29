using UnityEngine;

namespace TennisGame.Assets.Scripts
{
    public class WallController: MonoBehaviour
    {
        private BoxCollider2D _collider;

        public void Init(Vector2 size, Vector2 position)
        {
            _collider.size = size;
            transform.localPosition = position;
        }

        public void IgnoreCollisions(params Collider2D[] colliders)
        {
            foreach (var col in colliders)
                Physics2D.IgnoreCollision(_collider, col);
        }

        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
        }
    }
}
