using System;
using UnityEngine;

namespace TennisGame.Assets.Scripts
{
    public class PlatrformController: MonoBehaviour
    {
        public float speed = 600f;
        public float xMin = -400f;
        public float xMax = 400f;
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            var movement = new Vector2(Input.GetAxis("Horizontal"), 0.0f);
            _rigidbody.velocity = movement * speed;
            _rigidbody.position = new Vector2(Mathf.Clamp(_rigidbody.position.x, xMin, xMax), 0.0f);
            
        }

        private void Update()
        {
        }
    }
}
