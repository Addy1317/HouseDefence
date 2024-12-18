#region Summary
/// <summary>
/// EnemyBase serves as a base class for enemy characters in the game, handling health management, damage processing,
/// and death behavior. It includes functionality for taking damage, resetting health, and invoking death events.
/// Subclasses can implement specific health bar updates and additional enemy-specific behaviors.
/// </summary>
#endregion
using TowerDefence.Audio;
using TowerDefence.Services;
using UnityEngine;

namespace TowerDefence.Enemy
{
    public abstract class EnemyBase : MonoBehaviour
    {
        [Header("EnemySO Reference")]
        [SerializeField] private EnemySO enemySO; 
        public EnemySO EnemySO => enemySO; 
        public float EnemyCurrentHealth => currentHealth; 
        private float currentHealth;

        protected virtual void Start()
        {
            currentHealth = enemySO.maxHealth; 
        }

        #region EnemyBaseMethods
        internal virtual void EnemyTakeDamage(float damage)
        {
            currentHealth -= damage;
            UpdateHealthBar();
            if (currentHealth <= 0)
            {
                OnEnemyDeath();
            }
        }

        internal virtual void ResetEnemyHealth() 
        {
            currentHealth = enemySO.maxHealth;
        }

        protected virtual void OnEnemyDeath()
        {
            GameService.Instance.audioManager.PlaySFX(SFXType.OnEnemyGettingKilledSFX);
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
        #endregion
    }
}

