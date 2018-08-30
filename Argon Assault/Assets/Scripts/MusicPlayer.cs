using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
public class MusicPlayer : MonoBehaviour
{
    void Awake()
    {
        MusicPlayer[] musicPlayers = FindObjectsOfType<MusicPlayer>();

        if (musicPlayers.Length > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
