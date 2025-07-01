using UnityEngine;
public class MeleeEnemy : EnemyBase
{
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackDamage = 20f;
    [SerializeField] private float attackCooldown = 1.5f;

    // Tracks when the last attack happened
    private float lastAttackTime = 0f;

    // Override the base Update to add attack behavior
    protected override void Update()
    {
        // Run the movement from EnemyBase
        base.Update();

        if (player == null) return;

        // How far enemy is away from player
        float distance = Vector3.Distance(transform.position, player.position);

        // if player is in the range with enough time then attack
        if (distance <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            AttackPlayer();
            lastAttackTime = Time.time;
        }
    }

    private void AttackPlayer()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

        // If player has a health component, deal damage
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
            Debug.Log("Melee enemy dealt " + attackDamage + " damage to the player"); //had to debug, player won't take damage for some reason.
        }
    }
}
