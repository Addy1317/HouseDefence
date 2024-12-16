using HouseDefence.ZombieEnemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HouseDefence.EnemySpawn
{
    public enum EnemyType
    {
        FastEnemy,
        SlowEnemy
    }

    [System.Serializable]
    public struct EnemyDefinition
    {
        public EnemyType enemyType; 
        public GameObject enemyPrefab; 
    }

    [System.Serializable]
    public struct EnemyWaveEntry
    {
        public EnemyType enemyTypeInWave; 
        public int enemyCountInWave; 
    }

    [System.Serializable]
    public struct WaveData
    {
        public string waveName; 
        public List<EnemyWaveEntry> enemyWaveEntries; 
    }

    [CreateAssetMenu(fileName = "EnemySpawnSO", menuName = "Game/EnemySpawnSO", order = 0)]
    public class EnemySpawnSO : ScriptableObject
    {
        [Header("Global Spawn Settings")]
        public float spawnDelay = 1.0f; 
        public float waveDelay = 5.0f; 

        [Header("Enemy Definitions")]
        public List<EnemyDefinition> enemyDefinitions; 

        [Header("Wave Configurations")]
        public List<WaveData> waveConfigurations; 

        private void OnValidate()
        {
            foreach (var wave in waveConfigurations)
            {
                int totalEnemies = 0;

                foreach (var entry in wave.enemyWaveEntries)
                {
                    if (!EnemyTypeExists(entry.enemyTypeInWave))
                    {
                        Debug.LogWarning($"Enemy type {entry.enemyTypeInWave} in wave {wave.waveName} is not defined in Enemy Definitions!");
                    }

                    totalEnemies += entry.enemyCountInWave;
                }

                Debug.Log($"Wave {wave.waveName} has {totalEnemies} enemies configured.");
            }
        }

        private bool EnemyTypeExists(EnemyType type)
        {
            return enemyDefinitions.Exists(def => def.enemyType == type);
        }
    }
}
