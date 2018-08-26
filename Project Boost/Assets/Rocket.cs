using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (state != State.Alive)
            return;

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                state = State.Transcending;
                audioSource.Stop();
                audioSource.PlayOneShot(finish);
                Invoke(nameof(LoadNextScene), 1.0f);
                break;
            default:
                state = State.Dying;
                audioSource.Stop();
                audioSource.PlayOneShot(death);
                Invoke(nameof(LoadFisrtLevel), 1.0f);
                break;
        }
    }

    private void LoadFisrtLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }

    private void ProcesInput()
    {
        if (state == State.Alive)
        {
            RespondThrustInput();
            RespondRotateInput();
        }
    
    }

    private void RespondRotateInput()
    {
        float rotate = rcsThrust * Time.deltaTime;

        rigidBbody.freezeRotation = true;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward, rotate);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.back, rotate);
        }

        rigidBbody.freezeRotation = false;
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
            // if (engineSound.isPlaying)
            audioSource.Stop();
        }
    }

    private void ApplyThrust()
    {
        rigidBbody.AddRelativeForce(Vector3.up * mainThrust);
        if (!audioSource.isPlaying)
            audioSource.PlayOneShot(mainEngine);
    }
}
