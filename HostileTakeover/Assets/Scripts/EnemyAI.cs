using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public UIScript UI;
    NavMeshAgent enemy;
    public float rotationSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        UI = GameObject.FindGameObjectWithTag("UI").GetComponent<UIScript>();
        enemy = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null && UI.isPaused == false)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            enemy.speed = 10;
            enemy.destination = player.position;
            float dist = Vector3.Distance(enemy.transform.position, player.transform.position);
            if(dist <= 50)
            {
                Vector3 direction = (player.transform.position - enemy.transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            }
        }
        else if(UI.isPaused == true)
        {
            enemy.speed = 0;
        }
    }
}
