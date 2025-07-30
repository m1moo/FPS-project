using UnityEngine;

[RequireComponent(typeof(EnemyBase))]
public class EnemyVision : MonoBehaviour
{
    [Header("Vision Settings")]
    [SerializeField] private float detectionRange = 20f;
    [SerializeField] private float fieldOfView = 120f;
    [SerializeField] private LayerMask obstructionMask;

    private Transform player;
    private bool playerInSight = false;

    private void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogError(gameObject.name + " player not found ");
        }
    }

    private void Update()
    {
        if (player == null) return;
        UpdateVisibility();
    }

    private void UpdateVisibility()
    {
        bool seenNow = false;

        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, player.position);
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float angleToPlayer = Vector3.Angle(forward, directionToPlayer);

        if (distance <= detectionRange && angleToPlayer <= fieldOfView / 2f)
        {
            if (!Physics.Raycast(transform.position + Vector3.up, directionToPlayer, distance, obstructionMask))
            {
                seenNow = true;
            }
        }

        if (seenNow && !playerInSight)
        {
            Debug.Log(gameObject.name + " found the player ");
        }
        else if (!seenNow && playerInSight)
        {
            Debug.Log(gameObject.name + " Lost sight ");
        }

        playerInSight = seenNow;
    }

    public bool IsPlayerVisible() => playerInSight;

    // Draw vision cone and sight line even when not playing
    private void OnDrawGizmos()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);

        // Draw cone boundaries
        Gizmos.color = Color.cyan;
        Quaternion leftRot = Quaternion.AngleAxis(-fieldOfView / 2f, Vector3.up);
        Quaternion rightRot = Quaternion.AngleAxis(fieldOfView / 2f, Vector3.up);
        Gizmos.DrawRay(transform.position + Vector3.up, leftRot * forward * detectionRange);
        Gizmos.DrawRay(transform.position + Vector3.up, rightRot * forward * detectionRange);

        // Draw line toward player
        if (player != null)
        {
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            float dist = Vector3.Distance(transform.position, player.position);

            if (Physics.Raycast(transform.position + Vector3.up, dirToPlayer, dist, obstructionMask))
                Gizmos.color = Color.red; // blocked
            else
                Gizmos.color = Color.green; // clear

            Gizmos.DrawRay(transform.position + Vector3.up, dirToPlayer * Mathf.Min(dist, detectionRange));
        }
        else
        {
            // Draw range line 
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position + Vector3.up, forward * detectionRange);
        }
    }
}








