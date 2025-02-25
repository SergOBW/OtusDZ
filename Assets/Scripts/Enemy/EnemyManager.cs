using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField]
        private EnemyPositions enemyPositions;
        
        [SerializeField]
        private EnemyPool _enemyPool;

        [SerializeField]
        private BulletSystem _bulletSystem;
        
        private readonly HashSet<GameObject> _activeEnemies = new();

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                
                var enemy = _enemyPool.SpawnEnemy();
                
                if (enemy != null)
                {
                    if (_activeEnemies.Add(enemy))
                    {
                        InitializeEnemy(enemy);
                    }    
                }
            }
        }
        
        private void OnDestroyed(GameObject enemy)
        {
            if (_activeEnemies.Remove(enemy))
            {
                enemy.GetComponent<HitPointsComponent>().OnHpEmptyEvent -= OnDestroyed;

                _enemyPool.DeSpawnEnemy(enemy);
            }
        }
        
        private ITarget FindTarget()
        {
            ITarget target = FindObjectsOfType<MonoBehaviour>()
                .OfType<ITarget>()
                .FirstOrDefault();

            return target;
        }
        
        private void InitializeEnemy(GameObject enemy)
        {
            enemy.GetComponent<HitPointsComponent>().OnHpEmptyEvent += this.OnDestroyed;
            
            var attackPosition = enemyPositions.RandomAttackPosition();
            enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position);
                
            enemy.GetComponent<EnemyAttackAgent>().SetTarget(FindTarget().GetTransform());
        }
    }
}