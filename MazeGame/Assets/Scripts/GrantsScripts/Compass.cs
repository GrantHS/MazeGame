using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Compass : MonoBehaviour
{
    public Transform target;
    private Vector3 targetPostition;
    private GameObject arm;

    private void Awake()
    {
        arm = GetComponentInChildren<ChildObject>().gameObject;
        targetPostition = new Vector3(target.position.x, arm.transform.position.y, target.position.z);

    }
    private void Update()
    {
        arm.transform.forward = -arm.transform.right;
        arm.transform.LookAt(targetPostition);
    }
}
