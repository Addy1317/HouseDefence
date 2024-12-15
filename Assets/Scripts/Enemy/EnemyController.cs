using System.Collections;
using System.Collections.Generic;
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
            _houseTarget = houseTarget; // Set the target
            if (_navMeshAgent != null)
            {
                _navMeshAgent.SetDestination(_houseTarget.position); // Assign NavMeshAgent destination
            }
        }

        protected override void Start()
        {
            base.Start();
            if (_navMeshAgent != null)
            {
                _navMeshAgent.speed = EnemyData.moveSpeed;
            }
        }

        protected override void UpdateHealthBar()
        {
            _healthBar.value = CurrentHealth / EnemyData.maxHealth;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform == _houseTarget)
            {
                //GameManager.Instance.ReduceHealth(EnemyData.damageToHouse); // Reduce player health
                Destroy(gameObject); // Destroy enemy
            }
        }
    }
}
