using UnityEngine;

public class RangedEnemy : EnemyBase
{
    [Header("Ranged Settings")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float fireCooldown = 2f;
    [SerializeField] private float fireRange = 10f;
    [SerializeField] private Transform firePoint;

    private float lastFireTime = 0f;

    protected override void Update()
    {
        base.Update();

        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // If player is in range and cooldown is done, shoot
        if (distance <= fireRange && Time.time >= lastFireTime + fireCooldown)
        {
            Shoot();
            lastFireTime = Time.time;
        }
    }

    private void Shoot()
    {
        // Create a bullet and aim at the player
        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        // Get direction to player and normalize it
        Vector3 direction = (player.position - firePoint.position).normalized;

        // Add force to the bullet
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = direction * 20f; // Bullet speed
        }

        Debug.Log("Ranged enemy fired a projectile!");
    }
}

