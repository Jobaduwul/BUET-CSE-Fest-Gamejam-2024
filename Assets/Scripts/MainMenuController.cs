using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject levelSelectionPanel;
    public GameObject heroSelectionPanel; // Added for hero selection

    public void PlayGame()
    {
        mainMenuPanel.SetActive(false);
        levelSelectionPanel.SetActive(true);
        heroSelectionPanel.SetActive(false);
    }

    public void SelectLevel(int levelIndex)
    {
        // Move to hero selection after selecting the level
        levelSelectionPanel.SetActive(false);
        heroSelectionPanel.SetActive(true);

        // Save selected level for when hero is chosen
        PlayerPrefs.SetInt("SelectedLevel", levelIndex);
    }

    public void SelectHero(int heroIndex)
    {
        // Set the selected hero without instantiating
        HeroManager.instance.SelectHero(heroIndex);

        // Load the selected level after hero selection
        int levelIndex = PlayerPrefs.GetInt("SelectedLevel", 0);
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleLevel" + levelIndex);
    }

    public void MainMenu()
    {
        mainMenuPanel.SetActive(true);
        levelSelectionPanel.SetActive(false);
        heroSelectionPanel.SetActive(false);
    }

    public void LoadMainMenu()
    {
        // Check if there is an instantiated hero and destroy it if it exists
        if (HeroManager.instance != null && HeroManager.instance.selectedHero != null
            && HeroManager.instance.selectedHero.heroInstance != null)
        {
            Destroy(HeroManager.instance.selectedHero.heroInstance);
            HeroManager.instance.selectedHero.heroInstance = null; // Ensure reference is cleared
        }

        // Load the Main Menu scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuHeroChoosing");
    }



    public void ExitGame()
    {
        Application.Quit();
    }
}
