using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBbody;

    // Use this for initialization
    void Start()
    {
        rigidBbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcesInput();
    }

    private void ProcesInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBbody.AddRelativeForce(Vector3.up);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward, 10 * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.forward, -10 * Time.deltaTime);
        }
    }
}
