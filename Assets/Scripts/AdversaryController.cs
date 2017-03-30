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
            if (Input.GetKey(KeyCode.LeftArrow))
                MoveLeft();
            if (Input.GetKey(KeyCode.RightArrow))
                MoveRight();
            if (Input.GetKey(KeyCode.DownArrow))
                StopMove();
            if (Input.GetKeyUp(KeyCode.UpArrow))
                OffAdditionalForce();
            if (Input.GetKeyDown(KeyCode.UpArrow))
                OnAdditionalForce();
        }

        private void MoveRight()
        {
            _horizontal = Mathf.Clamp(_horizontal + 0.1f, -1f, 1f);
        }

        private void MoveLeft()
        {
            _horizontal = Mathf.Clamp(_horizontal - 0.1f, -1f, 1f);
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
