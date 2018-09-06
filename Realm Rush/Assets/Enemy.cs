using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] int hp = 100;
    [SerializeField] GameObject hitPrefab = null;
    [SerializeField] GameObject deathPrefab = null;

    bool deathTriggerd = false;

    private void OnParticleCollision(GameObject other)
    {
        if (deathTriggerd)
            return;

        Instantiate(hitPrefab, transform);

        --hp;

        if (hp <= 0)
            Kill();
    }

    private void Kill()
    {
        StartCoroutine(SpawnDeathFx());

        deathTriggerd = transform;
        Destroy(gameObject);


    }

    IEnumerator SpawnDeathFx()
    {
        GameObject deathFx = Instantiate(deathPrefab, transform.position + deathPrefab.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(6);
        Destroy(deathFx);
    }
}
