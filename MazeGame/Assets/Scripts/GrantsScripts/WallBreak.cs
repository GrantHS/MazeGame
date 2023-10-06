using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBreak : MonoBehaviour
{
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.gameObject.CompareTag("Breakable Wall"))
        {
            Debug.Log("Hit Breakable Wall");
            hit.collider.gameObject.SetActive(false);
            this.enabled = false;
        }
    }
}
