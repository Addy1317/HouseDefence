using HouseDefence.Services;
using HouseDefence.ZombieEnemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HouseDefence.Bullet
{
    public class BulletController : MonoBehaviour
    {
        private EnemyController _targetEnemy;
        private float _damage;
        private float speed = 10f;  // Bullet speed

        public void Initialize(EnemyController enemy, float bulletDamage)
        {
            _targetEnemy = enemy;
            _damage = bulletDamage;
        }

        void Update()
        {
            if (_targetEnemy != null)
            {
                // Move bullet towards enemy
                Vector3 direction = (_targetEnemy.transform.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;

                // Check for collision with enemy
                if (Vector3.Distance(transform.position, _targetEnemy.transform.position) < 0.5f)
                {
                    _targetEnemy.EnemyTakeDamage(_damage); 
                    ReturnToPool();
                }
            }
            else
            {
                // If no target enemy, return to the pool
                ReturnToPool();
            }
        }

        private void ReturnToPool()
        {
            GameService.Instance.bulletManager.ReturnToPool(this);
        }
    }
}
