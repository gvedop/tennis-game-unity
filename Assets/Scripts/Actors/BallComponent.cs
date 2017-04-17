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
        private RaycastHit2D firstHit;
        private RaycastHit2D secondHit;
        private RaycastHit2D thirdHit;
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
            if (isShowRay)
            {
                if (firstHit)
                    Gizmos.DrawWireSphere(firstHit.transform.position, 25);
                if (secondHit)
                    Gizmos.DrawWireSphere(secondHit.transform.position, 25);
                if (thirdHit)
                    Gizmos.DrawWireSphere(thirdHit.transform.position, 25);
            }
        }

        private RaycastHit2D GetDirectionHit(Vector2 origin, Vector2 direction)
        {
            var hit = Physics2D.Raycast(origin, direction, Mathf.Infinity, colliderLayerMask);
            if (hit.collider != null && isShowRay)
                Debug.DrawLine(origin, hit.point, Color.red);
            return hit;
        }


        private void CalcColliders()
        {
            firstHit = GetDirectionHit(selfRigidbody.position, selfRigidbody.velocity);
            if (firstHit)
            { 
                var secondReflectDirection = Vector2.Reflect(selfRigidbody.velocity, firstHit.normal);
                secondHit = GetDirectionHit(firstHit.point, secondReflectDirection);
                if (secondHit)
                {
                    var thirdReflectDirection = Vector2.Reflect(secondReflectDirection, secondHit.normal);
                    thirdHit = GetDirectionHit(secondHit.point, thirdReflectDirection);
                }
            }
        }
    }
}
