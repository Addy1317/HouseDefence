using HouseDefence.Bullet;
using HouseDefence.ZombieEnemy;
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

        // Bullet Object Pool
        [SerializeField] private GameObject bulletPrefab; // Bullet prefab (assigned in the inspector)
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
            // Check for enemies in range and attack
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
                if (hitCollider.CompareTag("Enemy"))  // Assuming enemy objects have the "Enemy" tag
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
            BulletController bullet = GameService.Instance.bulletManager.GetBullet(); // Get bullet from the pool
            bullet.transform.position = towerHead.position; // Spawn bullet at the tower
            bullet.Initialize(targetEnemy, towerSO.damageToEnemy);  // Initialize bullet with target enemy and damage
            lastAttackTime = Time.time;  // Reset attack cooldown timer
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, towerSO.towerRange);
        }
    }
}
