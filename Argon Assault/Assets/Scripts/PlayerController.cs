using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In ms^-1")] [SerializeField] float xSpeed = 20.0f;
    [Tooltip("In ms^-1")] [SerializeField] float ySpeed = 8.0f;

    [SerializeField] Vector2 range = new Vector2(6, 4);

    [Header("Screen-position based")]
    [SerializeField] float positionPitchFactor = -5.0f;
    [SerializeField] float positionYawFactor = 5.0f;

    [Header("Control-throw based")]
    [SerializeField] float controlRollFactor = 30.0f;
    [SerializeField] float controlPitchFactor = -30.0f;

    [SerializeField] GameObject deathFX = null;

    [SerializeField] GameObject[] guns = null;

    float xThrow = 0;
    float yThrow = 0;

    bool controlEnabled = true;
    bool isShooting = false;

    Scoreboard scoreboard;

    private int time;

    public void OnPlayerDeath()
    {
        controlEnabled = false;
        deathFX?.SetActive(true);

        //  Destroy(gameObject);
    }

    private void Start()
    {
        scoreboard = FindObjectOfType<Scoreboard>();
        time = (int)Time.realtimeSinceStartup;

        foreach (GameObject gun in guns)
            gun.GetComponent<ParticleSystem>().Stop();
    }

    // Update is called once per frame
    private void Update()
    {
        bool fire = false;

        if (controlEnabled)
        {
            UpdateScore();
            xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
            yThrow = CrossPlatformInputManager.GetAxis("Vertical");

            ProcessTranslation();
            ProcessRotation();

            fire = CrossPlatformInputManager.GetButton("Fire");
            if (fire)
                ProcessFiring();
        }

        if (!fire)
            ProcessStopFiring();
    }



    private void UpdateScore()
    {
        int deltaSeconds = (int)Time.realtimeSinceStartup - (int)time;
        if (deltaSeconds > 0)
        {
            scoreboard.Score(deltaSeconds);
            time += deltaSeconds;
        }
    }

    private void ProcessFiring()
    {
        if (!isShooting)
        {
            foreach (GameObject gun in guns)
                gun.GetComponent<ParticleSystem>().Play(false);
            isShooting = true;
        }

    }
    private void ProcessStopFiring()
    {
        if (isShooting)
        {
            foreach (GameObject gun in guns)
                gun.GetComponent<ParticleSystem>().Stop(false, ParticleSystemStopBehavior.StopEmitting);
            isShooting = false;
        }
    }

    private void ProcessRotation()
    {
        float pitch = transform.localPosition.y * positionPitchFactor + yThrow * controlPitchFactor;
        float yaw = transform.localPosition.x * positionYawFactor;// + xThrow;* controlYawFactor;
        float row = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, row);
    }

    private void ProcessTranslation()
    {
        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float yOffset = yThrow * ySpeed * Time.deltaTime;

        float newXPos = Mathf.Clamp(transform.localPosition.x + xOffset, -range.x, range.x);
        float newYPos = Mathf.Clamp(transform.localPosition.y + yOffset, -range.y, range.y);

        transform.localPosition = new Vector3(newXPos, newYPos, transform.localPosition.z);
    }
}
