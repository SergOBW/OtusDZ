using UnityEngine;
using VContainer;

namespace ShootEmUp
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        public Vector2 FirePointPosition => firePoint.position;
        public Quaternion FirePointRotation => firePoint.rotation;
        
        [SerializeField] private BulletConfig bulletConfig;
        [SerializeField] private Transform firePoint;
        
        private BulletSystem _bulletSystem;
        
        private ITeam _team;
        public void Initialize(BulletSystem bulletSystem)
        {
            _bulletSystem = bulletSystem;
            _team = GetComponent<ITeam>();
        }
        
        public void Fire(Vector2 fireDirection)
        {
            _bulletSystem.FlyBulletByArgs(new Args
            {
                isPlayer = _team.IsPlayer(),
                physicsLayer = (int) bulletConfig.physicsLayer,
                color = bulletConfig.color,
                damage = bulletConfig.damage,
                position = FirePointPosition,
                velocity = fireDirection  * bulletConfig.speed
            });
        }
        
        public void Fire()
        {
            _bulletSystem.FlyBulletByArgs(new Args
            {
                isPlayer = _team.IsPlayer(),
                physicsLayer = (int) bulletConfig.physicsLayer,
                color = bulletConfig.color,
                damage = bulletConfig.damage,
                position = FirePointPosition,
                velocity = FirePointRotation * Vector3.up * bulletConfig.speed
            });
        }
        
    }
}