using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class SplashScreenChanger : MonoBehaviour {

    [SerializeField] float delay = 2.0f;
    [SerializeField] string sceneName = null;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

	// Use this for initialization
	void Start () {
        Invoke("LoadLevel", delay);	

    }

    private void LoadLevel()
    {
        SceneManager.LoadScene(sceneName);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
