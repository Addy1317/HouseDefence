#region Summary
/// <summary>
/// GridController is responsible for generating and managing a grid-based layout for tower placement.
/// It handles cell selection, highlights selected cells, and interfaces with the UI for tower placement.
/// </summary>
#endregion
using HouseDefence.Services;
using HouseDefence.Tower;
using UnityEngine;

namespace HouseDefence.Grid
{
    public class GridController : MonoBehaviour
    {
        [SerializeField] internal float _planeWidth = 10f;
        [SerializeField] internal float _planeHeight = 10f;
        [SerializeField] internal float _gridCellSize = 1f;

        [SerializeField] internal float _gridMarginX = 3f;
        [SerializeField] internal float _gridMarginY = 3f;

        [Header("Highlight Settings")]
        [SerializeField] private Material _defaultCellMaterial;
        [SerializeField] private Material _highlightedCellMaterial;

        internal GameObject[,] _gridCells;
        internal GameObject _selectedCell;
        internal TowerBase[,] _placedTowers;
        internal Vector3 _selectedCellPosition;

        private void Start()
        {
            InitializeGrid();
        }

        private void Update()
        {
            HandleCellSelection();
        }

        #region Grid Methods
        internal void InitializeGrid()
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

        internal void HandleCellSelection()
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

        internal bool IsGridCell(GameObject obj)
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

        internal void HighlightCell(GameObject cell)
        {
            if (_selectedCell != null)
            {
                _selectedCell.GetComponent<Renderer>().material = _defaultCellMaterial;
            }

            _selectedCell = cell;
            _selectedCell.GetComponent<Renderer>().material = _highlightedCellMaterial;
            _selectedCellPosition = cell.transform.position;
        }

        internal void OpenTowerPlacementUI(GameObject cell)
        {
            Debug.Log($"Opening Tower Placement UI for {cell.name}");
            GameService.Instance.uiManager.ActivateTowerSelectionPanel();
        }

        internal Vector3 GetSelectedCellPosition()
        {
            return _selectedCellPosition;
        }
        #endregion
        #region Gizmos Methods
        void OnDrawGizmos()
        {
            if (!Application.isPlaying)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawWireCube(transform.position, new Vector3(_planeWidth, 0.1f, _planeHeight));

                Gizmos.color = Color.green;
                Vector3 center = this.transform.position;
                Vector3 gridSize = new Vector3(_planeWidth - 2 * _gridMarginX, 0.1f, _planeHeight - 2 * _gridMarginY);
                Gizmos.DrawWireCube(center, gridSize);

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
