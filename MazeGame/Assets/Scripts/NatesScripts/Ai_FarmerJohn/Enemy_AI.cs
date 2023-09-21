using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_AI : MonoBehaviour
{
    public Transform target;
    private float pathUpdate;
    private Enemy_Manager enemyManager;
   
    private float attackDis;


    //Patroling Stuff
  
    private void Awake()
    {
        enemyManager = GetComponent<Enemy_Manager>();
        attackDis = enemyManager.navAgent.stoppingDistance;
    }
 
    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            LookAtTarget();
        }
        else
        {
            Update();
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
