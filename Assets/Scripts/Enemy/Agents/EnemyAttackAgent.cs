using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : MonoBehaviour, IFixedUpdate
    {
        [SerializeField] private WeaponComponent weaponComponent;
        [SerializeField] private EnemyMoveAgent moveAgent;
        [SerializeField] private float countdown;

        private Transform _target;
        private float _currentTime;
        private WeaponComponent _weaponComponent;
        private UpdatesDispatcher updatesDispatcher;

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        public void Initialize(UpdatesDispatcher updatesDispatcher)
        {
            this.updatesDispatcher = updatesDispatcher;
            _weaponComponent = GetComponent<WeaponComponent>();
            this.updatesDispatcher.AddNewListener(this);
            Reset();
        }

        public void DeInitialize()
        {
            _weaponComponent = null;
            updatesDispatcher.RemoveListener(this);
        }

        public void Reset()
        {
            _currentTime = countdown;
        }

        public void CustomFixedUpdate()
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