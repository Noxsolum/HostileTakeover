using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseLazarCannon : MonoBehaviour
{
    [SerializeField] private Transform pfProjectile;

    public GameObject player;
    public GameObject firePoint;
    public UIScript uiScript;
    public SC_CameraCollision cameraColl;
    private float timeToFire;
    private float fireRate;

    void Awake()
    {
        uiScript = GameObject.FindGameObjectWithTag("UI").GetComponent<UIScript>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(this.gameObject == GameObject.FindGameObjectWithTag("Player"))
        {
            firePoint = this.gameObject.transform.GetChild(2).gameObject;
        }
        else
        {
            firePoint = this.gameObject.transform.GetChild(1).gameObject;
        }
        fireRate = 1;
        timeToFire = 0;
    }

    // Update is called once per frame
    void Update()
    {
        uiScript = GameObject.FindGameObjectWithTag("UI").GetComponent<UIScript>();
        if(uiScript.isPaused == false)
        {
            ShootLazar();
        }
    }

    private void ShootLazar()
    {
        if(this.gameObject == GameObject.FindGameObjectWithTag("Player"))
        {
            if (Input.GetButton("Fire1") && Time.time >= timeToFire)
            {
                Debug.Log("FIIIRRREEEE!!");
                timeToFire = Time.time + 1 / fireRate;
                SpawnVFX();
            }
        }
        else
        {
            if (player != null)
            {
                float distBetween = Vector3.Distance(this.gameObject.transform.position, player.transform.position);
                if (distBetween <= 150 && Time.time >= timeToFire)
                {
                    Debug.Log("EnemyFire!!");
                    timeToFire = Time.time + 2 / fireRate;
                    SpawnVFX();
                }
            }
            else
                return;
        }
    }

    // Get the height of the camera parent to dictate which direct the 

    public void SpawnVFX()
    {
        if(this.gameObject == GameObject.FindGameObjectWithTag("Player"))
        {
            firePoint.GetComponent<AudioSource>().Play();
            Transform vfx = Instantiate(pfProjectile, firePoint.transform.position, Quaternion.identity);
            vfx.gameObject.GetComponent<projectileScript>().GetOrigin(player);
            float y = player.GetComponent<SC_TPSController>().GetRotation().eulerAngles.y;
            float x = player.transform.GetChild(1).rotation.eulerAngles.x - 5f;
            vfx.localRotation = Quaternion.Euler(x, y, 0);
            //vfx.localRotation = player.GetComponent<SC_TPSController>().GetRotation();
        }
        else
        {
            //if(Random.Range(1,11) >= 7)
            //{
            //    uiScript.DisplayPilot(this.gameObject.GetComponent<EnemyController>().randoEnemy, 2);
            //}
            firePoint.GetComponent<AudioSource>().Play();
            Transform vfx = Instantiate(pfProjectile, firePoint.transform.position, Quaternion.identity);
            vfx.gameObject.GetComponent<projectileScript>().GetOrigin(this.gameObject);
            vfx.localRotation = this.gameObject.transform.rotation;
        }
    }
}
