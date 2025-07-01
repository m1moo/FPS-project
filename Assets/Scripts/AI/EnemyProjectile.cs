using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float destroyDelay = 5f;

    private void Start()
    {
        // Destroy the projectile after a few seconds so it doesn't stay there forever
        Destroy(gameObject, destroyDelay);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if hit player
        if (other.CompareTag("Player"))
        {
            // Try to get PlayerHealth script
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            // If found, deal damage
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log("Player hit by projectile! Damage: " + damage);
            }

            // Destroy the projectile on hit
            Destroy(gameObject);
        }
    }
}
