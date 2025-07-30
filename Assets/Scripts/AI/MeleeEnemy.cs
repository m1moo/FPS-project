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
        base.Update();

        if (player == null) return;

        // Get the AI state to check if meelee can attack
        EnemyAIState aiState = GetComponent<EnemyAIState>();
        if (aiState == null || aiState.GetCurrentState() != EnemyAIState.State.Alert) return;

        float distance = Vector3.Distance(transform.position, player.position);

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
