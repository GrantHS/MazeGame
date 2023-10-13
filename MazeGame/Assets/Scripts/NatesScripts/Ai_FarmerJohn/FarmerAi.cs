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
   public bool wayPointSet;
    public float walkRange;
    public Light spotlight;

    // Attacking 
    public float timeToAttack;
    bool attacked;

    //Respawn Placeholder
    //public GameObject playerSpawn; <= player already has one

    //Farmer States
    public float sightRange, attackRange;
    public bool playerInSight, playerInAttackRange;
    public bool playerInvisible;
    public float viewAngle;
   

    private PlayerMovementScript isInvisible;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        viewAngle = spotlight.spotAngle;
        //playerSpawn.transform.position = player.transform.position; <= Don't need
    }

    private void Update()
    {
        playerInvisible = player.GetComponent<PlayerMovementScript>()._isInvisible;
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, thisIsPlayer);

        if (canSeePlayer())
        {
            ChasingPlayer();
        }
        else
        {
            Patrolling();
        }

        if (playerInAttackRange && !playerInvisible)
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
      
        if (distanceToWalkPoint.magnitude < 2f)
        {
            wayPointSet = false;
        }
    }

    private bool canSeePlayer()
    {
     Vector3 dirToPlayer = (player.position - transform.position).normalized;
     float angleBetweenFarmer_Player = Vector3.Angle(transform.forward, dirToPlayer);


        if (Vector3.Distance(transform.position, player.position) < sightRange)
        {
          
           
            if (angleBetweenFarmer_Player < viewAngle / 2f)
            {
              //  Debug.Log("TARGET LOCKED");
                playerInSight = true;
                if (!Physics.Linecast(transform.position, player.position, thisIsGround))
                    {
                    return true;
                }

            }
          
        }
        playerInSight = false;
        return false;

    }
    private void WalkPoints()
    {
        float randomZ = Random.Range(-walkRange, walkRange);
        float randomX = Random.Range(-walkRange, walkRange);

        wayPoints = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(wayPoints, -transform.up, 1f, thisIsGround))
        {
            wayPointSet = true;
        }
    }

    private void Attacking()
    {
        nav.SetDestination(player.transform.position);
        Debug.Log("Attacking Player");
        //player.transform.position = playerSpawn.transform.position;
        player.GetComponent<Respawn>().PlayerHealth = 0;
    }

    private void ChasingPlayer()
    {

        nav.SetDestination(player.position);


    }


  
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, attackRange);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * sightRange);
        Gizmos.color = Color.green;
       // Gizmos.DrawRay(transform.position, transform.forward * sightRange);

    }

}
