using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{

    [Tooltip("In ms^-1")] [SerializeField] float xSpeed = 10.0f;
    [Tooltip("In ms^-1")] [SerializeField] float ySpeed = 4.0f;

    [SerializeField] Vector2 clamp = new Vector2(5, 2);

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float yOffset = yThrow * ySpeed * Time.deltaTime;

        float rawNewXPos = Mathf.Clamp(transform.localPosition.x + xOffset, -clamp.x, clamp.x);
        float rawNewYPos = Mathf.Clamp(transform.localPosition.y + yOffset, -clamp.y, clamp.y);

        transform.localPosition = new Vector3(rawNewXPos, rawNewYPos, transform.localPosition.z);
    }
}
