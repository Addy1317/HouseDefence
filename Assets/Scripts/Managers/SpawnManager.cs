using HouseDefence.ZombieEnemy;
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

        private void InitializePools()
        {
            _enemyPools = new Dictionary<EnemyType, GenericObjectPool<EnemyController>>();

            foreach (var definition in _enemySpawnSO.enemyDefinitions)
            {
                Transform poolParent = new GameObject($"{definition.enemyType}Pool").transform;
                poolParent.SetParent(_spawnPoint);

                _enemyPools[definition.enemyType] = new GenericObjectPool<EnemyController>(
                    definition.enemyPrefab.GetComponent<EnemyController>(), 10, poolParent
                );
            }
        }

        private IEnumerator SpawnWavesRoutine()
        {
            foreach (var wave in _enemySpawnSO.waveConfigurations)
            {
                _currentWave++;
                Debug.Log($"Starting Wave {_currentWave}");

                GameService.Instance.uiManager.UpdateWaveCount(_currentWave);

                List<EnemyWaveEntry> randomizedEntries = new List<EnemyWaveEntry>(wave.enemyWaveEntries);
                ShuffleList(randomizedEntries);

                foreach (var enemyWaveEntry in randomizedEntries)
                {
                    for (int i = 0; i < enemyWaveEntry.enemyCountInWave; i++)
                    {
                        var enemy = _enemyPools[enemyWaveEntry.enemyTypeInWave].Get();

                        if (enemy == null)
                        {
                            Debug.LogWarning($"Pool limit reached for {enemyWaveEntry.enemyTypeInWave}!");
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

        private void ShuffleList(List<EnemyWaveEntry> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int randomIndex = Random.Range(0, i + 1);
                var temp = list[i];
                list[i] = list[randomIndex];
                list[randomIndex] = temp;
            }
        }

        public void ReturnEnemyToPool(EnemyController enemyController, EnemyType enemyType)
        {
            if (_enemyPools.ContainsKey(enemyType))
            {
                enemyController.ResetHealth(); 
                enemyController.gameObject.SetActive(false); 
                _enemyPools[enemyType].ReturnToPool(enemyController);

                _activeEnemies--; 
                if (_activeEnemies < 0) _activeEnemies = 0; 
            }
            else
            {
                Debug.LogWarning($"Enemy type {enemyType} not found in pool.");
            }
        }
    }
} 
