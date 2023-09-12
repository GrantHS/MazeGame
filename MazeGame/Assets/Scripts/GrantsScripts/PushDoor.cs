using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Allows the player to push open any unlocked door using character controller collision
//Player needs character controller and door needs rigidbody with LockedDoor script
public class PushDoor : MonoBehaviour
{
    private LockedDoor _door;
    [SerializeField]
    private float _pushPower;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Door"))
        {
            _door = hit.gameObject.GetComponent<LockedDoor>();
            if(_door == null || _door.isLocked)
            {
                Debug.Log("I can't open the door");
                return;
            }

            if(_door.gameObject.transform.position.y <= this.gameObject.transform.position.y)
            {
                return;
            }

            Rigidbody rb = hit.collider.attachedRigidbody;

            Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

            rb.velocity = pushDirection * _pushPower;
        }


    }
}
