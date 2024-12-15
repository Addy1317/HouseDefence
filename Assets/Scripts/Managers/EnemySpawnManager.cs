using HouseDefence.Enemy;
using HouseDefence.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace HouseDefence.EnemySpawn
{
    public class EnemySpawnManager : MonoBehaviour
    {
        [Header("Enemy References")]
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _houseTarget;

        [Header("Spawn Config")]
        [SerializeField] private EnemySpawnSO _enemySpawnSO;

        private Dictionary<EnemyType, GenericObjectPool<EnemyController>> _enemyPools;

        private void Start()
        {
            InitializePools();
            StartCoroutine(SpawnWaves());
        }

        private void InitializePools()
        {
            _enemyPools = new Dictionary<EnemyType, GenericObjectPool<EnemyController>>();

            foreach (var definition in _enemySpawnSO.enemyDefinitions)
            {
                if (!_enemyPools.ContainsKey(definition.type))
                {
                    _enemyPools[definition.type] = new GenericObjectPool<EnemyController>(
                        definition.prefab.GetComponent<EnemyController>(), 10
                    );
                }
            }
        }

        private IEnumerator SpawnWaves()
        {
            foreach (var wave in _enemySpawnSO.waves)
            {
                Debug.Log($"Spawning {wave.count} {wave.type} enemies");

                for (int i = 0; i < wave.count; i++)
                {
                    var enemy = _enemyPools[wave.type].Get();
                    enemy.transform.position = _spawnPoint.position;
                    enemy.Initialize(_houseTarget);

                    yield return new WaitForSeconds(_enemySpawnSO.spawnDelay);
                }

                yield return new WaitForSeconds(_enemySpawnSO.waveDelay);
            }

            Debug.Log("All waves completed!");
        }

        public void ReturnEnemyToPool(EnemyController enemy, EnemyType type)
        {
            if (_enemyPools.ContainsKey(type))
            {
                _enemyPools[type].ReturnToPool(enemy);
            }
            else
            {
                Debug.LogWarning($"Enemy type {type} not found in pool.");
            }
        }
    }
} 
