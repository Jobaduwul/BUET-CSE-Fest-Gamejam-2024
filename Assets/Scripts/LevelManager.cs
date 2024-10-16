using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject levelCompletePanel; // Drag the level complete panel from the UI
    public int currentLevel;

    private void Update()
    {
        // Test level completion by pressing 'C'
        if (Input.GetKeyDown(KeyCode.C))
        {
            CompleteLevel();
            Debug.Log("Level " + currentLevel + " completed! Next level unlocked.");
        }
    }

    public void CompleteLevel()
    {
        int unlockedLevels = PlayerPrefs.GetInt("UnlockedLevels", 0);
        if (currentLevel > unlockedLevels)
        {
            PlayerPrefs.SetInt("UnlockedLevels", currentLevel);
            PlayerPrefs.Save();  // Saves progress to disk
        }

        // Show level complete panel
        ShowLevelCompletePanel();
    }

    private void ShowLevelCompletePanel()
    {
        levelCompletePanel.SetActive(true); // Activate the panel
    }
}
