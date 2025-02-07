using UnityEngine;

namespace ShootEmUp
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        public Vector2 Position => firePoint.position;

        public Quaternion Rotation => firePoint.rotation;
        
        [SerializeField] private BulletSystem bulletSystem;
        [SerializeField] private BulletConfig bulletConfig;
        [SerializeField] private Transform firePoint;

        private InputManager inputManager;

        private void Awake()
        {
            inputManager = GetComponent<InputManager>();
        }

        private void Update()
        {
            if (inputManager is null)
            {
                return;
            }

            if (inputManager.IsFireButtonDown)
            {
                OnFlyBullet();
            }
        }

        private void OnFlyBullet()
        {
            bulletSystem.FlyBulletByArgs(new BulletSystem.Args
            {
                isPlayer = true,
                physicsLayer = (int) bulletConfig.physicsLayer,
                color = bulletConfig.color,
                damage = bulletConfig.damage,
                position = Position,
                velocity = Rotation * Vector3.up * this.bulletConfig.speed
            });
        }
    }
}