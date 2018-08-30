using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX = null;
    [SerializeField] Transform parent = null;
    [SerializeField] int scoreHit = 12;
    [SerializeField] int hp = 10;

    bool deathTriggerd = false;

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
        if (deathTriggerd)
            return;

        --hp;

        if (hp <= 0)
            Kill();

    }

    private void Kill()
    {
        deathTriggerd = true;

        GameObject fx = Instantiate(deathFX, gameObject.transform.position, Quaternion.identity) as GameObject;
        fx.transform.parent = parent;

        scoreboard.Score(scoreHit);

        Destroy(gameObject);
    }
}
