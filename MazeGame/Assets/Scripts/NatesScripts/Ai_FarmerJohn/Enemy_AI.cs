using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    public Transform target;
    private float pathUpdate;
    private Enemy_Manager enemyManager;

    private float attackDis;
 

    private void Awake()
    {
        enemyManager = GetComponent<Enemy_Manager>();
        attackDis = enemyManager.navAgent.stoppingDistance;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            bool inRange = Vector3.Distance(transform.position, target.position) <= attackDis;
            if (inRange)
            {
                LookAtTarget();
            }
            else
            {
                UpdatePath();
            }

        }



    }

    private void LookAtTarget()
    {
        Vector3 lookPos = target.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);
    }

    private void UpdatePath()
    {
        if (Time.time >= pathUpdate)
        {
            Debug.Log("updating path");
            pathUpdate = Time.time + enemyManager.pathDelay;
            enemyManager.navAgent.SetDestination(target.position);
        }
    }



}
