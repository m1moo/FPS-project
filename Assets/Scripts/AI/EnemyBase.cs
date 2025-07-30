using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] protected float moveSpeed = 3.5f;
    [SerializeField] protected float health = 100f;

    protected Transform player;
    protected NavMeshAgent agent;

    private Renderer enemyRenderer;
    private Color originalColor;
    private bool isFlashing = false;
    private float flashDuration = 0.2f;

    protected virtual void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        agent = GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            agent.speed = moveSpeed;
        }

        enemyRenderer = GetComponentInChildren<Renderer>();
        if (enemyRenderer != null)
        {
            originalColor = enemyRenderer.material.color;
        }
    }

    protected virtual void Update()
    {
        // had to update movement so it is controlled by EnemyAIState, so it doesn't just automatically walk towards the player if we press play
       
    }

    public void MoveTowardsPlayer()
    {
        if (agent != null && player != null)
        {
            agent.SetDestination(player.position);
        }
    }

    public virtual void TakeDamage(float damage)
    {
        if (!isFlashing)
        {
            StartCoroutine(FlashRed());
        }

        health -= damage;

        Debug.Log(gameObject.name + " took " + damage + " damage. HP left: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    private IEnumerator FlashRed()
    {
        if (enemyRenderer == null) yield break;

        isFlashing = true;
        enemyRenderer.material.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        enemyRenderer.material.color = originalColor;
        isFlashing = false;
    }

    protected virtual void Die()
    {
        Debug.Log(gameObject.name + " has died");
        Destroy(gameObject);
    }
}
