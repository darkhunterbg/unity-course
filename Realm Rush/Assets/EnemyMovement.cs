using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [SerializeField]
    List<Waypoint> path = new List<Waypoint>();

	// Use this for initialization
	void Start () {
        StartCoroutine(VisitWaypoints());
	}
	
    IEnumerator VisitWaypoints()
    {
        print("Starting patrol...");

        foreach(Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            print($"Visiting:{waypoint.name}");
            yield return new WaitForSeconds(1.0f);
        }

        print("Ending patrol");
    }

	// Update is called once per frame
	void Update () {
		
	}
}
