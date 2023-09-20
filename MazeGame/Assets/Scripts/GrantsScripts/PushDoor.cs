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

    public bool yellowKey;
    public bool orangeKey;
    public bool redKey;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Door"))
        {
            _door = hit.gameObject.GetComponent<LockedDoor>();

            switch (_door.color)
            {
                case DoorColor.Yellow:
                    if (yellowKey) _door.isLocked = false;
                    break;
                case DoorColor.Orange:
                    if (orangeKey) _door.isLocked = false;
                    break;
                case DoorColor.Red:
                    if (redKey) _door.isLocked = false;
                    break;
                default:
                    Debug.Log("Color of door unknown");
                    break;
            }

            if(_door == null || _door.isLocked || _door.gameObject.transform.position.y <= this.gameObject.transform.position.y)
            {
                Debug.Log("I can't open the door");
                return;
            }
            else
            {
                StartCoroutine(PushOpen(hit, 3f));
                
            }

            /*dont push the door if it is already on the ground
            if(_door.gameObject.transform.position.y <= this.gameObject.transform.position.y)
            {
                return;
            }
            */

            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Red Key"))
        {
            redKey = true;
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Orange Key"))
        {
            orangeKey = true;
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Yellow Key"))
        {
            yellowKey = true;
            other.gameObject.SetActive(false);
        }
    }

    private IEnumerator PushOpen(ControllerColliderHit hit, float vanishTime)
    {
        Debug.Log("pushing");
        Rigidbody rb = hit.collider.attachedRigidbody;

        Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        rb.velocity = pushDirection * _pushPower;
        hit.gameObject.GetComponent<BoxCollider>().enabled = false;

        yield return new WaitForSeconds(vanishTime);

        hit.gameObject.SetActive(false);
    }
}
