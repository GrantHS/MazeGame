using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WallBreak : MonoBehaviour
{
    public GameObject strengthEffect;

    private void Start()
    {
        strengthEffect.SetActive(true);
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.gameObject.CompareTag("Breakable Wall"))
        {
            Debug.Log("Hit Breakable Wall");
            hit.collider.gameObject.SetActive(false);
            strengthEffect.SetActive(false);
            this.enabled = false;
        }
    }
}
