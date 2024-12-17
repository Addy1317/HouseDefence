#region Summary
// GridManager is responsible for managing the game grid where towers are placed in the HouseDefence game.
// It interacts with the GridController to determine grid size and placement rules for towers. 
// The class provides functions to place, remove, and check tower placement in a grid-based layout.
#endregion
using HouseDefence.Tower;
using UnityEngine;

namespace HouseDefence.Grid
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private GridController _gridController;
        [SerializeField] private Material _invalidCellMaterial;

        private TowerBase[,] _placedTowers;
       
        private void Start()
        {
            int rows = Mathf.FloorToInt((_gridController._planeHeight - 2 * _gridController._gridMarginY) / _gridController._gridCellSize);
            int cols = Mathf.FloorToInt((_gridController._planeWidth - 2 * _gridController._gridMarginX) / _gridController._gridCellSize);

            _placedTowers = new TowerBase[cols, rows]; 
        }

        #region Tower Management
        public void PlaceTowerAtCell(GameObject towerPrefab)
        {
            Vector3 selectedPosition = _gridController.GetSelectedCellPosition();
            int x = Mathf.FloorToInt(selectedPosition.x / _gridController._gridCellSize);
            int z = Mathf.FloorToInt(selectedPosition.z / _gridController._gridCellSize);

            if (_placedTowers[x, z] == null) 
            {
                GameObject tower = Instantiate(towerPrefab, selectedPosition, Quaternion.identity);
                _placedTowers[x, z] = tower.GetComponent<TowerBase>();

                Debug.Log("Tower placed at: " + selectedPosition);
            }
            else
            {
                _gridController._gridCells[x, z].GetComponent<Renderer>().material = _invalidCellMaterial;
                Debug.LogError("Cannot place tower here. Cell is occupied.");
            }
        }

        public void RemoveTowerAtCell()
        {
            Vector3 selectedPosition = _gridController.GetSelectedCellPosition();
            int x = Mathf.FloorToInt(selectedPosition.x / _gridController._gridCellSize);
            int z = Mathf.FloorToInt(selectedPosition.z / _gridController._gridCellSize);

            if (_placedTowers[x, z] != null) 
            {
                Destroy(_placedTowers[x, z].gameObject);
                _placedTowers[x, z] = null;

                Debug.Log("Tower removed from: " + selectedPosition);
            }
            else
            {
                Debug.LogError("No tower present at this cell.");
            }
        }

        public bool IsCellOccupied(Vector3 cellPosition)
        {
            int x = Mathf.FloorToInt(cellPosition.x / _gridController._gridCellSize);
            int z = Mathf.FloorToInt(cellPosition.z / _gridController._gridCellSize);
            return _placedTowers[x, z] != null;
        }
        #endregion
    }
}

