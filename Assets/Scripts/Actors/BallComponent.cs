using UnityEngine;
using TennisGame.Game;

namespace TennisGame.Actors
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteRenderer), typeof(CircleCollider2D), typeof(Rigidbody2D))]
    public class BallComponent : MonoBehaviour, IActor
    {
        public RaycastHit2D[] Hits = new RaycastHit2D[3];

        private IGameController gameController;
        private SpriteRenderer selfSpriteRenderer;
        private CircleCollider2D selfCollider;
        private Rigidbody2D selfRigidbody;
        private LayerMask colliderLayerMask;
        private float speed = 0f;
        [SerializeField]
        private bool isShowRay = false;
        [SerializeField]
        private float timeCalcCollider = 0.5f;
        private float currentTimeCalcCollider = 0f;

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
            if (!selfSpriteRenderer)
                throw new UnassignedReferenceException("SpriteRenderer doesn't set.");
            selfCollider = GetComponent<CircleCollider2D>();
            if (!selfCollider)
                throw new UnassignedReferenceException("CircleCollider2D doesn't set.");
            selfRigidbody = GetComponent<Rigidbody2D>();
            if (!selfRigidbody)
                throw new UnassignedReferenceException("Rigidbody2D doesn't set.");
            colliderLayerMask = LayerMask.GetMask("ColliderObject");
        }

        private void Update()
        {
            currentTimeCalcCollider += Time.deltaTime;
            if (currentTimeCalcCollider > timeCalcCollider)
            {
                currentTimeCalcCollider = 0f;
                CalcColliders();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var collisionProvider = collision.gameObject.GetComponent<ICollisionProvider>();
            if (collisionProvider != null)
            {
                var direction = collisionProvider.GetCollisionDirection(transform.position);
                selfRigidbody.velocity = direction * (speed + collisionProvider.GetCollisionAdditionalForce());
                CalcColliders();
            }
        }

        private void OnDrawGizmos()
        {
            if (isShowRay && Hits[0])
            {
                DrawGizmosDirection(selfRigidbody.position, Hits[0].point);
                if (Hits[1])
                {
                    DrawGizmosDirection(Hits[0].point, Hits[1].point);
                    if (Hits[2])
                    {
                        DrawGizmosDirection(Hits[1].point, Hits[2].point);
                    }
                }
            }
        }

        private void DrawGizmosDirection(Vector2 from, Vector2 to)
        {
            var color = Gizmos.color;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(from, to);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(to, 12f);
            Gizmos.color = color;
        }

        private RaycastHit2D GetDirectionHit(Vector2 origin, Vector2 direction)
        {
            return Physics2D.Raycast(origin, direction, Mathf.Infinity, colliderLayerMask);
        }


        private void CalcColliders()
        {
            Hits[0] = GetDirectionHit(selfRigidbody.position, selfRigidbody.velocity);
            if (Hits[0])
            {
                var secondReflectDirection = Vector2.Reflect(selfRigidbody.velocity, Hits[0].normal);
                Hits[1] = GetDirectionHit(Hits[0].point, secondReflectDirection);
                if (Hits[1])
                {
                    var thirdReflectDirection = Vector2.Reflect(secondReflectDirection, Hits[1].normal);
                    Hits[2] = GetDirectionHit(Hits[1].point, thirdReflectDirection);
                }
            }
        }
    }
}
