using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Allows the player to push open any unlocked door using character controller collision
//Player needs character controller and door needs rigidbody with LockedDoor script
public class PushDoor : MonoBehaviour
{
    private LockedDoor _door;

    public float _pushPower = 10f;

    public bool yellowKey = false;
    public bool orangeKey = false;
    public bool redKey = false;
    private bool _pushing = false;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Door"))
        {
            //Debug.Log("Hit door");
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
            if (!_door.isLocked)
            {
                /*
                if (_door == null || _door.gameObject.transform.position.y <= this.gameObject.transform.position.y)
                {
                    Debug.Log("I can't open the door");
                    return;
                }
                else
                {
                */
                    StartCoroutine(PushOpen(hit, 3f));
            
                    

                
            }
            else Debug.Log("I can't open the door");


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

    public IEnumerator PushOpen(ControllerColliderHit hit, float vanishTime)
    {
        if (!hit.gameObject.GetComponent<Rigidbody>())
        {
            hit.gameObject.AddComponent<Rigidbody>();
        }

        Rigidbody rb = hit.collider.gameObject.GetComponent<Rigidbody>();

        Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        
        Vector3 shrinkage = new Vector3(0.05f, 0.05f, 0.05f);

        if (!_pushing)
        {
            _pushing = true;
            hit.gameObject.transform.localScale -= shrinkage;
        }

        rb.velocity = pushDirection * _pushPower;
        //hit.gameObject.GetComponent<BoxCollider>().enabled = false;

        Debug.Log("pushing");

        yield return new WaitForSeconds(vanishTime);

        hit.gameObject.SetActive(false);
        _pushing = false;
    }
}
