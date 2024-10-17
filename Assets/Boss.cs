using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player;  // Reference to the player's Transform

    public bool isFlipped;  // Track whether the sprite is flipped

    private void Update()
    {
        // Call the LookAtPlayer method every frame
        LookAtPlayer();
    }

    public void LookAtPlayer()
    {
        // Check if the boss is to the right of the player
        if (transform.position.x > player.position.x && isFlipped)
        {
            // Flip the sprite to face the left
            FlipSprite();
            isFlipped = false;
        }
        // Check if the boss is to the left of the player
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            // Flip the sprite to face the right
            FlipSprite();
            isFlipped = true;
        }
    }

    // Flip the sprite by inverting the x-axis scale
    private void FlipSprite()
    {
        Vector3 flipped = transform.localScale;
        flipped.x *= -1;  // Invert the X axis to flip the sprite
        transform.localScale = flipped;
    }
}
