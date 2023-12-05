using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    public Transform target;
    private Vector3 targetPostition;
    private GameObject arm;

    private void OnEnable()
    {
        arm = GetComponentInChildren<ChildObject>().gameObject;
        target = GameManager.Instance.currentExitRoom.transform;
        targetPostition = new Vector3(target.position.x, arm.transform.position.y, target.position.z);

    }
    private void Update()
    {
        arm.transform.forward = -arm.transform.right;
        arm.transform.LookAt(targetPostition);
    }
}
