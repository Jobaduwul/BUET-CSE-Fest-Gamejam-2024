using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth; // Initialize the enemy's health
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reduce health by damage amount
        Debug.Log(gameObject.name + " took " + damage + " damage. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die(); // Destroy the enemy if health is 0 or less
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " has been defeated.");
        // Notify LevelManager that an enemy has been killed
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        if (levelManager != null)
        {
            levelManager.EnemyDefeated();
        }

        Destroy(gameObject); // Destroy the enemy GameObject
    }
}
