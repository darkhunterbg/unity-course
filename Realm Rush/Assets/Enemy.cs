using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] int hp = 100;

    bool deathTriggerd = false;

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
        deathTriggerd = transform;
        Destroy(gameObject);
    }
}
