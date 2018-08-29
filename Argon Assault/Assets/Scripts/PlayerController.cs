using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In ms^-1")] [SerializeField] float xSpeed = 20.0f;
    [Tooltip("In ms^-1")] [SerializeField] float ySpeed = 8.0f;

    [SerializeField] Vector2 range = new Vector2(6,4);

    [Header("Screen-position based")]
    [SerializeField] float positionPitchFactor = -5.0f;
    [SerializeField] float positionYawFactor = 5.0f;

    [Header("Control-throw based")]
    [SerializeField] float controlRollFactor = 30.0f;
    [SerializeField] float controlPitchFactor = -30.0f;

    float xThrow = 0;
    float yThrow = 0;

    bool controlEnabled = true;


    public void OnPlayerDeath()
    {
        controlEnabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (controlEnabled)
        {
            xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
            yThrow = CrossPlatformInputManager.GetAxis("Vertical");

            ProcessTranslation();
            ProcessRotation();
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
