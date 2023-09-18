using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Manager : MonoBehaviour
{

    public NavMeshAgent navAgent;

    public float pathDelay = 0.2f;
    // possible animation script here


    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        
    }
}
