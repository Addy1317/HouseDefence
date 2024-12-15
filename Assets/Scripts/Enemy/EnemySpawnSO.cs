using HouseDefence.Enemy;
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
        public EnemyType type; // Type of the enemy (Fast, Slow, etc.)
        public GameObject prefab; // Corresponding prefab for the enemy type
    }

    [System.Serializable]
    public struct WaveData
    {
        public EnemyType type; // Enemy type for this wave
        public int count; // Number of enemies of this type
    }

    [CreateAssetMenu(fileName = "EnemySpawnSO", menuName = "Game/EnemySpawnSO", order = 0)]
    public class EnemySpawnSO : ScriptableObject
    {
        [Header("Global Spawn Settings")]
        public float spawnDelay; // Delay between each enemy spawn
        public float waveDelay; // Delay between each wave

        [Header("Enemy Definitions")]
        public EnemyDefinition[] enemyDefinitions; // Array of enemy types and prefabs

        [Header("Wave Configurations")]
        public WaveData[] waves; // Array defining the enemies for each wave
    }
}
