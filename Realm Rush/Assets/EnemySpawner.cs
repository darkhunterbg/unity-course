using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] [Range(0.1f, 10.0f)] float secondsBetweenSpawns = 1.0f;

    [SerializeField] GameObject enemy = null;

    [SerializeField] Waypoint[] spawnWaypoints = new Waypoint[0];

    Pathfinder pathfinder = null;

    // Use this for initialization
    void Start()
    {

        pathfinder = FindObjectOfType<Pathfinder>();

        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(1.0f);

        int waypointIndex = -1;

        while (true)
        {
            ++waypointIndex;

            if (waypointIndex >= spawnWaypoints.Length)
                waypointIndex = 0;

            Waypoint spawnPoint = spawnWaypoints[waypointIndex];

            SpawnAtWaypoint(spawnPoint);

            yield return new WaitForSeconds(secondsBetweenSpawns);
        }

    }

    private void SpawnAtWaypoint(Waypoint spawnPoint)
    {
        GameObject newEnemy = Instantiate(enemy, transform) as GameObject;
        newEnemy.transform.position = spawnPoint.transform.position;
        newEnemy.GetComponent<EnemyMovement>().StartMoving();
    }


    // Update is called once per frame
    void Update()
    {

    }
}
