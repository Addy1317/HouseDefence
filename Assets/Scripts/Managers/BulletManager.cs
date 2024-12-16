using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HouseDefence.Bullet
{
    public class BulletManager : MonoBehaviour
    {
        [SerializeField] private BulletController _bulletPrefab; // Reference to the bullet prefab
        [SerializeField] private int _initialPoolSize = 50; // Initial pool size
        private GenericObjectPool<BulletController> _bulletPool;

        private void Awake()
        {
            _bulletPool = new GenericObjectPool<BulletController>(_bulletPrefab, _initialPoolSize, transform);
        }

        public BulletController GetBullet()
        {
            return _bulletPool.Get();
        }

        public void ReturnToPool(BulletController bullet)
        {
            _bulletPool.ReturnToPool(bullet);
        }
    }
    
}
