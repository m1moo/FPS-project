using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    [Header("Enemy Settings")] // just a header so I keep track
    [SerializeField] protected float moveSpeed = 3.5f;
    [SerializeField] protected float health = 100f;

    protected Transform player;
    protected NavMeshAgent agent;

    private Renderer enemyRenderer;
    private Color originalColor;
    private bool isFlashing = false;
    private float flashDuration = 0.2f; // duration enemy stays red

    // Called when enemy spawns
    protected virtual void Start()
    {
        // Find the player in scene with tag
        player = GameObject.FindWithTag("Player")?.transform;

        // Get the NavMeshAgent component on this enemy
        agent = GetComponent<NavMeshAgent>();

        // Get the renderer to change color on hit
        enemyRenderer = GetComponentInChildren<Renderer>();
        if (enemyRenderer != null)
        {
            originalColor = enemyRenderer.material.color;
        }
    }

    protected virtual void Update()
    {
        // If player is not found, do nothing
        if (player == null) return;

        // Move toward the player
        MoveTowardsPlayer();
    }

    // Move enemy toward the players position
    protected void MoveTowardsPlayer()
    {
        agent.SetDestination(player.position);
    }

    // Apply damage to the enemy
    public virtual void TakeDamage(float damage)
    {
        // Start flash coroutine if not already flashing
        if (!isFlashing)
        {
            StartCoroutine(FlashRed());
        }

        health -= damage;

        Debug.Log(gameObject.name + " took " + damage + " damage. HP left: " + health);

        // If health reaches zero or below, destroy the enemy
        if (health <= 0)
        {
            Die();
        }
    }

    // Coroutine to make enemy flash red briefly
    private IEnumerator FlashRed()
    {
        if (enemyRenderer == null) yield break;

        isFlashing = true;
        enemyRenderer.material.color = Color.red; // Turn red
        yield return new WaitForSeconds(flashDuration); // Wait
        enemyRenderer.material.color = originalColor; // Restore color
        isFlashing = false;
    }

    // Destroy the enemy
    protected virtual void Die()
    {
        Debug.Log(gameObject.name + " has died");
        Destroy(gameObject);
    }
}
