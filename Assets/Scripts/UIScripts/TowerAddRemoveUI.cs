#region Summary
// The TowerAddRemoveUI class handles the UI for adding and removing towers from the grid. 
// It provides functionality for selecting a tower to place on the grid and removing towers from the grid.
// The UI interacts with GameService and GridManager to place and remove towers accordingly. 
// The SetTowerPrefab method allows setting a specific tower prefab to be added to the grid.
#endregion
using HouseDefence.Services;
using HouseDefence.Tower;
using UnityEngine;
using UnityEngine.UI;

namespace HouseDefence.UI
{
    public class TowerAddRemoveUI : MonoBehaviour
    {
        [SerializeField] private GameObject _towerSelectionUI;
        [SerializeField] private Button _addTowerPrefab;
        [SerializeField] private Button _removeTowerPrefab;
        [SerializeField] private TowerBase _towerPrefabToAdd;

        public object GameServie { get; private set; }

        private void OnEnable()
        {
            _addTowerPrefab.onClick.AddListener(AddTowerOnGrid);
            _removeTowerPrefab.onClick.AddListener(RemoveTowerFromGrid);
        }

        private void OnDisable()
        {
            _addTowerPrefab.onClick.RemoveListener(AddTowerOnGrid);
            _removeTowerPrefab.onClick.RemoveListener(RemoveTowerFromGrid);
        }

        private void AddTowerOnGrid()
        {
            if (_towerPrefabToAdd != null)
            {
                GameService.Instance.gridManager.PlaceTowerAtCell(_towerPrefabToAdd.gameObject);
            }
            else
            {
                Debug.LogError("No tower selected for placement!");
            }
        }

        private void RemoveTowerFromGrid()
        {
            GameService.Instance.gridManager.RemoveTowerAtCell();
        }

        public void SetTowerPrefab(TowerBase towerPrefab)
        {
            _towerPrefabToAdd = towerPrefab;
        }
    }
}

