#region Summary
// TowerSO is a ScriptableObject that stores data related to different types of towers in the game.
// It includes properties such as tower name, type, max health, attack range, attack speed, damage to enemies, and tower cost.
// This data is used to configure towers dynamically in the game.
#endregion
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
        [Header("Tower Attributes")]
        [SerializeField] internal string towerName;
        [SerializeField] internal TowerType towerType;
        [SerializeField] internal float towerMaxHealth;
        [SerializeField] internal float towerRange;
        [SerializeField] internal float towerAttackSpeed;
        [SerializeField] internal int damageToEnemy;
        [SerializeField] internal int towerCost;
    }
}
