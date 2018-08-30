using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX = null;
    [SerializeField] Transform parent = null;

    bool deathTriggered = false;

    private void Start()
    {
        Collider collider = gameObject.AddComponent<BoxCollider>();
        collider.isTrigger = false;

    }

    private void OnParticleCollision(GameObject other)
    {
        if (deathTriggered)
            return;

        deathTriggered = true;

        print($"Particles collided with enemy {other.name}");

        GameObject fx = Instantiate(deathFX, gameObject.transform.position, Quaternion.identity) as GameObject;
        fx.transform.parent = parent;

        FindObjectOfType<Scoreboard>().ScoreHit();

        Destroy(gameObject);
     
    }
}
