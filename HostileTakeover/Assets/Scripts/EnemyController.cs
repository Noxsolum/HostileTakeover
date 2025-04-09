using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    private GameObject enemy;
    public GameObject gameController;
    public EnemySpawner controller;
    public UIScript uiScript;
    public int randoEnemy;

    public void Start()
    {
        enemy = this.gameObject;
        gameController = GameObject.FindGameObjectWithTag("GameController");
        controller = gameController.GetComponent<EnemySpawner>();
    }

    public void GetEnemy(int abc)
    {
        randoEnemy = abc;
    }

    public void onDeath()
    {
        Debug.Log("Dead");

        // UI Script
        gameController.GetComponent<GameController>().GotKill();
        controller.timeSinceDeath = Time.time;

        // Play Sound
        gameController.GetComponent<GameController>().EnemyDeath();

        // VFX Maybe?

        // Destroying the Object
        Destroy(enemy);
    }
}
