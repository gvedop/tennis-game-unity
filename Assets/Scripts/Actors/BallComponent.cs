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
        private LayerMask colliderLayerMask;
        private float speed = 0f;
        private Collider2D firstCollider;
        private Collider2D secondCollider;
        private Collider2D thirdCollider;
        [SerializeField]
        private bool isShowRay = false;
        private float timeCalcCollider = 0.7f;
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
            if (selfSpriteRenderer == null)
                throw new UnassignedReferenceException("SpriteRenderer doesn't set.");
            selfCollider = GetComponent<CircleCollider2D>();
            if (selfCollider == null)
                throw new UnassignedReferenceException("CircleCollider2D doesn't set.");
            selfRigidbody = GetComponent<Rigidbody2D>();
            if (selfRigidbody == null)
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

        private RaycastHit2D GetBallHit(Vector2 origin, Vector2 direction)
        {
            var hit = Physics2D.Raycast(origin, direction, Mathf.Infinity, colliderLayerMask);
            if (hit.collider != null && isShowRay)
                Debug.DrawLine(origin, hit.point, Color.red);
            return hit;
        }

        private void OnDrawGizmos()
        {
            if (isShowRay)
            {
                if (firstCollider != null)
                    Gizmos.DrawWireSphere(firstCollider.transform.position, 25);
                if (secondCollider != null)
                    Gizmos.DrawWireSphere(secondCollider.transform.position, 25);
                if (thirdCollider != null)
                    Gizmos.DrawWireSphere(thirdCollider.transform.position, 25);
            }
        }

        private void CalcColliders()
        {
            var hit = GetBallHit(selfRigidbody.position, selfRigidbody.velocity);
            firstCollider = hit.collider;
            if (firstCollider != null)
            { 
                var secondReflectDirection = Vector2.Reflect(selfRigidbody.velocity, hit.normal);
                hit = GetBallHit(hit.point, secondReflectDirection);
                secondCollider = hit.collider;
                if (secondCollider != null)
                {
                    var thirdReflectDirection = Vector2.Reflect(secondReflectDirection, hit.normal);
                    hit = GetBallHit(hit.point, thirdReflectDirection);
                    thirdCollider = hit.collider;
                }
                else
                {
                    thirdCollider = null;
                }
            }
            else
            {
                secondCollider = null;
                thirdCollider = null;
            }
        }
    }
}
