using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] private float damage = 25f;       // Damage dealt to enemy
    [SerializeField] private float destroyDelay = 5f;  // Bullet auto destroys after time

    private void Start()
    {
        // Destroy the bullet after a few seconds to clean up
        Destroy(gameObject, destroyDelay);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if an enemy is hit
        EnemyBase enemy = other.GetComponent<EnemyBase>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Debug.Log("Hit " + enemy.gameObject.name + " for " + damage + " damage!");
        }

        // Destroy the bullet after hitting anything
        Destroy(gameObject);
    }
}

