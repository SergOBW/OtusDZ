using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    public sealed class EnemyPool : MonoBehaviour
    {
        [Header("Spawn")]
        [SerializeField]
        private EnemyPositions enemyPositions;
        
        [SerializeField]
        private Transform worldTransform;

        [Header("Pool")]
        [SerializeField]
        private Transform container;

        [FormerlySerializedAs("prefab")] [SerializeField]
        private GameObject enemyPrefab;

        private readonly Queue<GameObject> enemyPool = new();
        
        private void Awake()
        {
            for (var i = 0; i < 7; i++)
            {
                var enemy = Instantiate(enemyPrefab, container);
                enemyPool.Enqueue(enemy);
            }
        }

        public GameObject SpawnEnemy()
        {
            if (!enemyPool.TryDequeue(out var enemy))
            {
                return null;
            }

            enemy.transform.SetParent(worldTransform);

            var spawnPosition = enemyPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;
            
            return enemy;
        }

        public void DeSpawnEnemy(GameObject enemy)
        {
            enemy.transform.SetParent(container);
            enemyPool.Enqueue(enemy);
        }
    }
}