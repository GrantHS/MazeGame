using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderClimb : MonoBehaviour
{
    public bool _climbing = false;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    [SerializeField]
    private float climbSpeed = 1.0f;

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            _climbing = false;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Ladder"))
        {
            _climbing = true;
        }
    }

    

    private void Update()
    {
        if (_climbing)
        {
            transform.Translate(Vector3.up * climbSpeed);
        }
    }
}
