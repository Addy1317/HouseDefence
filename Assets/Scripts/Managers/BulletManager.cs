#region Summary
/// <summary>
/// Manages the bullet pool for efficient instantiation and reuse of bullets in the game.
/// Utilizes a generic object pool to minimize performance overhead caused by frequent instantiation.
/// </summary>
#endregion
using UnityEngine;

namespace HouseDefence.Bullet
{
    public class BulletManager : MonoBehaviour
    {
        [Header("Components Reference")]
        [SerializeField] private BulletController _bulletPrefab; 
        [SerializeField] private int _initialPoolSize = 50; 
        private GenericObjectPool<BulletController> _bulletPool;

        private void Awake()
        {
            _bulletPool = new GenericObjectPool<BulletController>(_bulletPrefab, _initialPoolSize, transform);
        }

        internal BulletController GetBullet()
        {
            return _bulletPool.Get();
        }

       internal void ReturnToPool(BulletController bullet)
        {
            _bulletPool.ReturnToPool(bullet);
        }
    }
    
}
