using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class HitPointsComponent : MonoBehaviour, ITeam
    {
        public event Action<GameObject> OnHpEmptyEvent;
        
        [SerializeField]private int hitPoints;

        [SerializeField]private bool isPlayer;
        
        public bool IsHitPointsExists() {
            return hitPoints > 0;
        }

        public void TakeDamage(int damage)
        {
            hitPoints -= damage;
            if (hitPoints <= 0)
            {
                OnHpEmptyEvent?.Invoke(gameObject);
            }
        }

        public bool IsPlayer()
        {
            return isPlayer;
        }
    }
    
    public interface ITeam
    {
        public bool IsPlayer();
    }
}