using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour {

    [SerializeField] int scoreHit = 12;
    int score = 0;
    Text scoreText;
	// Use this for initialization
	void Start () {

        scoreText = GetComponent<Text>();
        scoreText.text = score.ToString();
	}
	
    public void ScoreHit()
    {
        score += scoreHit;
        scoreText.text = score.ToString();
    }
}
