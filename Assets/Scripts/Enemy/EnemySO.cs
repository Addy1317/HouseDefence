#region Summary
/// <summary>
/// EnemySO is a ScriptableObject that holds data for enemy characters in the game. It stores attributes such as the enemy's 
/// name, type, health, movement speed, attack interval, damage to the house, and gold reward. This object is used to define 
/// the characteristics of different enemy types in a modular and data-driven way.
/// </summary>
#endregion
using HouseDefence.EnemySpawn;
using UnityEngine;

namespace HouseDefence.Enemy
{
    [CreateAssetMenu(fileName = "EnemySO", menuName = "Game/EnemyData", order = 0)]
    public class EnemySO : ScriptableObject
    {
        [Header("EnemySo Attributes")]
        [SerializeField] internal string enemyName;
        [SerializeField] internal EnemyType enemyType;
        [SerializeField] internal float maxHealth;
        [SerializeField] internal float moveSpeed;
        [SerializeField] internal float attackInterval;
        [SerializeField] internal int damageToHouse;
        [SerializeField] internal int goldReward;
    }
}
