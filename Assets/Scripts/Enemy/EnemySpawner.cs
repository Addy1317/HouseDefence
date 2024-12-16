using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HouseDefence.ZombieEnemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Spawn Attributes")]
        [SerializeField] private int _initialEnemyCount = 5;
        [SerializeField] private float _spawnDelay = 1.0f;
        [SerializeField] private float _waveDelay = 5.0f;

        [Header("Enemy References")]
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform houseTarget;
        [SerializeField] private GameObject _fastEnemyPrefab;
        [SerializeField] private GameObject _strongEnemyPrefab;
        private int currentWave = 0;

        private void Start()
        {
            StartCoroutine(SpawnWaves());   
        }
        public void StartWaves()
        {
            StartCoroutine(SpawnWaves());
        }

        private IEnumerator SpawnWaves()
        {
            while (true)
            {
                currentWave++;
                int enemyCount = _initialEnemyCount + currentWave * 2;

                Debug.Log($"Wave {currentWave} started with {enemyCount} enemies.");

                for (int i = 0; i < enemyCount; i++)
                {
                    GameObject enemyPrefab = (i % 2 == 0) ? _fastEnemyPrefab : _strongEnemyPrefab;
                    SpawnEnemy(enemyPrefab);
                    yield return new WaitForSeconds(_spawnDelay);
                }

                Debug.Log($"Wave {currentWave} completed. Waiting for the next wave.");
                yield return new WaitForSeconds(_waveDelay);
            }
        }

        private void SpawnEnemy(GameObject enemyPrefab)
        {
            GameObject enemyInstance = Instantiate(enemyPrefab, _spawnPoint.position, Quaternion.identity);
            EnemyController enemyController = enemyInstance.GetComponent<EnemyController>();

            if (enemyController != null)
            {
                enemyController.Initialize(houseTarget); // Assign target dynamically
            }
        }
    }
}
