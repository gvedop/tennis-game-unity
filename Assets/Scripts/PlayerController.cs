using UnityEngine;

namespace TennisGame.Assets.Scripts
{
    public class PlayerController: MonoBehaviour, IForceProvider
    {
        public float speed = 500f;
        public float xMin = -100f;
        public float xMax = 100f;
        public float yPosition = 0f;
        public float additionalForce = 50f;

        private Rigidbody2D _rigidbody;
        private BoxCollider2D _collider;

        public float YForce
        {
            get { return 1f; }
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
            get { return Input.GetKey(KeyCode.UpArrow) ? additionalForce : 0f; }
        }

        public Vector2 Position
        {
            get { return _rigidbody.position; }
        }

        public Vector2 Velocity
        {
            get { return _rigidbody.velocity; }
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<BoxCollider2D>();
        }

        private void FixedUpdate()
        {
            var horizontal = Input.GetAxis("Horizontal");
            _rigidbody.velocity = Vector2.right * horizontal * speed;
            _rigidbody.position = new Vector2(Mathf.Clamp(_rigidbody.position.x, xMin, xMax), yPosition);
        }
    }
}
