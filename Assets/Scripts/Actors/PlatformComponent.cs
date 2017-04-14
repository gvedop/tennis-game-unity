using UnityEngine;

namespace TennisGame.Actors
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D), typeof(Rigidbody2D))]
    public class PlatformComponent: MonoBehaviour
    {
        private SpriteRenderer selfSpriteRenderer;
        private BoxCollider2D selfCollider;
        private Rigidbody2D selfRigidbody;
        private float speed = 300f;
        private float leftBorder = -300f;
        private float rightBorder = 300f;

        public SpriteRenderer SelfSpriteRenderer
        {
            get { return selfSpriteRenderer; }
        }

        public BoxCollider2D SelfCollider
        {
            get { return selfCollider; }
        }

        public Rigidbody2D SelfRigidbody
        {
            get { return selfRigidbody; }
        }

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public void SetBorder(float left, float right)
        {
            leftBorder = left;
            rightBorder = right;
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
            selfRigidbody.velocity = Vector2.right * speed * GetHorizontalAxis();
            var pos = selfRigidbody.position;
            selfRigidbody.position = new Vector2(Mathf.Clamp(pos.x, leftBorder, rightBorder), pos.y);
        }
    }
}
