using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : MonoBehaviour, IFixedUpdate
    {
        public bool IsReached => _isReached;

        [SerializeField] private MoveComponent moveComponent;

        private Vector2 _destination;

        private bool _isReached;

        public void Initialize()
        {
            UpdateController.Instance.AddNewListener(this);
        }

        public void DeInitialize()
        {
            UpdateController.Instance.RemoveListener(this);
        }

        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            _isReached = false;
        }
        
        public void CustomFixedUpdate()
        {
            if (_isReached)
            {
                return;
            }
            
            var vector = _destination - (Vector2) transform.position;
            if (vector.magnitude <= 0.25f)
            {
                _isReached = true;
                return;
            }

            var direction = vector.normalized * Time.fixedDeltaTime;
            moveComponent.MoveByRigidbodyVelocity(direction);
        }
    }
}