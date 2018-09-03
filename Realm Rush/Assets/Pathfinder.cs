using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField]
    Waypoint startWaypoint;

    [SerializeField]
    Waypoint endWaypoint;

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Vector2Int[] directions = new Vector2Int[]
    {
        Vector2Int.left, Vector2Int.right, Vector2Int.up, Vector2Int.down
    };

    // Use this for initialization
    void Start()
    {
        LoadBlocks();
        ColorStartAndEnd();
        ExploreNeighbours();
    }

    private void ColorStartAndEnd()
    {
        startWaypoint.SetTopColor(Color.green);
        endWaypoint.SetTopColor(Color.red);
    }

    private void ExploreNeighbours()
    {
        Waypoint waypoint = null;

        foreach (Vector2Int dir in directions)
        {
            Vector2Int key = dir + startWaypoint.GridPos;
            if (grid.TryGetValue(key, out waypoint))
            {
                waypoint.SetTopColor(Color.blue);
            }
        }
    }

    private void LoadBlocks()
    {
        Waypoint[] waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            if (grid.ContainsKey(waypoint.GridPos))
            {
                Debug.LogError($"Waypoint in position {waypoint.GridPos} already exists!");
            }
            else
            {
                grid.Add(waypoint.GridPos, waypoint);
            }
        }

    }
}
