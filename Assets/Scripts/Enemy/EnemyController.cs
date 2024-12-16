using HouseDefence.EnemySpawn;
using HouseDefence.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

namespace HouseDefence.Enemy
{
    public class EnemyController : EnemyBase
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Slider _healthBar;

        private Transform _houseTarget;

        public void Initialize(Transform houseTarget)
        {
            Debug.Log("Enemy initialized and activated.");
            _houseTarget = houseTarget; // Set the target
            if (_navMeshAgent != null)
            {
                _navMeshAgent.enabled = true; // Reactivate the NavMeshAgent
                _navMeshAgent.SetDestination(_houseTarget.position); // Assign NavMeshAgent destination
            }

            ResetHealth(); // Reset health for reused enemies
            gameObject.SetActive(true); // Reactivate the enemy
        }

        protected override void Start()
        {
            base.Start();
            if (_navMeshAgent != null)
            {
                _navMeshAgent.speed = EnemySO.moveSpeed; // Use moveSpeed from EnemySO
            }
        }
        private void Update()
        {
            if (_houseTarget != null)
            {
                // Move the enemy towards the target
                transform.position = Vector3.MoveTowards(transform.position, _houseTarget.position, 2f * Time.deltaTime);
            }
        }


        protected override void UpdateHealthBar()
        {
            _healthBar.value = EnemyCurrentHealth / EnemySO.maxHealth; // Use maxHealth from EnemySO
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform == _houseTarget)
            {
                // Reduce player health through GameManager
                // GameManager.Instance.ReduceHealth(EnemySO.damageToHouse);

                DestroyEnemy(); // Handle destruction or pooling
            }
        }

        private void DestroyEnemy()
        {
            gameObject.SetActive(false); // Return to pool
            GameService.Instance.enemySpawnManager.ReturnEnemyToPool(this, EnemySO.enemyType); // Use enemyType from EnemySO
        }

        internal void ResetHealth()
        {
            ResetEnemyHealth();
            UpdateHealthBar(); // Update UI
        }
    }
}
