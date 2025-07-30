using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 45f;      // For Ayush: How fast the camera rotates
    [SerializeField] private float rotationAngle = 45f;      // For Ayush: Angle

    [Header("Detection Settings")]
    [SerializeField] private float detectionRange = 15f;     // For Ayush: Max distance the camera can detect the player
    [SerializeField] private float fieldOfView = 90f;        // For Ayush: Camera vision, FOV so cone
    [SerializeField] private LayerMask obstructionMask;      // For Ayush: Layers that block vision, like the walls as a obstacle 

    private Transform player;                                
    private float startYRotation;                           
    private bool rotatingRight = true;                       // For Ayush: Whether the camera is currently rotating right
    private bool playerDetected = false;                     // For Ayush: checking if the player has been detected or not.

    private void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        startYRotation = transform.eulerAngles.y;

        if (player == null)
        {
            Debug.LogError(name + " Player not found ");
        }
    }

    private void Update()
    {
        RotateCamera();      // Continuously rotate camera
        DetectPlayer();      // Check if player is in the  vision
    }

    // This is the camera rotation function
    private void RotateCamera()
    {
        float targetAngle = startYRotation + (rotatingRight ? rotationAngle : -rotationAngle);
        float newY = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, newY, transform.eulerAngles.z);

        // Switch direction when reaching the target angle
        if (Mathf.Approximately(newY, targetAngle))
        {
            rotatingRight = !rotatingRight;
        }
    }

    // Uses raycasting and FOV to detect the player
    private void DetectPlayer()
    {
        if (player == null) return;

        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, player.position);
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float angleToPlayer = Vector3.Angle(forward, directionToPlayer);

        bool canSeePlayer = false;

        // Check if player is within FOV and unobstructed
        if (distance <= detectionRange && angleToPlayer <= fieldOfView / 2f)
        {
            if (!Physics.Raycast(transform.position, directionToPlayer, distance, obstructionMask))
            {
                canSeePlayer = true;
            }
        }

        // when detected alert all enemies
        if (canSeePlayer && !playerDetected)
        {
            playerDetected = true;
            Debug.Log(name + " Alert, player detected ");

            // Loop through all enemies and alert them
            EnemyAIState[] allEnemies = FindObjectsOfType<EnemyAIState>();
            foreach (var enemy in allEnemies)
            {
                enemy.ForceAlert(player.position);
            }
        }
        // If the player is no longer visible, reset detection
        else if (!canSeePlayer && playerDetected)
        {
            playerDetected = false;
            Debug.Log(name + " lost sight of the player.");
        }
    }

    // Draws vision cone rays in the Scene view for debugging
    private void OnDrawGizmos()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);

        // Draw FOV cone lines
        Gizmos.color = Color.cyan;
        Quaternion leftRot = Quaternion.AngleAxis(-fieldOfView / 2f, Vector3.up);
        Quaternion rightRot = Quaternion.AngleAxis(fieldOfView / 2f, Vector3.up);
        Gizmos.DrawRay(transform.position, leftRot * forward * detectionRange);
        Gizmos.DrawRay(transform.position, rightRot * forward * detectionRange);

        // Draw center line
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, forward * detectionRange);
    }
}

