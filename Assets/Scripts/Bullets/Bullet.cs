using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet, Collision2D> OnCollisionEnteredEvent;

        [NonSerialized] public bool IsPlayer;
        [NonSerialized] public int Damage;

        [SerializeField]
        private new Rigidbody2D rigidbody2D;

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        private Vector2 _targetVelocity;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEnteredEvent?.Invoke(this, collision);
        }

        public void SetVelocity(Vector2 velocity)
        {
            _targetVelocity = velocity;
            rigidbody2D.velocity = _targetVelocity;
        }

        public void SetPhysicsLayer(int physicsLayer)
        {
            gameObject.layer = physicsLayer;
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetColor(Color color)
        {
            spriteRenderer.color = color;
        }

        public void Pause()
        {
            rigidbody2D.velocity = Vector2.zero;
        }

        public void Resume()
        {
            rigidbody2D.velocity = _targetVelocity;
        }

        public void SetWorldTransform()
        {
            transform.SetParent(null);
        }
    }
}