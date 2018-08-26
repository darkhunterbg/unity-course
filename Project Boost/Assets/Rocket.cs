using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField]
    float rcsThrust = 30.0f;

    [SerializeField]
    float mainThrust = 15.0f;

    Rigidbody rigidBbody;
    AudioSource engineSound;


    // Use this for initialization
    void Start()
    {
        rigidBbody = GetComponent<Rigidbody>();
        engineSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcesInput();
    }

    private void ProcesInput()
    {
        bool isThrusting = Thrust();

        //if (isThrusting)
        Rotate();
    }

    private void Rotate()
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

    private bool Thrust()
    {
        bool thrust = false;

        float velocity = mainThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            rigidBbody.AddRelativeForce(Vector3.up * mainThrust);
            thrust = true;
            if (!engineSound.isPlaying)
                engineSound.Play();
        }
        else
        {
            if (engineSound.isPlaying)
                engineSound.Stop();
        }

        return thrust;
    }
}
