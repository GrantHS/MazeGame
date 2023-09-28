using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlaceHolder : MonoBehaviour
{
    // this is all just placeholder for now
    [SerializeField] private GameObject playerSpawn;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.position = playerSpawn.transform.position;
        }
    }
}
