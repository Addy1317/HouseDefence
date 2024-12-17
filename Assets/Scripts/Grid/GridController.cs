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
        [Header("Grid Variables")]
        [SerializeField] internal float planeWidth = 10f;
        [SerializeField] internal float planeHeight = 10f;
        [SerializeField] internal float gridCellSize = 1f;

        [SerializeField] internal float gridMarginX = 3f;
        [SerializeField] internal float gridMarginY = 3f;

        [Header("Highlight Settings")]
        [SerializeField] private Material _defaultCellMaterial;
        [SerializeField] private Material _highlightedCellMaterial;

        internal GameObject[,] gridCells;
        internal GameObject selectedCell;
        internal TowerBase[,] placedTowers;
        internal Vector3 selectedCellPosition;

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
            int rows = Mathf.FloorToInt((planeHeight - 2 * gridMarginY) / gridCellSize);
            int cols = Mathf.FloorToInt((planeWidth - 2 * gridMarginX) / gridCellSize);

            gridCells = new GameObject[cols, rows];
            float startX = transform.position.x - (planeWidth - 2 * gridMarginX) / 2;
            float startZ = transform.position.z - (planeHeight - 2 * gridMarginY) / 2;

            for (int x = 0; x < cols; x++)
            {
                for (int z = 0; z < rows; z++)
                {
                    Vector3 cellPosition = new Vector3(
                        startX + x * gridCellSize + gridCellSize / 2,
                        transform.position.y,
                        startZ + z * gridCellSize + gridCellSize / 2
                    );

                    GameObject cell = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cell.transform.position = cellPosition;
                    cell.transform.localScale = new Vector3(gridCellSize, 0.1f, gridCellSize);
                    cell.GetComponent<Renderer>().material = _defaultCellMaterial;
                    cell.transform.parent = transform;

                    gridCells[x, z] = cell;
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
            foreach (GameObject cell in gridCells)
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
            if (selectedCell != null)
            {
                selectedCell.GetComponent<Renderer>().material = _defaultCellMaterial;
            }

            selectedCell = cell;
            selectedCell.GetComponent<Renderer>().material = _highlightedCellMaterial;
            selectedCellPosition = cell.transform.position;
        }

        internal void OpenTowerPlacementUI(GameObject cell)
        {
            Debug.Log($"Opening Tower Placement UI for {cell.name}");
            GameService.Instance.uiManager.ActivateTowerSelectionPanel();
        }

        internal Vector3 GetSelectedCellPosition()
        {
            return selectedCellPosition;
        }
        #endregion

        #region Gizmos Methods
        void OnDrawGizmos()
        {
            if (!Application.isPlaying)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawWireCube(transform.position, new Vector3(planeWidth, 0.1f, planeHeight));

                Gizmos.color = Color.green;
                Vector3 center = this.transform.position;
                Vector3 gridSize = new Vector3(planeWidth - 2 * gridMarginX, 0.1f, planeHeight - 2 * gridMarginY);
                Gizmos.DrawWireCube(center, gridSize);

                Gizmos.color = Color.blue;
                float startX = center.x - (planeWidth - 2 * gridMarginX) / 2;
                float startZ = center.z - (planeHeight - 2 * gridMarginY) / 2;

                int rows = Mathf.FloorToInt((planeHeight - 2 * gridMarginY) / gridCellSize);
                int cols = Mathf.FloorToInt((planeWidth - 2 * gridMarginX) / gridCellSize);

                for (int x = 0; x <= cols; x++)
                {
                    for (int z = 0; z <= rows; z++)
                    {
                        Vector3 cellPosition = new Vector3(
                            startX + x * gridCellSize + gridCellSize / 2,
                            transform.position.y,
                            startZ + z * gridCellSize + gridCellSize / 2
                        );

                        Gizmos.DrawWireCube(cellPosition, new Vector3(gridCellSize, 0.1f, gridCellSize));
                    }
                }
            }
        }
        #endregion
    }
}
