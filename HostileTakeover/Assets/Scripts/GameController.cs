using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public AudioSource source;
    public AudioClip explosion;
    public UIScript UI;
    public float kills { get; set;}
    // Start is called before the first frame update
    void Start()
    {
        kills = 0;
        source = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GotKill()
    {
        kills += 1;
    }
    public void EnemyDeath()
    {
        source.clip = explosion;
        source.Play();
    }

    public void PlayerDeath()
    {
        source.clip = explosion;
        source.Play();
        UI.GameOverScreen();
    }
}
