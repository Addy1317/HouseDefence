using UnityEngine;

namespace HouseDefence.Tower
{
    public enum TowerType
    {
        FastTower,
        SlowTower
    }

    [CreateAssetMenu(fileName = "TowerSO", menuName = "Game/TowerData", order = 0)]
    public class TowerSO : ScriptableObject
    {
        [SerializeField] internal string towerName;
        [SerializeField] internal TowerType towerType;
        [SerializeField] internal float towerMaxHealth;
        [SerializeField] internal float towerRange;
        [SerializeField] internal float towerAttackSpeed;
        [SerializeField] internal int damageToEnemy;
        [SerializeField] internal int towerCost;
    }
}
