#region Summary
/// <summary>
/// EnemySpawnSO is a ScriptableObject that stores global spawn settings and configurations for enemy waves in the game. 
/// It includes settings such as spawn delays, wave delays, and the number of enemies to spawn in each wave. The class 
/// also holds a list of wave configurations, where each wave contains various enemy types and their corresponding prefabs. 
/// It validates enemy definitions and ensures that each wave has the correct configuration for enemy types and counts.
/// </summary>
#endregion
using System;
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
    public struct WaveData
    {
        public string waveName;
        public int totalEnemies;
        public EnemyDefinition[] enemyDefinitions;
    }

    [CreateAssetMenu(fileName = "EnemySpawnSO", menuName = "Game/EnemySpawnSO", order = 0)]
    public class EnemySpawnSO : ScriptableObject
    {
        [Header("Global Spawn Settings")]
        public float spawnDelay = 1.0f; 
        public float waveDelay = 5.0f; 

        [Header("Wave Configurations")]
        public int poolSize = 10;
        public List<WaveData> waveConfigurations;

        private void OnValidate()
        {
            // Validate the total number of enemies and check for missing enemy types
            foreach (var wave in waveConfigurations)
            {
                int totalEnemies = 0;

                // Check each enemy definition in the wave
                foreach (var definition in wave.enemyDefinitions)
                {
                    if (!EnemyTypeExists(definition.enemyType))
                    {
                        Debug.LogWarning($"Enemy type {definition.enemyType} in wave {wave.waveName} is not defined in Enemy Definitions!");
                    }

                    totalEnemies += wave.totalEnemies;  // Assuming the totalEnemies value is the correct number of enemies in the wave
                }

                Debug.Log($"Wave {wave.waveName} has {totalEnemies} enemies configured.");
            }
        }

        private bool EnemyTypeExists(EnemyType type)
        {
            return waveConfigurations.Exists(wave =>
                    Array.Exists(wave.enemyDefinitions, def => def.enemyType == type)
                );
        }
    }
}
