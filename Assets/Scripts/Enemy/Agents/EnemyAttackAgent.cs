using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : MonoBehaviour
    {
        [SerializeField] private WeaponComponent weaponComponent;
        [SerializeField] private EnemyMoveAgent moveAgent;
        [SerializeField] private float countdown;

        private Transform _target;
        private float _currentTime;
        private WeaponComponent _weaponComponent;

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        private void Awake()
        {
            _weaponComponent = GetComponent<WeaponComponent>();
        }

        public void Reset()
        {
            _currentTime = countdown;
        }

        private void FixedUpdate()
        {
            if (!moveAgent.IsReached)
            {
                return;
            }

            if (!_weaponComponent)
            {
                return;
            }
            
            if (!_target.GetComponent<HitPointsComponent>().IsHitPointsExists())
            {
                return;
            }

            _currentTime -= Time.fixedDeltaTime;
            if (_currentTime <= 0)
            {
                Fire();
                _currentTime += countdown;
            }
        }

        private void Fire()
        {
            var vector = (Vector2) _target.transform.position - _weaponComponent.FirePointPosition;
            var direction = vector.normalized;
            
            _weaponComponent.Fire(direction);
        }
    }
}