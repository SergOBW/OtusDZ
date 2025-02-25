using System.Collections.Generic;
using UnityEngine;
namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour
    {
        [SerializeField] private Transform worldTransform;
        [SerializeField] private LevelBounds levelBounds;
        [SerializeField] private BulletPool bulletPool;

        private readonly List<Bullet> _cache = new();

        private void FixedUpdate()
        {
            _cache.Clear();
            _cache.AddRange(bulletPool.GetActiveBullets());

            foreach (var bullet in _cache)
            {
                if (!levelBounds.InBounds(bullet.transform.position))
                {
                    RemoveBullet(bullet);
                }
            }
        }

        public void FlyBulletByArgs(Args args)
        {
            var bullet = bulletPool.GetBullet();
            bullet.transform.SetParent(worldTransform);
        
            bullet.SetPosition(args.position);
            bullet.SetColor(args.color);
            bullet.SetPhysicsLayer(args.physicsLayer);
            bullet.Damage = args.damage;
            bullet.IsPlayer = args.isPlayer;
            bullet.SetVelocity(args.velocity);
        
            bullet.OnCollisionEnteredEvent += OnBulletCollision;
        }

        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            BulletUtils.DealDamage(bullet, collision.gameObject);
            RemoveBullet(bullet);
        }

        private void RemoveBullet(Bullet bullet)
        {
            bullet.OnCollisionEnteredEvent -= OnBulletCollision;
            bulletPool.ReleaseBullet(bullet);
        }

        public struct Args
        {
            public Vector2 position;
            public Vector2 velocity;
            public Color color;
            public int physicsLayer;
            public int damage;
            public bool isPlayer;
        }
    }
}