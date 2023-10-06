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
    public Light spotlight;

    // Attacking 
    public float timeToAttack;
    bool attacked;

    //Respawn Placeholder
    public GameObject playerSpawn;

    //Farmer States
    public float sightRange, attackRange;
    public bool playerInSight, playerInAttackRange;
    public bool playerInvisible;
    private float viewDistance;
    private float viewAngle;
    public LayerMask viewMask;

    private PlayerMovementScript isInvisible;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        viewAngle = spotlight.spotAngle;
       
    }

    private void Update()
    {
        playerInvisible = player.GetComponent<PlayerMovementScript>()._isInvisible;
       // playerInvisible = isInvisible._isInvisible;
        playerInSight = Physics.CheckSphere(transform.position, sightRange, thisIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, thisIsPlayer);
       
        if (!canSeePlayer() && !playerInAttackRange) 
        {
            Patrolling();
        }
        if (canSeePlayer() && !playerInAttackRange)
        {
            if (playerInvisible)
            {
                Patrolling();
            }
            else
            {
                ChasingPlayer();
            }
            
           
            
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

    bool canSeePlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < viewDistance)
        {
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            float angleBetweenFarmer_Player = Vector3.Angle(transform.position, dirToPlayer);
            if (angleBetweenFarmer_Player < viewAngle / 2f)
            {
                if (!Physics.Linecast(transform.position, player.position, viewMask))
                    {
                    return true;
                }
               
            }
        }
        return false;
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
        GameObject.FindGameObjectWithTag("Player").transform.position = playerSpawn.transform.position;
    }

    private void ChasingPlayer()
    {

        nav.SetDestination(player.position);
    }


  
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
       // Gizmos.DrawSphere(transform.position, attackRange);
        Gizmos.color = Color.red;
       // Gizmos.DrawSphere(transform.position, sightRange);
    }

}
