using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour {

    
    int score = 0;
    Text scoreText;
	// Use this for initialization
	void Start () {

        scoreText = GetComponent<Text>();
        scoreText.text = score.ToString();
	}
	

    public void Score(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
    }
}
