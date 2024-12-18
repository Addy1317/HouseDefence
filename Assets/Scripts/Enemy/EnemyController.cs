#region Summary
/// <summary>
/// EnemyController is a concrete implementation of the EnemyBase class, handling the movement, health management, and 
/// interactions of the enemy character in the game. It uses a NavMeshAgent for pathfinding towards a target house, 
/// applies damage when colliding with the house, and updates the health bar. The class also manages the initialization 
/// and reset of enemy health, as well as collision handling with the house object.
/// </summary>
#endregion
using TowerDefence.House;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace TowerDefence.Enemy
{
    public class EnemyController : EnemyBase
    {
        [Header("Components Reference")]
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Slider _healthBar;

        private Transform _houseTarget;

        protected override void Start()
        {
            base.Start();
            if (_navMeshAgent != null)
            {
                _navMeshAgent.speed = EnemySO.moveSpeed;
            }
        }

        private void Update()
        {
            if (_houseTarget != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, _houseTarget.position, 2f * Time.deltaTime);
            }
        }

        #region Enemy Extended Methods
        public void Initialize(Transform houseTarget)
        {
            Debug.Log("Enemy initialized and activated.");
            _houseTarget = houseTarget;
            if (_navMeshAgent != null)
            {
                _navMeshAgent.enabled = true;
                _navMeshAgent.SetDestination(_houseTarget.position);
            }

            ResetHealth();
            gameObject.SetActive(true);
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Collided with House");
            if (other.CompareTag("House"))
            {
                TowerController houseController = other.GetComponent<TowerController>();
                if (houseController != null)
                {
                    Debug.Log($"Enemy collided with the House. Damage to apply: {EnemySO.damageToHouse}");

                    houseController.TakeDamage(EnemySO.damageToHouse);
                }
            }
        }

        internal void ResetHealth()
        {
            ResetEnemyHealth();
            UpdateHealthBar();
        }
       
        protected override void UpdateHealthBar()
        {
            _healthBar.value = EnemyCurrentHealth / EnemySO.maxHealth;
        }
        #endregion
    }
}
