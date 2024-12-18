#region Summary
// The TowerManager is responsible for managing tower types, selecting towers, and placing them in the game world.
// It handles both manual tower placement and random tower creation based on available tower types.
#endregion
using UnityEngine;

namespace TowerDefence.Tower
{
    [System.Serializable]
    public struct TowerInfo
    {
        public TowerType towerType;
        public GameObject towerPrefab;
    }

    public class TowerManager : MonoBehaviour
    {
        [Header("Tower Data")]
        [SerializeField] internal TowerInfo[] towerInfos; 
        private TowerInfo _selectedTowerInfo;

        #region Tower Methods
        internal void SelectTower(TowerType towerType)
        {
            _selectedTowerInfo = GetTowerInfo(towerType);

            if (_selectedTowerInfo.towerPrefab != null)
            {
                Debug.Log($"Selected tower: {_selectedTowerInfo.towerPrefab.name}");
            }
            else
            {
                Debug.LogError("Selected tower prefab is missing.");
            }
        }

        internal GameObject CreateRandomTower(Vector3 position)
        {
            if (towerInfos.Length == 0)
            {
                Debug.LogError("No tower types available.");
                return null;
            }

            int randomIndex = Random.Range(0, towerInfos.Length);
            TowerInfo towerInfo = towerInfos[randomIndex];

            if (towerInfo.towerPrefab == null)
            {
                Debug.LogError("Prefab not found for selected tower.");
                return null;
            }

            GameObject towerObject = Instantiate(towerInfo.towerPrefab, position, Quaternion.identity);
            TowerController towerController = towerObject.GetComponent<TowerController>();

            if (towerController != null)
            {
                towerController.towerSO = towerInfo.towerPrefab.GetComponent<TowerSO>(); 
            }

            return towerObject;
        }

        internal void PlaceTower(Vector3 position)
        {
            if (_selectedTowerInfo.towerPrefab != null)
            {
                GameObject towerObject = Instantiate(_selectedTowerInfo.towerPrefab, position, Quaternion.identity);

                TowerController towerController = towerObject.GetComponent<TowerController>();

                if (towerController != null)
                {
                    towerController.towerSO = _selectedTowerInfo.towerPrefab.GetComponent<TowerSO>(); 
                }

                Debug.Log($"Placed tower: {_selectedTowerInfo.towerPrefab.name} at {position}");
            }
            else
            {
                Debug.LogError("No tower selected to place.");
            }
        }

        internal TowerInfo GetTowerInfo(TowerType towerType)
        {
            foreach (var towerInfo in towerInfos)
            {
                if (towerInfo.towerType == towerType)
                {
                    return towerInfo;
                }
            }

            Debug.LogError($"Tower type {towerType} not found.");
            return default;
        }

        #endregion
    }
}
