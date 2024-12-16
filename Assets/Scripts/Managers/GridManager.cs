using HouseDefence.Services;
using HouseDefence.Tower;
using System.Collections.Generic;
using UnityEngine;

namespace HouseDefence.Grid
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private float _planeWidth = 10f;  
        [SerializeField] private float _planeHeight = 10f; 
        [SerializeField] private float _gridCellSize = 1f; 

        [SerializeField] private float _gridMarginX = 3f; 
        [SerializeField] private float _gridMarginY = 3f; 

        [Header("Highlight Settings")]
        [SerializeField] private Material _defaultCellMaterial;
        [SerializeField] private Material _highlightedCellMaterial;

        private GameObject[,] _gridCells;
        private GameObject _selectedCell;
        private TowerBase[,] _placedTowers;
        private Vector3 _selectedCellPosition;

        private Dictionary<Vector3, GameObject> _towerPositions = new Dictionary<Vector3, GameObject>();

        private void Start()
        {
            InitializeGrid();
        }

        private void Update()
        {
            HandleCellSelection();
        }

        private void InitializeGrid()
        {
            int rows = Mathf.FloorToInt((_planeHeight - 2 * _gridMarginY) / _gridCellSize);
            int cols = Mathf.FloorToInt((_planeWidth - 2 * _gridMarginX) / _gridCellSize);

            _gridCells = new GameObject[cols, rows];
            float startX = transform.position.x - (_planeWidth - 2 * _gridMarginX) / 2;
            float startZ = transform.position.z - (_planeHeight - 2 * _gridMarginY) / 2;

            for (int x = 0; x < cols; x++)
            {
                for (int z = 0; z < rows; z++)
                {
                    Vector3 cellPosition = new Vector3(
                        startX + x * _gridCellSize + _gridCellSize / 2,
                        transform.position.y,
                        startZ + z * _gridCellSize + _gridCellSize / 2
                    );

                    GameObject cell = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cell.transform.position = cellPosition;
                    cell.transform.localScale = new Vector3(_gridCellSize, 0.1f, _gridCellSize);
                    cell.GetComponent<Renderer>().material = _defaultCellMaterial;
                    cell.transform.parent = transform;

                    _gridCells[x, z] = cell;
                }
            }
        }

        private void HandleCellSelection()
        {
            if (Input.GetMouseButtonDown(0)) 
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    GameObject clickedObject = hit.collider.gameObject;

                    if (IsGridCell(clickedObject))
                    {
                        HighlightCell(clickedObject);
                        OpenTowerPlacementUI(clickedObject);
                    }
                }
            }
        }

        private bool IsGridCell(GameObject obj)
        {
            foreach (GameObject cell in _gridCells)
            {
                if (cell == obj)
                {
                    return true;
                }
            }
            return false;
        }

        private void HighlightCell(GameObject cell)
        {
            if (_selectedCell != null)
            {
                _selectedCell.GetComponent<Renderer>().material = _defaultCellMaterial;
            }

            _selectedCell = cell;
            _selectedCell.GetComponent<Renderer>().material = _highlightedCellMaterial;
            _selectedCellPosition = cell.transform.position;
        }
        public Vector3 GetSelectedCellPosition()
        {
            return _selectedCellPosition;
        }
        private void OpenTowerPlacementUI(GameObject cell)
        {
            Debug.Log($"Opening Tower Placement UI for {cell.name}");
            GameService.Instance.uiManager.ActivateTowerSelectionPanel();
        }
        // Place tower on the selected cell
        public void PlaceTowerOnGrid(Vector3 position)
        {
            if (GameService.Instance.towerManager != null)
            {
                GameService.Instance.towerManager.PlaceTower(position);
            }
            else
            {
                Debug.LogError("TowerManager is not assigned.");
            }
        }

        // Remove tower from the selected cell
        public void RemoveTowerFromCell(GameObject cell)
        {
            Vector3 towerPosition = cell.transform.position;
            int xIndex = (int)(towerPosition.x / _gridCellSize);
            int zIndex = (int)(towerPosition.z / _gridCellSize);

            if (_placedTowers[xIndex, zIndex] != null)
            {
                Destroy(_placedTowers[xIndex, zIndex].gameObject); // Destroy the tower
                _placedTowers[xIndex, zIndex] = null; // Reset the array entry
            }
        }

        #region Gizmos Methods
        void OnDrawGizmos()
        {
            if (!Application.isPlaying)
            {
                // Draw the entire plane boundary in white
                Gizmos.color = Color.white;
                Gizmos.DrawWireCube(transform.position, new Vector3(_planeWidth, 0.1f, _planeHeight));

                // Draw the valid grid placement area in green
                Gizmos.color = Color.green;
                Vector3 center = this.transform.position;
                Vector3 gridSize = new Vector3(_planeWidth - 2 * _gridMarginX, 0.1f, _planeHeight - 2 * _gridMarginY);
                Gizmos.DrawWireCube(center, gridSize);

                // Draw individual grid cells within the valid placement area
                Gizmos.color = Color.blue;
                float startX = center.x - (_planeWidth - 2 * _gridMarginX) / 2;
                float startZ = center.z - (_planeHeight - 2 * _gridMarginY) / 2;

                int rows = Mathf.FloorToInt((_planeHeight - 2 * _gridMarginY) / _gridCellSize);
                int cols = Mathf.FloorToInt((_planeWidth - 2 * _gridMarginX) / _gridCellSize);

                for (int x = 0; x <= cols; x++)
                {
                    for (int z = 0; z <= rows; z++)
                    {
                        Vector3 cellPosition = new Vector3(
                            startX + x * _gridCellSize + _gridCellSize / 2,
                            transform.position.y,
                            startZ + z * _gridCellSize + _gridCellSize / 2
                        );

                        Gizmos.DrawWireCube(cellPosition, new Vector3(_gridCellSize, 0.1f, _gridCellSize));
                    }
                }
            }
        }
        #endregion

    }
}
