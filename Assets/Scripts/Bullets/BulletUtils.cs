using UnityEngine;

namespace ShootEmUp
{
    public interface ITarget
    {
        public Transform GetTransform();
    }
    internal static class BulletUtils
    {
        internal static void DealDamage(Bullet bullet, GameObject other)
        {
            if (!other.TryGetComponent(out ITeam team))
            {
                return;
            }

            if (bullet.IsPlayer == team.IsPlayer())
            {
                return;
            }

            if (other.TryGetComponent(out HitPointsComponent hitPoints))
            {
                hitPoints.TakeDamage(bullet.Damage);
            }
        }
    }
}