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
        private LayerMask _layerMask;
        private float _currentAdditionalForce = 0f;
        private float _horizontal = 0f;
        private float _selfStep = 0f;
        private float _selfStepTime = 1f;
        private float _currentSelfStepTime = 0f;
        private float _moveStep = 0.15f;
        
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
            _layerMask = LayerMask.GetMask("ColliderBehaviour");
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = Vector2.right * _horizontal * speed;
            _rigidbody.position = new Vector2(Mathf.Clamp(_rigidbody.position.x, xMin, xMax), yPosition);
        }

        private void Update()
        {
            Think();
        }

        private void Think()
        {
            var hit = GetBallHit(ballController.Position, ballController.Velocity);
            HandleHit(hit, ballController.Velocity, Random.Range(0, 3), 0);
        }

        private void HandleHit(RaycastHit2D hit, Vector2 direction, int maxStep, int currentStep)
        {
            if (hit.collider != null)
            {
                if (IsAdversaryHit(hit))
                {
                    GenerateSelfStep(hit);
                    MoveBySelf(hit);
                }
                else if (IsTopWallHit(hit))
                {
                    GenerateSelfStep(hit);
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

        private void GenerateSelfStep(RaycastHit2D hit)
        {
            _currentSelfStepTime += Time.deltaTime;
            if (_currentSelfStepTime < _selfStepTime)
                return;
            _currentSelfStepTime = 0f;
            var rand = Random.Range(-10, 10);
            if (rand >= 2)
            {
                GenerateSelfStepByStrategy1(hit);
            }
            else if (rand >= -3 && rand < 2)
            {
                GenerateSelfStepByStrategy2(hit);
            }
            else
            {
                GenerateSelfStepByStrategy3(hit);
            }
            Debug.Log(_selfStep);
        }

        private void GenerateSelfStepByStrategy1(RaycastHit2D hit)
        {
            OnAdditionalForce();
            var size = _collider.size.x / 2f;
            var mSize = size / 2f;
            if (playerController.Position.x > hit.point.x)
                _selfStep = Random.Range(-size, -mSize);
            else
                _selfStep = Random.Range(mSize, size);
        }

        private void GenerateSelfStepByStrategy2(RaycastHit2D hit)
        {
            TryAdditionalForce();
            _selfStep = 0f;
        }

        private void GenerateSelfStepByStrategy3(RaycastHit2D hit)
        {
            TryAdditionalForce();
            var size = _collider.size.x / 2f;
            _selfStep = Random.Range(-size, size);
        }

        private void TryAdditionalForce()
        {
            if (Random.Range(-10, 10) >= 0)
                OnAdditionalForce();
            else
                OffAdditionalForce();
        }

        private void MoveBySelf(RaycastHit2D hit)
        {
            if (hit.point.x > _rigidbody.position.x + _selfStep)
                MoveRight();
            else if (hit.point.x < _rigidbody.position.x + _selfStep)
                MoveLeft();
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
            var hit = Physics2D.Raycast(origin, direction, Mathf.Infinity, _layerMask);
            if (hit.collider != null)
                Debug.DrawLine(origin, hit.point, Color.red);
            return hit;
        }

        private void MoveRight()
        {
            _horizontal = Mathf.Clamp(_horizontal + _moveStep, -1f, 1f);
        }

        private void MoveLeft()
        {
            _horizontal = Mathf.Clamp(_horizontal - _moveStep, -1f, 1f);
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
