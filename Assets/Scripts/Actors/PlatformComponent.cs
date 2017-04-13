using UnityEngine;

namespace TennisGame.Actors
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D), typeof(Rigidbody2D))]
    public class PlatformComponent: MonoBehaviour
    {
        public float Speed = 300f;
        public float LeftBorder = -300f;
        public float RightBorder = 300f;

        private SpriteRenderer selfSpriteRenderer;
        private BoxCollider2D selfCollider;
        private Rigidbody2D selfRigidbody;

        public SpriteRenderer SelfSpriteRenderer
        {
            get { return SelfSpriteRenderer; }
        }

        public BoxCollider2D SelfCollider
        {
            get { return selfCollider; }
        }

        public Rigidbody2D SelfRigidbody
        {
            get { return selfRigidbody; }
        }

        protected virtual void PostAwake()
        {

        }

        protected virtual float GetHorizontalAxis()
        {
            return 0f;
        }

        private void Awake()
        {
            selfSpriteRenderer = GetComponent<SpriteRenderer>();
            if (!selfSpriteRenderer)
                throw new UnassignedReferenceException("SpriteRenderer doesn't set.");
            selfCollider = GetComponent<BoxCollider2D>();
            if (!selfCollider)
                throw new UnassignedReferenceException("BoxCollider2D doesn't set.");
            selfRigidbody = GetComponent<Rigidbody2D>();
            if (!selfRigidbody)
                throw new UnassignedReferenceException("Rigidbody2D doesn't set.");
            PostAwake();
        }

        private void FixedUpdate()
        {
            selfRigidbody.velocity = Vector2.right * Speed * GetHorizontalAxis();
            var pos = selfRigidbody.position;
            selfRigidbody.position = new Vector2(Mathf.Clamp(pos.x, LeftBorder, RightBorder), pos.y);
        }
    }
}
