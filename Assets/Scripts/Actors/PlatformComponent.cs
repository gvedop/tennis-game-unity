using UnityEngine;
using TennisGame.Game;

namespace TennisGame.Actors
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D), typeof(Rigidbody2D))]
    public class PlatformComponent: MonoBehaviour, IActor, ICollisionProvider
    {
        protected IGameController gameController;
        protected float additionalForce;

        private SpriteRenderer selfSpriteRenderer;
        private BoxCollider2D selfCollider;
        private Rigidbody2D selfRigidbody;
        private float speed = 300f;
        private float leftBorder = -300f;
        private float rightBorder = 300f;
        private float yForce = 0f;
        private float widthScale = 1f;

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

        public float AdditionalForce
        {
            get { return additionalForce; }
            set { additionalForce = value; }
        }

        public float YForce
        {
            get { return yForce; }
            set { yForce = value; }
        }

        public float WidthScale
        {
            get { return widthScale; }
            set { widthScale = value; }
        }

        public float Height
        {
            get { return selfSpriteRenderer.bounds.size.y; }
        }

        public float MiddleHeight
        {
            get { return Height / 2f; }
        }

        public void SetBorder(float left, float right)
        {
            leftBorder = left;
            rightBorder = right;
        }

        public string ActorName
        {
            get { return gameObject.name; }
        }

        public void RegisterGameController(IGameController controller)
        {
            gameController = controller;
        }

        public void UnregisterGameController()
        {
            gameController = null;
        }

        public virtual float GetHorizontalAxis()
        {
            return 0f;
        }

        public Vector2 GetCollisionDirection(Vector2 position)
        {
            float x = GetHitFactor(position.x, transform.position.x, selfCollider.bounds.size.x);
            return new Vector2(x, yForce).normalized;
        }

        public virtual float GetCollisionAdditionalForce()
        {
            return 0f;
        }

        protected virtual void PostAwake()
        {

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

        private float GetHitFactor(float ballPos, float platformPos, float platformWidth)
        {
            return (ballPos - platformPos) / platformWidth * widthScale;
        }
    }
}
