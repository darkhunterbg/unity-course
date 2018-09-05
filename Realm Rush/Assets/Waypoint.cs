using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public const int gridSize = 10;

    Vector2Int gridPos = Vector2Int.zero;

    public int GridSize => gridSize;
    public Vector2Int GridPos => gridPos;

    public bool IsExplored;

    public Waypoint exploredFrom;

    public static Vector2Int GetGridPosition(Vector3 position)
    {
        return new Vector2Int(Mathf.RoundToInt(position.x / gridSize),
         Mathf.RoundToInt(position.z / gridSize));
    }


    public void SetGridPosFromWorldPos(Vector3 position)
    {
        gridPos = GetGridPosition(position);
    }

    public void SetTopColor(Color color)
    {
        MeshRenderer meshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();
        meshRenderer.material.color = color;
    }
}
