using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] [Range(1f, 100f)] float velocity = 10;

    public void StartMoving()
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();

        Waypoint waypoint = pathfinder.GetWaypointAtPosition(transform.position);

        var path = pathfinder.GetPath(waypoint).Skip(1).ToArray();

        StartCoroutine(FollowPath(path));
    }


    IEnumerator FollowPath(IEnumerable<Waypoint> path)
    {
        float y = transform.position.y;

        foreach (Waypoint waypoint in path)
        {
            Vector3 endPos = new Vector3(waypoint.transform.position.x, y, waypoint.transform.position.z);
            Vector3 direction = endPos - transform.position;
            float step = 0;
            do
            {
                yield return new WaitForFixedUpdate();
                step = Time.fixedDeltaTime * velocity;
                transform.position += direction.normalized * step;

                direction = endPos - transform.position;

            }
            while (Vector3.Magnitude(direction) > step);
            transform.position = endPos;

        }

        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<Enemy>().Damage();
    }
}