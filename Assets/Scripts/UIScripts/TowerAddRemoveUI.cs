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
        [Header("Tower UI Attributes")]
        [SerializeField] private Button _addTowerButton;
        [SerializeField] private Button _removeTowerButton;
        [SerializeField] private TowerBase _towerPrefabToAdd;

        public object GameServie { get; private set; }

        private void OnEnable()
        {
            _addTowerButton.onClick.AddListener(AddTowerOnGrid);
            _removeTowerButton.onClick.AddListener(RemoveTowerFromGrid);
        }

        private void OnDisable()
        {
            _addTowerButton.onClick.RemoveListener(AddTowerOnGrid);
            _removeTowerButton.onClick.RemoveListener(RemoveTowerFromGrid);
        }

        #region Tower Add Remove Methods
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

        internal void SetTowerPrefab(TowerBase towerPrefab)
        {
            _towerPrefabToAdd = towerPrefab;
        }
        #endregion
    }
}

