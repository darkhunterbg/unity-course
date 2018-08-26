using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
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
        const float rotationSpeed = 30.0f;

        rigidBbody.freezeRotation = true;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.back, rotationSpeed * Time.deltaTime);
        }

        rigidBbody.freezeRotation = false;
    }

    private bool Thrust()
    {
        bool thrust = false;

        if (Input.GetKey(KeyCode.Space))
        {
            rigidBbody.AddRelativeForce(Vector3.up);
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
