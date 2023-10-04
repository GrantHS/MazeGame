using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlaceHolder : MonoBehaviour
{
    // this is all just placeholder for now
    [SerializeField] private GameObject playerSpawn;

    void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.position = new Vector3(7, 0.5f, -5);
        }
    }
}
