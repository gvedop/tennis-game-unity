using UnityEngine;

namespace TennisGame.Assets.Scripts
{
    public class AdversaryController : MonoBehaviour, IForceProvider
    {
        public SceneController sceneController;
        public PlayerController playerController;
        public BallController ballController;
        public float speed = 500f;
        public float xMin = -100f;
        public float xMax = 100f;
        public float yPosition = 0f;
        public float additionalForce = 50f;

        private Rigidbody2D _rigidbody;
        private BoxCollider2D _collider;
        private float _currentAdditionalForce = 0f;
        private float _horizontal = 0f;

        public float YForce
        {
            get { return -1f; }
        }

        public float XPosition
        {
            get { return transform.position.x; }
        }

        public float XColliderSize
        {
            get { return _collider.bounds.size.x; }
        }

        public float AdditionalForce
        {
            get { return _currentAdditionalForce; }
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<BoxCollider2D>();
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = Vector2.right * _horizontal * speed;
            _rigidbody.position = new Vector2(Mathf.Clamp(_rigidbody.position.x, xMin, xMax), yPosition);
        }

        private void Update()
        {
            //if (Input.GetKey(KeyCode.LeftArrow))
            //    MoveLeft();
            //if (Input.GetKey(KeyCode.RightArrow))
            //    MoveRight();
            //if (Input.GetKey(KeyCode.DownArrow))
            //    StopMove();
            //if (Input.GetKeyUp(KeyCode.UpArrow))
            //    OffAdditionalForce();
            //if (Input.GetKeyDown(KeyCode.UpArrow))
            //    OnAdditionalForce();

            Think();

            //RaycastHit2D hit = Physics2D.Raycast(ballController.Position, ballController.Velocity, Mathf.Infinity, _layerMask);
            //if (hit.collider != null)
            //{
            //    Debug.DrawLine(ballController.Position, hit.point, Color.yellow);
            //    var dir = Vector2.Reflect(ballController.Velocity, hit.normal);
            //    var hit2 = Physics2D.Raycast(hit.point, dir, Mathf.Infinity, _layerMask);
            //    if (hit2.collider != null)
            //    {
            //        Debug.DrawLine(hit.point, hit2.point, Color.red);
            //    }
            //}
        }

        private void Think()
        {
            var hit = GetBallHit(ballController.Position, ballController.Velocity);
            HandleHit(hit, ballController.Velocity, 3, 1);
        }

        private void HandleHit(RaycastHit2D hit, Vector2 direction, int maxStep, int currentStep)
        {
            if (hit.collider != null)
            {
                if (IsAdversaryHit(hit))
                {

                }
                else if (IsTopWallHit(hit))
                {
                    MoveByTopWall(hit);
                }
                else if (maxStep > currentStep)
                {
                    var reflectDirection = Vector2.Reflect(direction, hit.normal);
                    hit = GetBallHit(hit.point, reflectDirection);
                    HandleHit(hit, reflectDirection, maxStep, currentStep + 1);
                }
                else
                {
                    StopMove();
                }
            }
            else
            {
                StopMove();
            }
        }

        private void MoveByTopWall(RaycastHit2D hit)
        {
            if (hit.point.x > _rigidbody.position.x)
                MoveRight();
            else if (hit.point.x < _rigidbody.position.x)
                MoveLeft();
        }

        private bool IsTopWallHit(RaycastHit2D hit)
        {
            return hit.collider.gameObject.name == "TopWall";
        }

        private bool IsAdversaryHit(RaycastHit2D hit)
        {
            return hit.collider.gameObject.name == gameObject.name;
        }

        private RaycastHit2D GetBallHit(Vector2 origin, Vector2 direction)
        {
            var hit = Physics2D.Raycast(origin, direction, Mathf.Infinity);
            if (hit.collider != null)
                Debug.DrawLine(origin, hit.point, Color.red);
            return hit;
        }

        private void MoveRight()
        {
            _horizontal = Mathf.Clamp(_horizontal + 0.25f, -1f, 1f);
        }

        private void MoveLeft()
        {
            _horizontal = Mathf.Clamp(_horizontal - 0.25f, -1f, 1f);
        }

        private void StopMove()
        {
            _horizontal = 0f;
        }

        private void OnAdditionalForce()
        {
            _currentAdditionalForce = additionalForce;
        }

        private void OffAdditionalForce()
        {
            _currentAdditionalForce = 0f;
        }
    }
}
