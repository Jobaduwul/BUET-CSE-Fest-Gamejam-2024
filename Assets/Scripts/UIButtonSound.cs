using UnityEngine;
using UnityEngine.UI;

public class UIButtonSound : MonoBehaviour
{
    public AudioClip buttonClickSound;  // Drag your button click sound here
    private AudioSource audioSource;

    private void Start()
    {
        // Get the AudioSource component from the object where the script is attached
        audioSource = GetComponent<AudioSource>();
    }

    // Method to be called when the button is clicked
    public void PlayButtonClickSound()
    {
        if (buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);  // Play the button click sound
        }
    }
}
