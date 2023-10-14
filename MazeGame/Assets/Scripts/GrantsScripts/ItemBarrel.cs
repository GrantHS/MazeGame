using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//useless script
public class ItemBarrel : MonoBehaviour
{
    public GameObject explosionEffect;

    private void Awake()
    {
        explosionEffect.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colliding with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            
            //StartCoroutine(Explosion());
        }
    }

    private IEnumerator Explosion()
    {

        ParticleSystem particleSystem = explosionEffect.GetComponent<ParticleSystem>();
        explosionEffect.SetActive(true);
        Debug.Log("Explostion active");
        yield return new WaitForSeconds(particleSystem.main.duration);
        explosionEffect.SetActive(false);
        Debug.Log("Explostion active");
    }
}
