using UnityEngine;

namespace TennisGame.Assets.Scripts
{
    public class BallController : MonoBehaviour
    {
        public float speed = 300.0f;
        public float scale = 1f;

        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _rigidbody.velocity = Vector2.down * speed;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var forceProvider = collision.gameObject.GetComponent<IForceProvider>();
            if (forceProvider != null)
            {
                float x = hitFactor(transform.position.x, forceProvider.XPosition, forceProvider.XColliderSize);
                var direction = new Vector2(x, forceProvider.YForce).normalized;
                _rigidbody.velocity = direction * (speed + forceProvider.AdditionalForce);
            }
        }

        // 1  -0.5  0  0.5   1  <- x value
        private float hitFactor(float ballPos, float platformPos, float platformWidth)
        {
            return (ballPos - platformPos) / platformWidth / scale;
        }
    }
}
