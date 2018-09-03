using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField]
    Waypoint startWaypoint = null;

    [SerializeField]
    Waypoint endWaypoint = null;


    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Vector2Int[] directions = new Vector2Int[]
    {
        Vector2Int.left, Vector2Int.right, Vector2Int.up, Vector2Int.down
    };

    Queue<Waypoint> queue = new Queue<Waypoint>();

    List<Waypoint> path = new List<Waypoint>();

    [SerializeField]
    bool isRunning = true;

    void Start()
    {
        LoadBlocks();
        ColorStartAndEnd();
    }

    public List<Waypoint> GetPath()
    {
        BreathFirstSearch();
        GeneratePath();
        return path;
    }

    private void ColorStartAndEnd()
    {
        startWaypoint.SetTopColor(Color.green);
        endWaypoint.SetTopColor(Color.red);
    }

    private void BreathFirstSearch()
    {
        foreach (Waypoint waypoint in grid.Values)
        {
            waypoint.IsExplored = false;
            waypoint.exploredFrom = null;
        }
        queue.Clear();

        queue.Enqueue(startWaypoint);

        List<Waypoint> explored = new List<Waypoint>();

        while (queue.Count > 0 && isRunning)
        {
            Waypoint waypoint = queue.Dequeue();
            waypoint.IsExplored = true;

            if (waypoint == endWaypoint)
            {
                //Stop
                isRunning = false;
            }
            else
            {
                explored.Clear();
                ExploreNeighbours(waypoint, explored);

                foreach (Waypoint exploredWaypoint in explored)
                {
                    if (!queue.Contains(exploredWaypoint))
                        queue.Enqueue(exploredWaypoint);
                }
            }

        }
    }

    private void GeneratePath()
    {
        path.Clear();
        Waypoint wp = endWaypoint;
        do
        {
            path.Add(wp);
            wp = wp.exploredFrom;

        }
        while (wp != startWaypoint);
        path.Add(startWaypoint);
        path.Reverse();
    }

    private void ExploreNeighbours(Waypoint currentWaypoint, List<Waypoint> outWaypoints)
    {
        Waypoint waypoint = null;

        foreach (Vector2Int dir in directions)
        {
            Vector2Int key = dir + currentWaypoint.GridPos;
            if (grid.TryGetValue(key, out waypoint) && !waypoint.IsExplored)
            {
                waypoint.exploredFrom = currentWaypoint;
                outWaypoints.Add(waypoint);
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
