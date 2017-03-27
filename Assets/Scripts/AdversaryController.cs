using UnityEngine;

namespace TennisGame.Assets.Scripts
{
    public class AdversaryController: MonoBehaviour
    {
        public float speed = 500f;
        public float xMin = -100f;
        public float xMax = 100f;

        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            var horizontal = Input.GetAxis("Horizontal");
            //var horizontal = Random.Range(-1f, 1f);
            var movement = new Vector2(horizontal, 0f);
            _rigidbody.velocity = movement * speed;
            _rigidbody.position = new Vector2(Mathf.Clamp(_rigidbody.position.x, xMin, xMax), 0.0f);
        }
    }
}
