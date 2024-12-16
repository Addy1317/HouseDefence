using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace HouseDefence.GridLayout
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] internal Grid grid; // The grid object that defines the layout
        [SerializeField] internal Tilemap tilemap; // Tilemap to manage grid cells
        [SerializeField] internal GameObject highlightPrefab; // Visual highlight of valid placement

        private GameObject highlightInstance; // Instance of the highlight

        private void Start()
        {
            // Optionally instantiate a highlight at the start (can be hidden initially)
            highlightInstance = Instantiate(highlightPrefab);
            highlightInstance.SetActive(false); // Initially hidden
        }

        private void Update()
        {
            HandleGridClick();
        }

        private void HandleGridClick()
        {
            if (Input.GetMouseButtonDown(0)) // Left click to place/remove
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 hitPosition = hit.point;

                    // Snap the hit position to the nearest grid cell
                    Vector3 snappedPosition = SnapToGrid(hitPosition);

                    // Show a highlight to indicate where the player can place a tower
                    ShowHighlight(snappedPosition);
                }
            }
        }

        // Snap the position to the closest grid cell based on tile size
        public Vector3 SnapToGrid(Vector3 worldPosition)
        {
            // Convert world position to grid position
            Vector3Int cellPosition = grid.WorldToCell(worldPosition);

            // Convert cell position back to world position
            return grid.CellToWorld(cellPosition);
        }

        private void ShowHighlight(Vector3 position)
        {
            // Move the highlight to the selected position on the grid
            highlightInstance.transform.position = position;
            highlightInstance.SetActive(true); // Show the highlight

            // Here, you can add logic to check if this position is valid for placing a tower
        }

        public void HideHighlight()
        {
            // Hide the highlight when no longer needed
            if (highlightInstance != null)
            {
                highlightInstance.SetActive(false);
            }
        }
    }
}
