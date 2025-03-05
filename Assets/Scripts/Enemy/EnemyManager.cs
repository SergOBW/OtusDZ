using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyManager : IUpdate, IStartGameListener  , IPauseGameListener, IFinishGameListener, IResumeGameListener
    {
        private float spawnTimer = 1f;
        
        private readonly HashSet<GameObject> _activeEnemies = new();
        
        private float _timer;
        private bool _isSpawning;

        private UpdatesDispatcher _updatesDispatcher;
        private EnemyPositions _enemyPositions;
        private EnemyPool _enemyPool;
        private BulletSystem _bulletSystem;
        
        public EnemyManager(UpdatesDispatcher updatesDispatcher, EnemyPositions enemyPositions, EnemyPool enemyPool, BulletSystem bulletSystem)
        {
            _updatesDispatcher = updatesDispatcher;
            _enemyPositions = enemyPositions;
            _enemyPool = enemyPool;
            _bulletSystem = bulletSystem;
        }
        
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
            
            var attackPosition = _enemyPositions.RandomAttackPosition();
            enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position);
            
            enemy.GetComponent<EnemyMoveAgent>().Initialize(_updatesDispatcher);
            enemy.GetComponent<EnemyAttackAgent>().Initialize(_updatesDispatcher);

            enemy.GetComponent<WeaponComponent>().Initialize(_bulletSystem);
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
    }
}