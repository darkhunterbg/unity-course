using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] [Range(1, 10)] int maxTowers = 3;
    [SerializeField] GameObject towerPrefab = null;

    private Queue<GameObject> towers = new Queue<GameObject>();

    public void CreateTower(Waypoint waypoint)
    {
        GameObject tower = null;

        if (towers.Count < maxTowers)
        {
            tower = Instantiate(towerPrefab, waypoint.transform.position, Quaternion.identity, transform);
          
        }
        else
        {
            tower = towers.Dequeue();
            tower.GetComponent<Tower>().OccupiedWaypoint.isPlacable = true;
            tower.transform.position = waypoint.transform.position;
        }

        tower.GetComponent<Tower>().OccupiedWaypoint = waypoint;
        waypoint.isPlacable = false;
        towers.Enqueue(tower);
    }
}
