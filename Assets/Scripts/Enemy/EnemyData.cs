using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HouseDefence.Enemy
{
    [CreateAssetMenu(fileName = "EnemySO", menuName = "Game/EnemyData", order = 0)]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] internal string enemyName;
        [SerializeField] internal float maxHealth;
        [SerializeField] internal float moveSpeed;
        [SerializeField] internal int damageToHouse;
        [SerializeField] internal float goldReward;
    }
}
