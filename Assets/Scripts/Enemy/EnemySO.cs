using HouseDefence.EnemySpawn;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HouseDefence.ZombieEnemy
{
    [CreateAssetMenu(fileName = "EnemySO", menuName = "Game/EnemyData", order = 0)]
    public class EnemySO : ScriptableObject
    {
        [SerializeField] internal string enemyName;
        [SerializeField] internal EnemyType enemyType;
        [SerializeField] internal float maxHealth;
        [SerializeField] internal float moveSpeed;
        [SerializeField] internal int damageToHouse;
        [SerializeField] internal float goldReward;
    }
}
