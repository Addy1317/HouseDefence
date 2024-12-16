using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HouseDefence.Tower
{
    public class TowerManager : MonoBehaviour
    {
        [Header("Tower Prefabs")]
        [SerializeField] private GameObject fastTowerPrefab;
        [SerializeField] private GameObject slowTowerPrefab;
        private TowerSO selectedTowerSO;

        // Call this method to select a tower (e.g., from a UI button or other interaction)
        public void SelectTower(TowerSO towerSO)
        {
            selectedTowerSO = towerSO;
            Debug.Log($"Selected tower: {selectedTowerSO.towerName}");
        }

        // This method will instantiate the correct tower prefab based on selectedTowerSO
        public TowerController CreateTower(Vector3 position)
        {
            if (selectedTowerSO == null)
            {
                Debug.LogError("No tower selected.");
                return null;
            }

            GameObject towerPrefab = null;

            // Determine the correct prefab based on the tower type
            switch (selectedTowerSO.towerType)
            {
                case TowerType.FastTower:
                    towerPrefab = fastTowerPrefab;
                    break;
                case TowerType.SlowTower:
                    towerPrefab = slowTowerPrefab;
                    break;
                default:
                    Debug.LogError("Unknown tower type");
                    return null;
            }

            // Instantiate the tower prefab
            if (towerPrefab != null)
            {
                GameObject towerObject = Instantiate(towerPrefab, position, Quaternion.identity);
                TowerController towerController = towerObject.GetComponent<TowerController>();

                // Ensure the correct TowerSO data is assigned to the tower controller
                if (towerController != null)
                {
                    towerController.towerSO = selectedTowerSO;
                }

                return towerController;
            }

            return null;
        }

        // This method is called when the player clicks to place the tower
        public void PlaceTower(Vector3 position)
        {
            if (selectedTowerSO != null)
            {
                CreateTower(position);
            }
            else
            {
                Debug.LogError("No tower selected.");
            }
        }
    }
}
