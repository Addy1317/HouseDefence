using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HouseDefence.GridLayout
{
    public class CustomGridManager : MonoBehaviour
    {
        // Plane size and grid cell size
        public float planeWidth = 10f;  // Width of the plane
        public float planeHeight = 10f; // Height of the plane
        public float gridCellSize = 1f; // Size of each grid cell

        // The range where towers can be placed (center of the plane)
        public float gridMarginX = 3f; // Left-right margin for tower placement
        public float gridMarginY = 3f; // Top-bottom margin for tower placement

        // Tower placement visuals
        public GameObject highlightPrefab; // Highlight to show possible tower placement

        private GameObject currentHighlight; // Current highlight instance

        void Start()
        {
            // Initialize the highlight instance and hide it initially
            currentHighlight = Instantiate(highlightPrefab);
            currentHighlight.SetActive(false);
        }

        void Update()
        {
            HandleGridClick();
            UpdateGridHighlight();
        }

        // Handle mouse click to place/remove towers
        void HandleGridClick()
        {
            if (Input.GetMouseButtonDown(0)) // Left mouse click
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 hitPosition = hit.point;

                    // Snap the position to the grid and ensure it's within the valid area
                    Vector3 snappedPosition = SnapToGrid(hitPosition);

                    // Place/remove tower logic here (if the position is valid)
                    Debug.Log("Placing tower at: " + snappedPosition);
                    PlaceTower(snappedPosition);
                }
            }
        }

        // Snap the hit position to the closest grid cell within the valid grid region
        Vector3 SnapToGrid(Vector3 worldPosition)
        {
            // Calculate grid position in world space
            float snappedX = Mathf.Floor((worldPosition.x - transform.position.x) / gridCellSize) * gridCellSize + transform.position.x;
            float snappedY = Mathf.Floor((worldPosition.z - transform.position.z) / gridCellSize) * gridCellSize + transform.position.z;

            // Restrict position to the center area where towers can be placed
            snappedX = Mathf.Clamp(snappedX, transform.position.x - gridMarginX, transform.position.x + gridMarginX);
            snappedY = Mathf.Clamp(snappedY, transform.position.z - gridMarginY, transform.position.z + gridMarginY);

            return new Vector3(snappedX, 0f, snappedY); // Return the snapped position on the plane's XZ plane
        }

        // Show a highlight at the hovered grid cell
        void UpdateGridHighlight()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 hitPosition = hit.point;

                // Snap to the grid and show highlight if it's within the valid range
                Vector3 snappedPosition = SnapToGrid(hitPosition);

                // Set highlight position
                currentHighlight.transform.position = snappedPosition;
                currentHighlight.SetActive(true); // Show the highlight
            }
            else
            {
                currentHighlight.SetActive(false); // Hide highlight when not hovering over the grid
            }
        }

        // Dummy function to place a tower at a valid position (you will implement this later)
        void PlaceTower(Vector3 position)
        {
            // Place tower logic (instantiate tower at position)
            // For now, we just log the position
            Debug.Log("Tower placed at: " + position);
        }

        void OnDrawGizmos()
        {
            if (!Application.isPlaying)
            {
                // Draw the entire plane boundary in white
                Gizmos.color = Color.white;
                Gizmos.DrawWireCube(transform.position, new Vector3(planeWidth, 0.1f, planeHeight));

                // Draw the valid grid placement area in green
                Gizmos.color = Color.green;
                Vector3 center = this.transform.position;
                Vector3 gridSize = new Vector3(planeWidth - 2 * gridMarginX, 0.1f, planeHeight - 2 * gridMarginY);
                Gizmos.DrawWireCube(center, gridSize);

                // Draw individual grid cells within the valid placement area
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

    }
}
