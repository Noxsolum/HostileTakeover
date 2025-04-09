using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    public SC_TPSController movement;

    public float speedBoost;

    void Start()
    {
        speedBoost = 30f;
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {


            Destroy(this.gameObject);
        }
    }
}
