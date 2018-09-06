using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] int hp = 100;
    [SerializeField] ParticleSystem hitPrefab = null;
    [SerializeField] ParticleSystem deathPrefab = null;
    [SerializeField] ParticleSystem damagePrefab = null;

    [SerializeField] AudioClip deathSFX = null;

    bool deathTriggerd = false;

    private void OnParticleCollision(GameObject other)
    {
        if (deathTriggerd)
            return;

        Instantiate(hitPrefab, transform);

        --hp;

        if (hp <= 0)
            Kill(deathPrefab);
    }

    private void Kill(ParticleSystem vfx)
    {
        deathTriggerd = true;

        ParticleSystem deathFx = Instantiate(vfx, transform.position + vfx.transform.position, Quaternion.identity);
        Destroy(deathFx.gameObject, vfx.main.duration);


        Destroy(gameObject);


    }


    public void Damage()
    {
        Kill(damagePrefab);
        FindObjectOfType<Player>().DamageBase(1);
    }

}
