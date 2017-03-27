﻿using UnityEngine;

namespace TennisGame.Assets.Scripts
{
    public class AdversaryController: MonoBehaviour
    {
        public float speed = 600f;

        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            //var horizontal = Input.GetAxis("Horizontal");
            var horizontal = Random.Range(-1f, 1f);
            var movement = new Vector2(horizontal, 0f);
            _rigidbody.velocity = movement * speed;
        }
    }
}
