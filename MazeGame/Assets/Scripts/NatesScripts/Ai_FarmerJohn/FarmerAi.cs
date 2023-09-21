using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FarmerAi : MonoBehaviour
{
    public NavMeshAgent nav;
    public Transform player;
    public LayerMask thisIsGround, thisIsPlayer;

    //Patroling
    public Vector3 wayPoints;
    bool wayPointSet;
    public float walkRange;

    // Attacking 
    public float timeToAttack;
    bool attacked;

    //Farmer States
    public float sightRange, attackRange;
    public bool playerInSight, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        nav = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, thisIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, thisIsPlayer);

        if (!playerInSight && !playerInAttackRange) 
        {
            Patrolling();
        }
        if (playerInSight && !playerInAttackRange)
        {
            ChasingPlayer();
        }
        if (playerInAttackRange && playerInAttackRange)
        {
            Attacking();
        }
    }

    private void Patrolling()
    {
        if (!wayPointSet)
        {
            WalkPoints();
        }
        if (wayPointSet)
        {
            nav.SetDestination(wayPoints);
        }
        Vector3 distanceToWalkPoint = transform.position - wayPoints;
        if (distanceToWalkPoint.magnitude < 1f)
        {
            wayPointSet = false;
        }
    }
    private void WalkPoints()
    {
        float randomZ = Random.Range(-walkRange, walkRange);
        float randomX = Random.Range(-walkRange, walkRange);

        wayPoints = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(wayPoints, -transform.up, 2f, thisIsGround))
        {
            wayPointSet = true;
        }
    }

    private void Attacking()
    {
        nav.SetDestination(transform.position);
        Debug.Log("Attacking Player");
        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(7, 0.5f, -5);
    }

    private void ChasingPlayer()
    {
        nav.SetDestination(player.position);
    }

}
