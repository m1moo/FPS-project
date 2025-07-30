using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyBase))]
[RequireComponent(typeof(EnemyVision))]
public class EnemyAIState : MonoBehaviour
{
    public enum State { Idle, Alert, Investigating }

    [Header("State Settings")]
    [SerializeField] private State currentState = State.Idle;

    private EnemyBase enemyBase;
    private EnemyVision enemyVision;
    private NavMeshAgent agent;
    private Transform player;

    private Vector3 investigateTarget;
    private bool hasInvestigateTarget = false;

    private void Start()
    {
        enemyBase = GetComponent<EnemyBase>();
        enemyVision = GetComponent<EnemyVision>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player")?.transform;

        ChangeState(State.Idle);
    }

    private void Update()
    {
        if (currentState == State.Alert)
        {
            if (!enemyVision.IsPlayerVisible())
            {
                // If they lose sight during chase, stop and wait
                ChangeState(State.Idle);
            }
        }
        else if (currentState == State.Idle && hasInvestigateTarget)
        {
            // Move to the investigate point set by the camera
            ChangeState(State.Investigating);
        }

        // If enemy can see the player at any time, force Alert
        if (enemyVision.IsPlayerVisible())
        {
            ChangeState(State.Alert);
        }

        HandleBehavior();
    }

    private void HandleBehavior()
    {
        switch (currentState)
        {
            case State.Idle:
                agent.SetDestination(transform.position); // Stay in place
                break;

            case State.Investigating:
                if (hasInvestigateTarget)
                {
                    agent.SetDestination(investigateTarget);

                    // Stop investigating if reached target
                    if (Vector3.Distance(transform.position, investigateTarget) < 1.5f)
                    {
                        hasInvestigateTarget = false;
                        ChangeState(State.Idle);
                    }
                }
                break;

            case State.Alert:
                if (player != null)
                {
                    enemyBase.MoveTowardsPlayer(); // Chase the player
                }
                break;
        }
    }

    private void ChangeState(State newState)
    {
        if (currentState == newState) return;

        currentState = newState;
        Debug.Log(gameObject.name + " state changed to " + currentState);
    }

    // Called when the camera alerts this enemy
    public void ForceAlert(Vector3 alertPosition)
    {
        investigateTarget = alertPosition;
        hasInvestigateTarget = true;
        ChangeState(State.Investigating);
        Debug.Log(gameObject.name + " camera alert location " + alertPosition);
    }

    public State GetCurrentState() => currentState;
}

