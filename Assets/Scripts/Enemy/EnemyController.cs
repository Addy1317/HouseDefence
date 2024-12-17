using HouseDefence.House;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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
            _houseTarget = houseTarget;
            if (_navMeshAgent != null)
            {
                _navMeshAgent.enabled = true;
                _navMeshAgent.SetDestination(_houseTarget.position);
            }

            ResetHealth();
            gameObject.SetActive(true);
        }

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

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Collided with House");
            if (other.CompareTag("House"))
            {
                HouseController houseController = other.GetComponent<HouseController>();
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
    }
}
