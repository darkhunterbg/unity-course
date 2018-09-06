using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
public class CubeEditor : MonoBehaviour
{
    Waypoint waypoint;

    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
        waypoint.SetGridPosFromWorldPos(transform.position);
        transform.hasChanged = false;
    }
    void Update()
    {
        if (transform.hasChanged)
        {
            waypoint.SetGridPosFromWorldPos(transform.position);

            SnapGrid();
            UpdateLabel();

            transform.hasChanged = false;
        }
    }

    private void SnapGrid()
    {
        int gridSize = waypoint.GridSize;
        Vector2Int gridPos = waypoint.GridPos;

        transform.position = new Vector3(gridPos.x * gridSize, 0, gridPos.y * gridSize); ;
    }

    private void UpdateLabel()
    {
        Vector2Int gridPos = waypoint.GridPos;

        string labelText = $"{gridPos.x },{gridPos.y}";

        TextMesh textMesh = GetComponentInChildren<TextMesh>();
        textMesh.text = labelText;

        gameObject.name = labelText;
    }

}
