using UnityEngine;

namespace TennisGame.Actors
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteRenderer), typeof(CircleCollider2D), typeof(Rigidbody2D))]
    public class BallComponent: MonoBehaviour
    {
        public float Speed = 300f;

        private SpriteRenderer selfSpriteRenderer;
        private CircleCollider2D selfCollider;
        private Rigidbody2D selfRigidbody;

        private void Awake()
        {
            selfSpriteRenderer = GetComponent<SpriteRenderer>();
            if (selfSpriteRenderer == null)
                throw new UnassignedReferenceException("SpriteRenderer doesn't set.");
            selfCollider = GetComponent<CircleCollider2D>();
            if (selfCollider == null)
                throw new UnassignedReferenceException("CircleCollider2D doesn't set.");
            selfRigidbody = GetComponent<Rigidbody2D>();
            if (selfRigidbody == null)
                throw new UnassignedReferenceException("Rigidbody2D doesn't set.");
        }

        // 1  -0.5  0  0.5   1  <- x value
        private float hitFactor(float ballPos, float platformPos, float platformWidth)
        {
            return (ballPos - platformPos) / platformWidth;
        }
    }
}
