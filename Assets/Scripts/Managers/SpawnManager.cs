#region Summary
// The SpawnManager is responsible for managing the spawning of enemies during the game. 
// It handles enemy pool management, wave-based enemy spawning, and the return of enemies to the pool after they are defeated.
#endregion
using HouseDefence.Enemy;
using HouseDefence.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HouseDefence.EnemySpawn
{
    public class SpawnManager : MonoBehaviour
    {
        [Header("Enemy References")]
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _houseTarget;

        [Header("Spawn Config")]
        [SerializeField] private EnemySpawnSO _enemySpawnSO;

        private Dictionary<EnemyType, GenericObjectPool<EnemyController>> _enemyPools;
        private int _currentWave = 0;
        private int _activeEnemies = 0;

        private void Start()
        {
            InitializePools();
            StartCoroutine(SpawnWavesRoutine());
        }

        #region EnemySpawn Methods
        private void InitializePools()
        {
            {
                _enemyPools = new Dictionary<EnemyType, GenericObjectPool<EnemyController>>();

                foreach (var waveData in _enemySpawnSO.waveConfigurations)
                {
                    foreach (var definition in waveData.enemyDefinitions)
                    {
                        // Create pools for each enemy type
                        if (!_enemyPools.ContainsKey(definition.enemyType))
                        {
                            Transform poolParent = new GameObject($"{definition.enemyType}Pool").transform;
                            poolParent.SetParent(_spawnPoint);

                            _enemyPools[definition.enemyType] = new GenericObjectPool<EnemyController>(
                                definition.enemyPrefab.GetComponent<EnemyController>(), _enemySpawnSO.poolSize, poolParent
                            );
                        }
                    }
                }
            }
        }

        private IEnumerator SpawnWavesRoutine()
        {
            foreach (var wave in _enemySpawnSO.waveConfigurations)
            {
                _currentWave++;
                Debug.Log($"Starting Wave {_currentWave}");

                GameService.Instance.uiManager.UpdateWaveCount(_currentWave);

                List<EnemyDefinition> randomizedEnemies = new List<EnemyDefinition>(wave.enemyDefinitions);
                ShuffleList(randomizedEnemies);

                foreach (var enemyDefinition in randomizedEnemies)
                {
                    for (int i = 0; i < wave.totalEnemies; i++)
                    {
                        var enemy = _enemyPools[enemyDefinition.enemyType].Get();

                        if (enemy == null)
                        {
                            Debug.LogWarning($"Pool limit reached for {enemyDefinition.enemyType}!");
                            continue;
                        }

                        enemy.gameObject.SetActive(true);
                        enemy.transform.position = _spawnPoint.position;
                        enemy.Initialize(_houseTarget);

                        _activeEnemies++;

                        yield return new WaitForSeconds(_enemySpawnSO.spawnDelay);
                    }
                }

                yield return new WaitUntil(() => _activeEnemies == 0);

                Debug.Log($"Wave {_currentWave} completed!");

                yield return new WaitForSeconds(_enemySpawnSO.waveDelay);
            }

            Debug.Log("All waves completed!");
        }

        private void ShuffleList(List<EnemyDefinition> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int randomIndex = Random.Range(1, i + 2);
                var temp = list[i];
                list[i] = list[randomIndex];
                list[randomIndex] = temp;
            }
        }

        internal void ReturnEnemyToPool(EnemyController enemyController, EnemyType enemyType)
        {
            if (_enemyPools.ContainsKey(enemyType))
            {
                enemyController.ResetHealth(); 
                enemyController.gameObject.SetActive(false); 
                _enemyPools[enemyType].ReturnToPool(enemyController);

                _activeEnemies--; 
                if (_activeEnemies < 0) _activeEnemies = 0;
                Debug.Log($"Enemy returned to pool. Active Enemies: {_activeEnemies}");
            }
            else
            {
                Debug.LogWarning($"Enemy type {enemyType} not found in pool.");
            }
        }

        #endregion
    }
} 
