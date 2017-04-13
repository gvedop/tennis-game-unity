using UnityEngine;

namespace TennisGame.Actors
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(BoxCollider2D))]
    public class WallComponent: MonoBehaviour
    {
        private BoxCollider2D selfCollider;

        public void Init(Vector2 size, Vector2 position)
        {
            selfCollider.size = size;
            transform.localPosition = position;
        }

        public void IgnoreCollision(Collider2D collider)
        {
            Physics2D.IgnoreCollision(selfCollider, collider);
        }

        public void IgnoreCollisions(params Collider2D[] colliders)
        {
            foreach (var col in colliders)
                Physics2D.IgnoreCollision(selfCollider, col);
        }

        private void Awake()
        {
            selfCollider = GetComponent<BoxCollider2D>();
            if (!selfCollider)
                throw new UnassignedReferenceException("BoxCollider2D doesn't set.");
        }
    }
}
