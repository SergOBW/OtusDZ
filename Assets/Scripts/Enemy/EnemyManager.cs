using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour, IUpdate, IStartGameListener  , IPauseGameListener, IFinishGameListener, IResumeGameListener
    {
        [SerializeField] private EnemyPositions enemyPositions;
        
        [SerializeField] private EnemyPool _enemyPool;

        [SerializeField] private BulletSystem _bulletSystem;

        [SerializeField] private float spawnTimer = 1f;
        
        private readonly HashSet<GameObject> _activeEnemies = new();
        
        private float _timer;
        private bool _isSpawning;
        
        public void CustomUpdate()
        {
            if (!_isSpawning) return;
    
            _timer += Time.deltaTime;
    
            if (_timer >= spawnTimer)
            {
                _timer = 0f;
                SpawnEnemy();
            }
        }
        
        private void SpawnEnemy()
        {
            var enemy = _enemyPool.SpawnEnemy();
    
            if (enemy != null && _activeEnemies.Add(enemy))
            {
                InitializeEnemy(enemy);
            }
        }
        
        private void InitializeEnemy(GameObject enemy)
        {
            enemy.GetComponent<HitPointsComponent>().SetDefaults();
            enemy.GetComponent<HitPointsComponent>().OnHpEmptyEvent += this.OnEnemyDestroyed;
            
            var attackPosition = enemyPositions.RandomAttackPosition();
            enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position);
                
            enemy.GetComponent<EnemyAttackAgent>().SetTarget(FindTarget().GetTransform());
            
            enemy.GetComponent<EnemyMoveAgent>().Initialize();
            enemy.GetComponent<EnemyAttackAgent>().Initialize();
            
        }
        
        
        private void OnEnemyDestroyed(GameObject enemy)
        {
            if (_activeEnemies.Remove(enemy))
            {
                enemy.GetComponent<HitPointsComponent>().OnHpEmptyEvent -= OnEnemyDestroyed;

                _enemyPool.DeSpawnEnemy(enemy);
            }
        }
        

        #region IGameListener

        public void OnStartGame()
        {
            _isSpawning = true;
        }
        
        public void OnPauseGame()
        {
            _isSpawning = false;
        }
        
        public void OnFinishGame()
        {
            _isSpawning = false;
            foreach (var enemy in _activeEnemies)
            {
                enemy.GetComponent<HitPointsComponent>().OnHpEmptyEvent -= OnEnemyDestroyed;
                
                enemy.GetComponent<EnemyMoveAgent>().DeInitialize();
                enemy.GetComponent<EnemyAttackAgent>().DeInitialize();

                _enemyPool.DeSpawnEnemy(enemy);
            }
            
            _activeEnemies.Clear();
        }
        
        public void OnResumeGame()
        {
            _isSpawning = true;
        }

        #endregion

        #region Utils

        private ITarget FindTarget()
        {
            ITarget target = FindObjectsOfType<MonoBehaviour>()
                .OfType<ITarget>()
                .FirstOrDefault();

            return target;
        }


        #endregion
        
    }
}