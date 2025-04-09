using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileScript : MonoBehaviour
{
    public GameObject projectile;
    public GameObject origin;
    public float speed;
    public SC_TPSController lazarScript;
    private Vector3 shootDir;

    private void Awake()
    {
        speed = 150;
        StartCoroutine(ParticleLifeTimeCoroutine());
    }

    private void Update()
    {
        transform.position += transform.forward * (speed * Time.deltaTime);
    }

    IEnumerator ParticleLifeTimeCoroutine()
    {
        yield return new WaitForSeconds(15);
        DestroyProjectile();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject != origin)
        {
            speed = 0;
            if (origin != null && collision.gameObject.GetComponent<HealthController>() != null)
            {
                collision.gameObject.GetComponent<HealthController>().InflictDamage(1, origin);
            }
        }
    }

    public void GetOrigin(GameObject origin)
    {
        this.origin = origin;
    }

    public void DestroyProjectile()
    { 
        Destroy(this.gameObject);
    }
}
