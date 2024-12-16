using HouseDefence.Grid;
using HouseDefence.Services;
using HouseDefence.Tower;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HouseDefence.UI
{
    public class TowerSelectionUI : MonoBehaviour
    {
        [SerializeField] private GameObject _towerSelectionUI;
        [SerializeField] private Button _addTowerPrefab;
        [SerializeField] private Button _removeTowerPrefab;

        private GridManager _gridManager;

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

        private void Start()
        {
            _gridManager = GameService.Instance.gridManager;  // Assuming GameService gives you the GridManager instance
        }

        private void AddTowerOnGrid()
        {
            if (_gridManager != null)
            {
                Vector3 towerPlacementPosition = _gridManager.GetSelectedCellPosition();  // Assuming you have this method to get the selected position on the grid
                _gridManager.PlaceTowerOnGrid(towerPlacementPosition);
            }
            else
            {
                Debug.LogError("GridManager not found!");
            }
        }

        private void RemoveTowerFromGrid()
        {
            
        }
    }
}

