using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX = null;
    [SerializeField] Transform parent = null;
    [SerializeField] int scoreHit = 12;

    bool deathTriggered = false;

    Scoreboard scoreboard;

    private void Start()
    {
        scoreboard = FindObjectOfType<Scoreboard>();

        AddBoxCollider();

    }

    private void AddBoxCollider()
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

        scoreboard.Score(scoreHit);

        Destroy(gameObject);
     
    }
}
