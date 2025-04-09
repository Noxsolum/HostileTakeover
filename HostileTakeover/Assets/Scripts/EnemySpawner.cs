using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private Transform[] enemies;
    private Transform enemyPed;
    public EnemyController EnemyController;
    public VoiceLineScript vScript;
    public UIScript uiScript;
    private int character;
    public int[] RNGchar;
    public float timeSinceDeath;
    public int[] RNGspawn;
    public int previousSpawn;

    public void Awake()
    {
        vScript = GameObject.FindGameObjectWithTag("Voice").GetComponent<VoiceLineScript>();
    }

    // Start is called before the first frame update
    void Start()
    {
        RNGchar = new int[4];
        RNGspawn = new int[7];
        previousSpawn = 0;
        SpawnNewEnemy();
        EnemyController = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnNewEnemyWithDelay();
        if(Time.time >= timeSinceDeath + 5)
        {
            EnemyController = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
        }
    }

    private void SpawnNewEnemy()
    {
        if (enemyPed == null)
        {
            RandRange(uiScript.playerCharacter.characterProfile);
            vScript.GetEnemyCharacter(character);
            vScript.EnemySpawned();
            previousSpawn = Random.Range(0, spawnPoints.Length);
            enemyPed = Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPoints[previousSpawn].transform.position, Quaternion.identity);
            enemyPed.GetComponent<EnemyController>().GetEnemy(character);
        }
        else
            return;
    }

    private void SpawnNewEnemyWithDelay()
    {
        if (enemyPed == null && Time.time >= timeSinceDeath + 5)
        {
            RandRange(uiScript.playerCharacter.characterProfile);
            vScript.GetEnemyCharacter(character);
            vScript.EnemySpawned();
            previousSpawn = RandSpawn(previousSpawn);
            enemyPed = Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPoints[previousSpawn].transform.position, Quaternion.identity);
            enemyPed.GetComponent<EnemyController>().GetEnemy(character);
        }
        else
            return;
    }

    public void RandRange(int player)
    {
        Debug.Log(player);
        for(int i = 0; i < 4; i++)
        {
            if(i != player)
            {
                RNGchar[i] = i;
            }
        }

        character = RNGchar[Random.Range(0 , RNGchar.Length)];
    }

    public int RandSpawn(int prevSpawn)
    {
        for (int i = 0; i < 7; i++)
        {
            if (i != prevSpawn)
            {
                RNGspawn[i] = i;
            }
        }

        return RNGspawn[Random.Range(0, RNGspawn.Length)];
    }
}
