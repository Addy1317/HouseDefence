#region Summary
/// <summary>
/// BulletController manages the behavior of bullets in the game. It handles the movement of bullets towards a target enemy, 
/// applies damage when the bullet reaches the enemy, and returns the bullet to the object pool once it has hit its target or 
/// if the target is no longer valid.
/// </summary>
#endregion
using TowerDefence.Services;
using TowerDefence.Enemy;
using UnityEngine;

namespace TowerDefence.Bullet
{
    public class BulletController : MonoBehaviour
    {
        [Header("Bullets Attributes")]
        private EnemyController _targetEnemy;
        private float _damage;
        private float _speed = 5f;

        private void Update()
        {
            BulletBehaviour();
        }

        #region Bullets Methods
        internal void Initialize(EnemyController enemy, float bulletDamage)
        {
            _targetEnemy = enemy;
            _damage = bulletDamage;
        }

        private void BulletBehaviour()
        {
            if (_targetEnemy != null)
            {
                Vector3 direction = (_targetEnemy.transform.position - transform.position).normalized;
                transform.position += direction * _speed * Time.deltaTime;

                if (Vector3.Distance(transform.position, _targetEnemy.transform.position) < 0.5f)
                {
                    _targetEnemy.EnemyTakeDamage(_damage);
                    ReturnToPool();
                }
            }
            else
            {
                ReturnToPool();
            }
        }

        private void ReturnToPool()
        {
            GameService.Instance.bulletManager.ReturnToPool(this);
        }

        #endregion
    }
}
