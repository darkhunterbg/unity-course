using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{

    [Tooltip("In ms^-1")] [SerializeField] float xSpeed = 10.0f;
    [Tooltip("In ms^-1")] [SerializeField] float ySpeed = 4.0f;

    [SerializeField] Vector2 clamp = new Vector2(5, 2);
    [SerializeField] float yawScale = 1.0f;

    [SerializeField] float positionPitchFactor = -5.0f;
    [SerializeField] float controlPitchFactor = -30.0f;

    [SerializeField] float positionYawFactor = 5.0f;
    // [SerializeField] float controlYawFactor = -5.0f;

    [SerializeField] float controlRollFactor = 30.0f;

    float xThrow = 0;
    float yThrow = 0;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        ProcessTranslation();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        float pitch = transform.localPosition.y * positionPitchFactor + yThrow * controlPitchFactor;
        float yaw = transform.localPosition.x * positionYawFactor;// + xThrow;* controlYawFactor;
        float row = xThrow *controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, row);
    }

    private void ProcessTranslation()
    {
        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float yOffset = yThrow * ySpeed * Time.deltaTime;

        float newXPos = Mathf.Clamp(transform.localPosition.x + xOffset, -clamp.x, clamp.x);
        float newYPos = Mathf.Clamp(transform.localPosition.y + yOffset, -clamp.y, clamp.y);

        transform.localPosition = new Vector3(newXPos, newYPos, transform.localPosition.z);
    }
}
