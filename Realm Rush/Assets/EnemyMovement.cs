using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    void Start()
    {
        Invoke(nameof(StartMoving), 0.1f);
    }

    private void StartMoving()
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();

        Waypoint waypoint = pathfinder.GetWaypointAtPosition(transform.position);

        var path = pathfinder.GetPath(waypoint).ToArray();

        StartCoroutine(FollowPath(path));
    }


    IEnumerator FollowPath(IEnumerable<Waypoint> path)
    {

        foreach (Waypoint waypoint in path)
        {
            float y = transform.position.y;
            transform.position = new Vector3(waypoint.transform.position.x, y, waypoint.transform.position.z);
            yield return new WaitForSeconds(1.0f);
        }

    }
}