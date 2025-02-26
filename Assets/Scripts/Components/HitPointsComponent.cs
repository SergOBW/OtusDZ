using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class HitPointsComponent : MonoBehaviour, ITeam
    {
        public event Action<GameObject> OnHpEmptyEvent;
        
        [SerializeField]private int startedHitpoints = 5;
        private int _hitPoints;

        [SerializeField]private bool isPlayer;
        
        public bool IsHitPointsExists() {
            return _hitPoints > 0;
        }

        public void TakeDamage(int damage)
        {
            _hitPoints -= damage;
            if (_hitPoints <= 0)
            {
                OnHpEmptyEvent?.Invoke(gameObject);
            }
        }

        public bool IsPlayer()
        {
            return isPlayer;
        }

        public void SetDefaults()
        {
            _hitPoints = startedHitpoints;
        }
    }
    
    public interface ITeam
    {
        public bool IsPlayer();
    }
}