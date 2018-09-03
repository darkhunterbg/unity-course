using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    const int gridSize = 10;

    Vector2Int gridPos = Vector2Int.zero;

    public int GridSize => gridSize;
    public Vector2Int GridPos => gridPos;

    public void SetGridPosFromWorldPos(Vector3 position)
    {
        gridPos.x = Mathf.RoundToInt(position.x / gridSize);
        gridPos.y = Mathf.RoundToInt(position.z / gridSize);
    }

    public void SetTopColor(Color color)
    {
        MeshRenderer meshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();
        meshRenderer.material.color = color;
    }
}
