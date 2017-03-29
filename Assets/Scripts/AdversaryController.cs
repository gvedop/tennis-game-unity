using UnityEngine;

namespace TennisGame.Assets.Scripts
{
    public class AdversaryController: MonoBehaviour, IForceProvider
    {
        public SceneController sceneController;
        public BallController ballController;
        public float speed = 500f;
        public float xMin = -100f;
        public float xMax = 100f;
        public float yPosition = 0f;
        public float additionalForce = 50f;

        private Rigidbody2D _rigidbody;
        private BoxCollider2D _collider;

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
            get { return Input.GetKey(KeyCode.UpArrow) ? additionalForce : 0f; }
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<BoxCollider2D>();
        }

        private void FixedUpdate()
        {
            var horizontal = Input.GetAxis("Horizontal");
            //var horizontal = Random.Range(-1f, 1f);
            _rigidbody.velocity = Vector2.right * horizontal * speed;
            _rigidbody.position = new Vector2(Mathf.Clamp(_rigidbody.position.x, xMin, xMax), yPosition);
        }
    }
}
