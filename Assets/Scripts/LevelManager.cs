using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject levelCompletePanel; // Drag the level complete panel from the UI
    public int currentLevel;
    public int enemiesRequiredToKill = 5; // Number of enemies required to complete the level
    private int enemiesKilled = 0;

    private void Update()
    {
        // Test level completion by pressing 'C' or when all enemies are killed
        if (Input.GetKeyDown(KeyCode.C) || enemiesKilled >= enemiesRequiredToKill)
        {
            CompleteLevel();
        }
    }

    public void EnemyDefeated()
    {
        enemiesKilled++;
        Debug.Log("Enemies killed: " + enemiesKilled + "/" + enemiesRequiredToKill);

        if (enemiesKilled >= enemiesRequiredToKill)
        {
            CompleteLevel();
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
