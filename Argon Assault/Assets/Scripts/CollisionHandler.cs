using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1.0f;

    private void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
    }

    private void StartDeathSequence()
    {
        print("Player dying");
        SendMessage(nameof(PlayerController.OnPlayerDeath));
        Invoke(nameof(ReloadLevel), levelLoadDelay);
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
