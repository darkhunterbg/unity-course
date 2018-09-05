using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform objectToPan = null;
    [SerializeField] Transform targetEnemy = null;
    [SerializeField] [Range(1.0f, 200.0f)] float attackRange = 10;

    [SerializeField] ParticleSystem attackFX = null;

    [SerializeField] float floatYOffset = 6;

    void Update()
    {
        bool shouldShoot = false;

        if (targetEnemy != null)
        {
            float distance = Vector3.Distance(targetEnemy.position, objectToPan.position);

            shouldShoot = distance <= attackRange;

        }

        if (shouldShoot)
            Shoot();
        else
            StopShooting();
    }

    private void Shoot()
    {
        if (attackFX.isStopped)
            attackFX.Play();
        objectToPan.LookAt(targetEnemy.position+new Vector3(0, floatYOffset,0));
    }

    private void StopShooting()
    {
        if (attackFX.isPlaying)
            attackFX.Stop();
    }
}
