using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float horizontalOffset;
    public float leftLimit = -10f;
    public float rightLimit = 10f;

    private float originalYPosition;

    void Start()
    {
        originalYPosition = transform.position.y;

        // Initialize the player reference at the start
        UpdatePlayerReference();
    }

    void LateUpdate()
    {
        if (player != null) // Check if the player is assigned
        {
            Vector3 playerPosition = player.position;

            if (playerPosition.x > leftLimit && playerPosition.x < rightLimit)
            {
                transform.position = new Vector3(playerPosition.x + horizontalOffset, originalYPosition, transform.position.z);
            }
        }
        else
        {
            UpdatePlayerReference(); // Try to update player reference if it's null
        }
    }

    // Method to update the player reference
    private void UpdatePlayerReference()
    {
        if (HeroManager.instance != null && HeroManager.instance.selectedHero != null)
        {
            player = HeroManager.instance.selectedHero.heroInstance?.transform; // Use the null-conditional operator
        }
    }
}

