using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] internal TowerInfo[] towerInfos; // Store different types of towers
        private TowerInfo selectedTowerInfo;

        // Call this method to select a tower (e.g., from a UI button or other interaction)
        public void SelectTower(TowerType towerType)
        {
            // Find the tower in the towerInfos array based on the selected TowerType
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

        // This method will instantiate a random tower from the available towers
        public GameObject CreateRandomTower(Vector3 position)
        {
            if (towerInfos.Length == 0)
            {
                Debug.LogError("No tower types available.");
                return null;
            }

            // Randomly select a tower from the available tower types
            int randomIndex = Random.Range(0, towerInfos.Length);
            TowerInfo towerInfo = towerInfos[randomIndex];

            if (towerInfo.towerPrefab == null)
            {
                Debug.LogError("Prefab not found for selected tower.");
                return null;
            }

            // Instantiate the tower prefab
            GameObject towerObject = Instantiate(towerInfo.towerPrefab, position, Quaternion.identity);
            TowerController towerController = towerObject.GetComponent<TowerController>();

            if (towerController != null)
            {
                // Optionally initialize or configure the tower with the corresponding data
                towerController.towerSO = towerInfo.towerPrefab.GetComponent<TowerSO>(); // If needed, assign towerSO data.
            }

            return towerObject;
        }

        // Method to place the tower in the game world
        public void PlaceTower(Vector3 position)
        {
            if (selectedTowerInfo.towerPrefab != null)
            {
                // Instantiate the selected tower prefab at the specified position
                GameObject towerObject = Instantiate(selectedTowerInfo.towerPrefab, position, Quaternion.identity);

                TowerController towerController = towerObject.GetComponent<TowerController>();

                if (towerController != null)
                {
                    // Optionally, initialize or configure the tower with additional data, if needed
                    towerController.towerSO = selectedTowerInfo.towerPrefab.GetComponent<TowerSO>(); // If you need to use TowerSO data.
                }

                Debug.Log($"Placed tower: {selectedTowerInfo.towerPrefab.name} at {position}");
            }
            else
            {
                Debug.LogError("No tower selected to place.");
            }
        }

        // Fetch TowerInfo for a given tower type
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
