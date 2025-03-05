using System.Collections.Generic;
using UnityEngine;
namespace ShootEmUp

{
    public struct Args
    {
        public Vector2 position;
        public Vector2 velocity;
        public Color color;
        public int physicsLayer;
        public int damage;
        public bool isPlayer;
    }
    
    public sealed class BulletSystem : IFixedUpdate, IPauseGameListener, IResumeGameListener, IFinishGameListener
    {
        private LevelBounds _levelBounds;
        private BulletPool _bulletPool;

        private readonly List<Bullet> _cache = new();

        public BulletSystem(LevelBounds levelBounds, BulletPool bulletPool)
        {
            _levelBounds = levelBounds;
            _bulletPool = bulletPool;
        }

        public void CustomFixedUpdate()
        {
            _cache.Clear();
            _cache.AddRange(_bulletPool.GetActiveBullets());

            foreach (var bullet in _cache)
            {
                if (!_levelBounds.InBounds(bullet.transform.position))
                {
                    RemoveBullet(bullet);
                }
            }
        }

        public void FlyBulletByArgs(Args args)
        {
            var bullet = _bulletPool.GetBullet();
            bullet.SetWorldTransform();
        
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
            _bulletPool.ReleaseBullet(bullet);
        }

        public void OnPauseGame()
        {
            foreach (var bullet in _bulletPool.GetActiveBullets())
            {
                bullet.Pause();
            }
        }

        public void OnResumeGame()
        {
            foreach (var bullet in _bulletPool.GetActiveBullets())
            {
                bullet.Resume();
            }
        }


        public void OnFinishGame()
        {
            foreach (var bullet in _bulletPool.GetActiveBullets())
            {
                RemoveBullet(bullet);
            }
        }
    }
}