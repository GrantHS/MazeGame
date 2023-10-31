using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderClimb : MonoBehaviour
{
    public bool _climbing = false;
    public bool _goingDown = false;
    private Rigidbody _rb;
    public Transform[] travelPoints = new Transform[2];

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private float climbSpeed = 2f;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            _climbing = false;
            _rb.useGravity = true;
            gameObject.GetComponent<PlayerMovementScript>().enabled = true;
            Debug.Log("exiting climb");
        }
    }



    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Ladder"))
        {
            /*
            if (!_climbing)
            {
                travelPoints = hit.gameObject.GetComponentsInChildren<Transform>();
                foreach (Transform travelPoint in travelPoints)
                {
                    if (travelPoint.gameObject.CompareTag("Top"))
                    {
                        travelPoints[0] = travelPoint;
                    }
                    else if (travelPoint.gameObject.CompareTag("Bottom"))
                    {
                        travelPoints[1] = travelPoint;
                    }
                    else
                    {
                        Debug.Log(travelPoint.name + " is not a valid gameObject");
                    }
                }
            }
            */

            if (!_goingDown) _climbing = true;
            
            _rb.useGravity = false;
        }
    }

    

    private void Update()
    {
        if (_climbing)
        {
            _goingDown = false;
            gameObject.GetComponent<PlayerMovementScript>().enabled = false;
            transform.position = Vector3.MoveTowards(transform.position, travelPoints[0].transform.position, climbSpeed * Time.deltaTime);
            if(transform.position.y >= travelPoints[0].position.y)
            {
                _climbing = false;
                gameObject.GetComponent<PlayerMovementScript>().enabled = true;
            }
        }

        if (_goingDown)
        {
            _climbing = false;
            transform.position = Vector3.MoveTowards(transform.position, travelPoints[1].transform.position, climbSpeed * Time.deltaTime);
            if (transform.position.y <= travelPoints[1].position.y)
            {
                _goingDown = false;
            }
        }
    }
}
