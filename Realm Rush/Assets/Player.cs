using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] [Range(1, 10)] int maxTowers = 3;
    [SerializeField] GameObject towerPrefab = null;
    [SerializeField] [Range(1, 100)] int baseHP = 10;

    [SerializeField] Text HPUI = null;
    [SerializeField] Text ScoreUI = null;

    [SerializeField] AudioClip damageSFX = null;

    private int score;

    private Queue<GameObject> towers = new Queue<GameObject>();

    private void Start()
    {
        HPUI.text = baseHP.ToString();
        ScoreUI.text = score.ToString();
    }

    public void CreateTower(Waypoint waypoint)
    {
        GameObject tower = null;

        if (towers.Count < maxTowers)
        {
            tower = Instantiate(towerPrefab, waypoint.transform.position, Quaternion.identity, transform);
          
        }
        else
        {
            tower = towers.Dequeue();
            tower.GetComponent<Tower>().OccupiedWaypoint.isPlacable = true;
            tower.transform.position = waypoint.transform.position;
        }

        tower.GetComponent<Tower>().OccupiedWaypoint = waypoint;
        waypoint.isPlacable = false;
        towers.Enqueue(tower);
    }

    public void DamageBase(int damage)
    {
        baseHP -= damage;
        HPUI.text = baseHP.ToString();
        GetComponent<AudioSource>().PlayOneShot(damageSFX);
    }
    public void AddScore(int score)
    {
        this.score += score;
        ScoreUI.text = this.score.ToString();
        
    }
}
