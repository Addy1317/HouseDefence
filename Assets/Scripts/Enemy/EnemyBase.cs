using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HouseDefence.Enemy
{
    public abstract class EnemyBase : MonoBehaviour
    {
        [SerializeField] private EnemySO enemySO; 
        public EnemySO EnemySO => enemySO; 
        public float EnemyCurrentHealth => currentHealth; 

        public delegate void OnEnemyDeath(float goldReward);
        public static event OnEnemyDeath EnemyDeathEvent;

        private float currentHealth;
        protected virtual void Start()
        {
            currentHealth = enemySO.maxHealth; 
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
        public void ResetEnemyHealth() // Added method to reset health
        {
            currentHealth = enemySO.maxHealth;
        }

        protected virtual void Die()
        {
            EnemyDeathEvent?.Invoke(enemySO.goldReward); 
            Destroy(gameObject); 
        }

        protected abstract void UpdateHealthBar(); 
    }
}

