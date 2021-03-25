using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent enemyNavMeshAgent;
    private bool isPaused = false;
    private float stoppingDistance;

    public float StoppingDistance
    {
        private get
        {
            return stoppingDistance;
        }

        set
        {
            stoppingDistance = value;
            enemyNavMeshAgent.stoppingDistance = value;
        }
    }

    public Transform Target { private get; set; }


    private void Awake()
    {
        enemyNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        if (isPaused || Target == null)
        {
            return;
        }

        if (Vector3.Distance(transform.position, Target.position) < StoppingDistance)
        {
            ReachedTarget?.Invoke(transform.position, Resume);
            isPaused = true;
        }
        else
        {
            enemyNavMeshAgent.destination = Target.position;
        }
    }

    private void Resume()
    {
        isPaused = false;
    }


    public event Action<Vector3, Action> ReachedTarget;
}
