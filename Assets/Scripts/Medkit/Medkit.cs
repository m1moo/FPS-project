using UnityEngine;

public class Medkit : MonoBehaviour
{
    [SerializeField] private float healAmount = 50f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.Heal(healAmount);
                Debug.Log("player healed");
                Destroy(gameObject); // Remove medkit after use
            }
        }
    }
}
