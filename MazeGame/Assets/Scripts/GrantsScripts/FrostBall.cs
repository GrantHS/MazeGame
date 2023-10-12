using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FrostBall : MonoBehaviour
{
    private float speed = 5f;
    private float freezeTime = 3f;
    private NavMeshAgent target;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        //TODO: not exectuting trigger event
        if (other.gameObject.CompareTag("Farmer"))
        {
            Debug.Log("Freezing Farmer");
            target = other.gameObject.GetComponentInParent<NavMeshAgent>();
            StartCoroutine(Freeze(target));
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator Freeze(NavMeshAgent target)
    {
        float tempSpeed = target.speed;
        Material tempMat = target.GetComponent<MeshRenderer>().material;
        target.speed = 0;
        target.GetComponent<MeshRenderer>().material.color = Color.cyan;
        yield return new WaitForSeconds(freezeTime);
        target.GetComponent<MeshRenderer>().material = tempMat;
        target.speed = tempSpeed;
    }
}
