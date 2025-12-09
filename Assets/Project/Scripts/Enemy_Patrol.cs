using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Patrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float patrolSpeed = 2f;
    public float patrolPointArrivalDistance = 2f;

    private UnityEngine.AI.NavMeshAgent agent;
    private int currentIndex = 0;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        if (patrolPoints.Length > 0)
        {
            agent.speed = patrolSpeed;
            agent.SetDestination(patrolPoints[currentIndex].position);
        }
    }

    void Update()
    {
        if(agent == null || !agent.isOnNavMesh)
            return;

        if(patrolPoints.Length == 0)
            return;

        //パトロール
        if(!agent.pathPending && agent.remainingDistance <= patrolPointArrivalDistance)
        {
            currentIndex = (currentIndex + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentIndex].position);
        }
    }

    public void StopPatrol()
    {
        if(agent != null)
        {
            agent.isStopped = true;
        }
    }
}
