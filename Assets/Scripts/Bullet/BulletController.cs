using HouseDefence.Services;
using HouseDefence.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HouseDefence.Bullet
{
    public class BulletController : MonoBehaviour
    {
        private EnemyController _targetEnemy;
        private float _damage;
        private float speed = 5f;  

        public void Initialize(EnemyController enemy, float bulletDamage)
        {
            _targetEnemy = enemy;
            _damage = bulletDamage;
        }

        private void Update()
        {
            BulletBehaviour();
        }

        private void BulletBehaviour()
        {
            if (_targetEnemy != null)
            {
                Vector3 direction = (_targetEnemy.transform.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;

                if (Vector3.Distance(transform.position, _targetEnemy.transform.position) < 0.5f)
                {
                    _targetEnemy.EnemyTakeDamage(_damage);
                    ReturnToPool();
                    //_targetEnemy.DestroyEnemy();

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
