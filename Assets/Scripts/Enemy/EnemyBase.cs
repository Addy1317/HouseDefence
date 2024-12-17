using HouseDefence.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HouseDefence.ZombieEnemy
{
    public abstract class EnemyBase : MonoBehaviour
    {
        [SerializeField] private EnemySO enemySO; 
        public EnemySO EnemySO => enemySO; 
        public float EnemyCurrentHealth => currentHealth; 

        private float currentHealth;

        protected virtual void Start()
        {
            currentHealth = enemySO.maxHealth; 
        }

        public void EnemyTakeDamage(float damage)
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
            GameService.Instance.eventManager.OnEnemyDeathEvent.InvokeEvents(enemySO.goldReward);
            Destroy(gameObject); 
        }

        protected abstract void UpdateHealthBar(); 
    }
}

