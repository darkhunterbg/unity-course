﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class Rocket : MonoBehaviour
{
    [SerializeField]
    float rcsThrust = 30.0f;
    [SerializeField]
    float mainThrust = 15.0f;
    [SerializeField]
    AudioClip mainEngine = null;
    [SerializeField]
    AudioClip death = null;
    [SerializeField]
    AudioClip finish = null;

    [SerializeField]
    ParticleSystem mainEngineParticles = null;
    [SerializeField]
    ParticleSystem deathParticles = null;
    [SerializeField]
    ParticleSystem finishParticles = null;

    [SerializeField]
    float levelLoadDelay = 1.0f;

    [SerializeField]
    bool collisionEnabled = true;

    Rigidbody rigidBbody;
    AudioSource audioSource;

    State state = State.Alive;

    enum State
    {
        Alive,
        Dying,
        Transcending
    }

    // Use this for initialization
    void Start()
    {
        rigidBbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcesInput();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collisionEnabled)
            return;

        if (state != State.Alive)
            return;

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                SuccessSequence();
                break;
            default:
                DeathSequence();
                break;
        }
    }

    private void DeathSequence()
    {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        mainEngineParticles.Stop();
        deathParticles.Play();
        Invoke(nameof(LoadFisrtLevel), levelLoadDelay);
    }

    private void SuccessSequence()
    {
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(finish);
        mainEngineParticles.Stop();
        finishParticles.Play();
        Invoke(nameof(LoadNextScene), levelLoadDelay);
    }

    private void LoadFisrtLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextScene()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        scene++;
        if (SceneManager.sceneCountInBuildSettings == scene)
            scene = 0;

        SceneManager.LoadScene(scene);
    }

    private void ProcesInput()
    {
        if (Debug.isDebugBuild)
            RespondDebugInput();

        if (state == State.Alive)
        {
            RespondThrustInput();
            RespondRotateInput();
        }
    }

    private void RespondDebugInput()
    {
        if (Input.GetKeyUp(KeyCode.L))
            LoadNextScene();

        if (Input.GetKeyUp(KeyCode.C))
            collisionEnabled = !collisionEnabled;
    }

    private void RespondRotateInput()
    {
        float rotate = rcsThrust * Time.deltaTime;
        rigidBbody.angularVelocity = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward, rotate);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.back, rotate);
        }

    }

    private void RespondThrustInput()
    {
        float velocity = mainThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }

    private void ApplyThrust()
    {
        rigidBbody.AddRelativeForce(Vector3.up * mainThrust);
        if (!audioSource.isPlaying)
            audioSource.PlayOneShot(mainEngine);
        mainEngineParticles.Play();
    }
}
