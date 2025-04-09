using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public EnemyController enemy;
    public GameObject gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void onDeath()
    {
        Camera.main.transform.parent = null;
        gameController.GetComponent<GameController>().PlayerDeath();


        Destroy(this.gameObject);
    }
}
