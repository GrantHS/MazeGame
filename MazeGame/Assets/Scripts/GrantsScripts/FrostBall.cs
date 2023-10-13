using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FrostBall : MonoBehaviour
{
    private float speed = 200f;
    private float freezeTime = 3f;
    private float shootDist = 10f;
    private NavMeshAgent target;
    private Rigidbody rb;
    private Vector3 _shootDirection;
    public Transform master;
    public Material freezeMat;
    private bool _freezing = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    private void Start()
    {
        //rb.AddForce(_shootDirection * speed * Time.deltaTime);
        _shootDirection = this.transform.position - master.position;
        rb.velocity = _shootDirection * speed * Time.deltaTime;
        StartCoroutine(Lifespan());
        
    }

    void Update()
    {
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);
        
    }

    /*
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
    */

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider != null)
        {
            //Debug.Log("Collision");
            if (collision.gameObject.CompareTag("Farmer"))
            {
                //Debug.Log("Freezing Farmer");
                target = collision.gameObject.GetComponentInParent<NavMeshAgent>();
                StartCoroutine(Freeze(target));              
            }

            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        
    }

    private IEnumerator Freeze(NavMeshAgent target)
    {
        _freezing = true;
        float tempSpeed = target.speed;
        Material tempMat = target.GetComponentInChildren<MeshRenderer>().material;
        target.speed = 0;
        target.GetComponentInChildren<MeshRenderer>().material = freezeMat;
        Debug.Log("Farmer frozen");
        yield return new WaitForSeconds(freezeTime);
        Debug.Log("Thawed farmer");
        target.GetComponentInChildren<MeshRenderer>().material = tempMat;
        target.speed = tempSpeed;
        this.gameObject.SetActive(false);
    }

    private IEnumerator Lifespan()
    {
        yield return new WaitForSeconds(shootDist);
        if (!_freezing) this.gameObject.SetActive(false);
        
    }


}
