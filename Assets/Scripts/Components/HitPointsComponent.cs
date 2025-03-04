using System;
using UnityEngine;
using VContainer;

namespace ShootEmUp
{
    public sealed class HitPointsComponent : MonoBehaviour, ITeam
    {
        public event Action<GameObject> OnHpEmptyEvent;
        
        [SerializeField]private int startedHitpoints = 5;
        private int _hitPoints;

        [SerializeField]private bool isPlayer;

        [Inject]
        private EventBus _eventBus;
        
        public bool IsHitPointsExists() {
            return _hitPoints > 0;
        }

        public void TakeDamage(int damage)
        {
            _hitPoints -= damage;
            if (_hitPoints <= 0)
            {
                OnHpEmptyEvent?.Invoke(gameObject);
                if (isPlayer)
                {
                    _eventBus.Publish(new PlayerDiedEvent());
                }
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