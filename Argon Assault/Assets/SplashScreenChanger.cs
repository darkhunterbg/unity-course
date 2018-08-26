using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class SplashScreenChanger : MonoBehaviour {

    [SerializeField] float delay;
    [SerializeField] string sceneName;

	// Use this for initialization
	void Start () {
        Invoke(nameof(LoadLevel), delay);	
	}

    private void LoadLevel()
    {
        SceneManager.LoadScene(sceneName);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
