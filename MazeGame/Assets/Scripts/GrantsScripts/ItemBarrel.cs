using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBarrel : MonoBehaviour
{
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    

    /*
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Player"))
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    */
}
