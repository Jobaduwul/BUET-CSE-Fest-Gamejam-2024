using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;    // Array to store enemy prefabs
    public float spawnInterval;     // Time interval between spawns
    public float spawnOffset;     // Offset from the camera's position for spawning enemies

    private float timer = 0f;            // Timer to keep track of spawn intervals

    void Update()
    {
        // Update the spawn timer
        timer += Time.deltaTime;

        // Check if it's time to spawn a new enemy
        if (timer >= spawnInterval)
        {
            SpawnEnemy();    // Call the enemy spawning method
            timer = 0f;      // Reset the timer after spawning
        }
    }

    void SpawnEnemy()
    {
        // Select a random enemy from the array
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject selectedEnemy = enemyPrefabs[randomIndex];

        // Get the main camera's position
        Vector3 cameraPosition = Camera.main.transform.position;

        // Determine a random spawn position (x and y) based on the camera's position
        float xPosition = Random.Range(0, 2) == 0 ? cameraPosition.x - spawnOffset : cameraPosition.x + spawnOffset;  // Spawn 5.5 units left or right from the camera
        float yPosition = Random.Range(-1.5f, -0.5f);                                                                // Random y between -1.5 and -0.5

        Vector2 spawnPosition = new Vector2(xPosition, yPosition);

        // Instantiate the selected enemy at the random position
        GameObject spawnedEnemy = Instantiate(selectedEnemy, spawnPosition, Quaternion.identity);
    }
}
