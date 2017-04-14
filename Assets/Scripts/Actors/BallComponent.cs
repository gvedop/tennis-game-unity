using UnityEngine;
using TennisGame.Game;

namespace TennisGame.Actors
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteRenderer), typeof(CircleCollider2D), typeof(Rigidbody2D))]
    public class BallComponent: MonoBehaviour, IActor
    {
        private IGameController gameController;
        private SpriteRenderer selfSpriteRenderer;
        private CircleCollider2D selfCollider;
        private Rigidbody2D selfRigidbody;
        private float speed = 0f;
        
        public float Speed
        {
            get { return speed; }
            set { speed = value; }
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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var collisionProvider = collision.gameObject.GetComponent<ICollisionProvider>();
            if (collisionProvider != null)
            {
                var direction = collisionProvider.GetCollisionDirection(transform.position);
                selfRigidbody.velocity = direction * (speed + collisionProvider.GetCollisionAdditionalForce());
            }
        }
    }
}
