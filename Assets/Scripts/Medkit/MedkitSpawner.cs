using UnityEngine;

public class MedkitSpawner : MonoBehaviour
{
    [SerializeField] private GameObject medkitPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private PlayerHealth playerHealth;

    private bool hasSpawned = false;

    private void Update()
    {
        if (!hasSpawned && playerHealth.CurrentHealth <= 50)
        {
            Instantiate(medkitPrefab, spawnPoint.position, Quaternion.identity);
            hasSpawned = true;
            Debug.Log("Medkit spawned");
        }
    }
}
