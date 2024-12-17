#region Summary
// The TowerManager is responsible for managing tower types, selecting towers, and placing them in the game world.
// It handles both manual tower placement and random tower creation based on available tower types.
#endregion
using UnityEngine;

namespace HouseDefence.Tower
{
    [System.Serializable]
    public struct TowerInfo
    {
        public TowerType towerType;
        public GameObject towerPrefab;
    }

    public class TowerManager : MonoBehaviour
    {
        [SerializeField] internal TowerInfo[] towerInfos; 
        private TowerInfo selectedTowerInfo;

        public void SelectTower(TowerType towerType)
        {
            selectedTowerInfo = GetTowerInfo(towerType);

            if (selectedTowerInfo.towerPrefab != null)
            {
                Debug.Log($"Selected tower: {selectedTowerInfo.towerPrefab.name}");
            }
            else
            {
                Debug.LogError("Selected tower prefab is missing.");
            }
        }

        public GameObject CreateRandomTower(Vector3 position)
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

        public void PlaceTower(Vector3 position)
        {
            if (selectedTowerInfo.towerPrefab != null)
            {
                GameObject towerObject = Instantiate(selectedTowerInfo.towerPrefab, position, Quaternion.identity);

                TowerController towerController = towerObject.GetComponent<TowerController>();

                if (towerController != null)
                {
                    towerController.towerSO = selectedTowerInfo.towerPrefab.GetComponent<TowerSO>(); 
                }

                Debug.Log($"Placed tower: {selectedTowerInfo.towerPrefab.name} at {position}");
            }
            else
            {
                Debug.LogError("No tower selected to place.");
            }
        }

        private TowerInfo GetTowerInfo(TowerType towerType)
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
    }
}
