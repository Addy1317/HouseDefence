#region Summary
// TowerBase is an abstract class for tower behavior in a tower defense game, handling the tower's health, attack, and targeting mechanics.
// It manages the tower's health, detects enemies within range, and fires bullets at the enemies with a cooldown between attacks.
// The class also manages bullet pooling and provides functionality for rotating the tower to face its target. 
// Derived classes must implement health bar updates and may override death behavior (e.g., destruction or animation).
#endregion
using HouseDefence.Bullet;
using HouseDefence.Enemy;
using UnityEngine;
using HouseDefence.Services;

namespace HouseDefence.Tower
{
    public abstract class TowerBase : MonoBehaviour
    {
        [SerializeField] internal TowerSO towerSO;
        [SerializeField] private Transform towerHead;
        public TowerSO TowerSO => towerSO;
        private float towerCurrentHealth;
        public float TowerCurrentHealth => towerCurrentHealth;

        [SerializeField] private GameObject bulletPrefab; 
        private GenericObjectPool<BulletController> bulletPool;

        private float attackCooldown;
        private float lastAttackTime;

        protected virtual void Start()
        {
            if (towerSO != null)
            {
                towerCurrentHealth = towerSO.towerMaxHealth;
            }
            else
            {
                Debug.LogError("TowerSO not found on " + gameObject.name);
            }

            bulletPool = new GenericObjectPool<BulletController>(bulletPrefab.GetComponent<BulletController>(), 10, transform);
            attackCooldown = 1 / towerSO.towerAttackSpeed;  
        }
        protected void Update()
        {
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                EnemyController targetEnemy = FindEnemyInRange();
                if (targetEnemy != null)
                {
                    RotateTowerTowards(targetEnemy);
                    Shoot(targetEnemy);
                }
            }
        }

        public void TowerTakeDamage(float damage)
        {
            towerCurrentHealth -= damage;
            towerCurrentHealth = Mathf.Max(towerCurrentHealth, 0);
            UpdateTowerHealthBar();

            if (towerCurrentHealth <= 0)
            {
                Die();
            }
        }

        protected abstract void UpdateTowerHealthBar();

        protected virtual void Die()
        {
            Destroy(gameObject);
        }

        private EnemyController FindEnemyInRange()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, towerSO.towerRange);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Enemy"))  
                {
                    return hitCollider.GetComponent<EnemyController>();
                }
            }
            return null;
        }

        private void RotateTowerTowards(EnemyController targetEnemy)
        {
            Vector3 directionToEnemy = targetEnemy.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(directionToEnemy);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, towerSO.towerAttackSpeed * Time.deltaTime);
        }

        private void Shoot(EnemyController targetEnemy)
        {
            BulletController bullet = GameService.Instance.bulletManager.GetBullet(); 
            bullet.transform.position = towerHead.position; 
            bullet.Initialize(targetEnemy, towerSO.damageToEnemy);  
            lastAttackTime = Time.time;  
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, towerSO.towerRange);
        }
    }
}
