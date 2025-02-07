using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class MoveComponent : MonoBehaviour
    {
        [SerializeField]
        private new Rigidbody2D rigidbody2D;

        [SerializeField]
        private float speed = 5.0f;

        private InputManager inputManager;

        private void Awake()
        {
            inputManager = FindObjectOfType<InputManager>();
        }

        public void MoveByRigidbodyVelocity(Vector2 vector)
        {
            var nextPosition = this.rigidbody2D.position + vector * this.speed;
            this.rigidbody2D.MovePosition(nextPosition);
        }
        private void FixedUpdate()
        {
            if (inputManager is null)
            {
                return;
            }
            MoveByRigidbodyVelocity(new Vector2(this.inputManager.HorizontalDirection, 0) * Time.fixedDeltaTime);
        }
    }
}