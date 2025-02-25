using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletPool : MonoBehaviour
    {
        [SerializeField] private int initialCount = 50;
        [SerializeField] private Transform container;
        [SerializeField] private Bullet bulletPrefab;

        private readonly Queue<Bullet> _inactiveBullets = new();
        private readonly HashSet<Bullet> _activeBullets = new();

        private void Awake()
        {
            for (var i = 0; i < initialCount; i++)
            {
                var bullet = Instantiate(bulletPrefab, container);
                bullet.gameObject.SetActive(false);
                _inactiveBullets.Enqueue(bullet);
            }
        }

        public Bullet GetBullet()
        {
            if (!_inactiveBullets.TryDequeue(out var bullet))
            {
                bullet = Instantiate(bulletPrefab, container);
                bullet.gameObject.SetActive(false);
            }

            _activeBullets.Add(bullet);
            bullet.gameObject.SetActive(true);
            return bullet;
        }

        public void ReleaseBullet(Bullet bullet)
        {
            if (!_activeBullets.Remove(bullet)) return;
        
            bullet.gameObject.SetActive(false);
            bullet.transform.SetParent(container);
            _inactiveBullets.Enqueue(bullet);
        }

        public List<Bullet> GetActiveBullets()
        {
            return new List<Bullet>(_activeBullets);
        }
    }

}