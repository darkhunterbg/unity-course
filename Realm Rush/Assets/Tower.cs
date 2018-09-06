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

    public Waypoint OccupiedWaypoint { get; set; }

    void Update()
    {
        SetTarget();
        bool shouldShoot = targetEnemy != null;

        if (shouldShoot)
            Shoot();
        else
            StopShooting();
    }

    private void SetTarget()
    {
        bool needsTarget = targetEnemy == null;

        if (targetEnemy != null)
        {
            float distance = Vector3.Distance(targetEnemy.position, objectToPan.position);
            if (distance < attackRange)
                return;
            else
                needsTarget = true;
        }

        if (needsTarget)
        {
            Enemy selected = null;
            float selectedDistance = float.MaxValue;

            foreach (Enemy enemy in FindObjectsOfType<Enemy>())
            {
                float distance = Vector3.Distance(enemy.transform.position, objectToPan.position);
                if(distance< attackRange && selectedDistance > distance)
                {
                    selectedDistance = distance;
                    selected = enemy;
                }
            }

            if (selected != null)
                targetEnemy = selected.transform;
        }
    }

    private void Shoot()
    {
        if (attackFX.isStopped)
            attackFX.Play();
        objectToPan.LookAt(targetEnemy.position + new Vector3(0, floatYOffset, 0));
    }

    private void StopShooting()
    {
        if (attackFX.isPlaying)
            attackFX.Stop();
    }
}
