using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HouseDefence.Enemy
{
    public abstract class EnemyBase : MonoBehaviour
    {
        [SerializeField] private EnemyData _enemyData; 
        public EnemyData EnemyData => _enemyData; 
        public float CurrentHealth => currentHealth; 

        public delegate void OnEnemyDeath(float goldReward);
        public static event OnEnemyDeath EnemyDeathEvent;

        private float currentHealth;
        protected virtual void Start()
        {
            currentHealth = _enemyData.maxHealth; 
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            UpdateHealthBar();
            if (currentHealth <= 0)
            {
                Die();
            }
        }

        protected virtual void Die()
        {
            EnemyDeathEvent?.Invoke(_enemyData.goldReward); 
            Destroy(gameObject); 
        }

        protected abstract void UpdateHealthBar(); 
    }
}

