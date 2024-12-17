#region Summary
/// <summary>
/// EnemyBase serves as a base class for enemy characters in the game, handling health management, damage processing,
/// and death behavior. It includes functionality for taking damage, resetting health, and invoking death events.
/// Subclasses can implement specific health bar updates and additional enemy-specific behaviors.
/// </summary>
#endregion
using HouseDefence.Services;
using UnityEngine;

namespace HouseDefence.Enemy
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

        public virtual void EnemyTakeDamage(float damage)
        {
            currentHealth -= damage;
            UpdateHealthBar();
            if (currentHealth <= 0)
            {
                OnEnemyDeath();
            }
        }

        public virtual void ResetEnemyHealth() 
        {
            currentHealth = enemySO.maxHealth;
        }

        protected virtual void OnEnemyDeath()
        {
            GameService.Instance.eventManager.OnEnemyDeathEvent.InvokeEvents(enemySO.goldReward);

            EnemyController enemyController = this as EnemyController;

            if (enemyController != null)
            {
                gameObject.SetActive(false);
                GameService.Instance.enemySpawnManager.ReturnEnemyToPool(enemyController, enemySO.enemyType);
            }
            else
            {
                Debug.LogError("Failed to cast EnemyBase to EnemyController");
            }
        }

        protected abstract void UpdateHealthBar(); 
    }
}

