using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscilator : MonoBehaviour
{

    [SerializeField] Vector3 movementVector = new Vector3(10, 10, 10);

    [Range(0, 1)]
    [SerializeField] float movementFactor = 0;

    [SerializeField] float period = 2.0f;

    Vector3 startingPos;

    // Use this for initialization
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float cycle = Time.time / period;

        const float tau = Mathf.PI * 2;
        float rawSin = Mathf.Sin(cycle * tau);
        movementFactor = rawSin / 2.0f + 0.5f;

        Vector3 offset = movementVector * movementFactor ;
        transform.position = startingPos + offset;
    }
}
