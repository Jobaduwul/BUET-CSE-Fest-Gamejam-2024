using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Import TextMeshPro namespace

public class LevelSelectionController : MonoBehaviour
{
    public Button prologueButton;
    public Button[] levelButtons;
    public Button finalLevelButton;
    public TextMeshProUGUI finalLevelText; // TextMeshPro for the final level text

    private void Start()
    {
        LoadLevelProgress();
    }

    void LoadLevelProgress()
    {
        bool isPrologueCompleted = true;
        //bool isPrologueCompleted = PlayerPrefs.GetInt("PrologueCompleted", 0) == 1;
        int unlockedLevels = PlayerPrefs.GetInt("UnlockedLevels", 0);

        prologueButton.interactable = true;

        // Handle level button interaction
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i == 0 && isPrologueCompleted)
            {
                levelButtons[i].interactable = true;
            }
            else if (i > 0 && i <= unlockedLevels)
            {
                levelButtons[i].interactable = true;
            }
            else
            {
                levelButtons[i].interactable = false;
            }
        }

        // If the final level is unlocked
        if (unlockedLevels >= levelButtons.Length)
        {
            finalLevelButton.interactable = true;
            finalLevelText.text = "Final Level";
        }
        else
        {
            finalLevelButton.interactable = false;
            finalLevelText.text = "???";
        }
    }

    public void LoadPrologue()
    {
        SceneManager.LoadScene("PrologueScene");
    }

    public void CompletePrologue()
    {
        PlayerPrefs.SetInt("PrologueCompleted", 1);
        PlayerPrefs.SetInt("UnlockedLevels", 0);
        LoadLevelProgress();
    }

    public void LoadFinalBossLevel()
    {
        SceneManager.LoadScene("FinalBossScene");
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
        HeroManager.instance.ResetHeroes(); // Reset all hero states
        LoadLevelProgress();
    }

}
