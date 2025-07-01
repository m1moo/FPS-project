using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button restartButton;
    public PlayerHealth playerHealth;
    public EnemyBase[] enemies; // Assign your melee and ranged enemies here

    private bool gameOver = false;

    void Start()
    {
        restartButton.gameObject.SetActive(false);
    }

    void Update()
    {
        if (gameOver) return;

        // Check if player is dead
        if (playerHealth.CurrentHealth <= 0)
        {
            GameOver();
        }
        // Check if all enemies are dead
        else if (AllEnemiesDead())
        {
            GameOver();
        }
    }

    bool AllEnemiesDead()
    {
        foreach (var enemy in enemies)
        {
            if (enemy != null) return false;
        }
        return true;
    }

    void GameOver()
    {
        gameOver = true;
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

