using UnityEngine;
using System.Collections.Generic;

public class HeroManager : MonoBehaviour
{
    public static HeroManager instance;

    [System.Serializable]
    public class Hero
    {
        public string heroName;
        public GameObject heroPrefab; // Prefab of the hero
        public int lives = 3; // Default lives
        public List<string> abilities = new List<string>(); // List of abilities
        public bool isDead = false; // Track if the hero is dead
        public GameObject heroInstance; // The instantiated hero in the level
    }

    public Hero[] heroes; // Array of heroes
    public Hero selectedHero; // The currently selected hero
    private List<string> inheritedAbilities = new List<string>(); // List of inherited abilities
    private Vector2 heroSpawnPosition = new Vector2(0, -0.75f); // Position to spawn hero

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Select and instantiate a hero at the beginning of a level
    public void SelectHero(int heroIndex)
    {
        // If there's already a selected hero instance in the scene, destroy it
        if (selectedHero != null && selectedHero.heroInstance != null)
        {
            Destroy(selectedHero.heroInstance); // Destroy the old hero's instance
            selectedHero.heroInstance = null;   // Reset the reference to null
        }

        selectedHero = heroes[heroIndex];

        if (selectedHero.isDead)
        {
            Debug.LogError("This hero is dead and cannot be selected.");
            return;
        }

        // Instantiate the selected hero's prefab at the desired position if not already instantiated
        if (selectedHero.heroInstance == null)
        {
            selectedHero.heroInstance = Instantiate(selectedHero.heroPrefab, heroSpawnPosition, Quaternion.identity);
            DontDestroyOnLoad(selectedHero.heroInstance);

            // Find the MainCamera and assign the instantiated hero to the CameraFollow script
            CameraFollow cameraFollow = Camera.main.GetComponent<CameraFollow>();
            if (cameraFollow != null)
            {
                cameraFollow.player = selectedHero.heroInstance.transform; // Assign the player's transform to follow the new hero
            }
        }

        Debug.Log("Selected hero: " + selectedHero.heroName);
    }


    // Get the selected hero's abilities (including inherited ones)
    public List<string> GetSelectedHeroAbilities()
    {
        List<string> allAbilities = new List<string>(selectedHero.abilities);
        allAbilities.AddRange(inheritedAbilities);
        return allAbilities;
    }

    // Reduce hero's life and kill if lives reach 0
    public void ReduceHeroLife(int heroIndex)
    {
        Hero hero = heroes[heroIndex];

        if (hero.lives > 0)
        {
            hero.lives--;
            Debug.Log(hero.heroName + " lost a life. Remaining lives: " + hero.lives);
        }

        if (hero.lives <= 0 && !hero.isDead)
        {
            KillHero(heroIndex);
        }
    }

    // Mark the hero as dead but don't destroy immediately (wait for abilities to be inherited)
    public void KillHero(int heroIndex)
    {
        Hero hero = heroes[heroIndex];
        if (!hero.isDead)
        {
            hero.isDead = true;
            Debug.Log(hero.heroName + " is dead.");
        }

        // Check if all heroes are dead (Game Over)
        if (IsGameOver())
        {
            Debug.LogError("All heroes are dead. Game Over!");
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
        }
    }

    // Inherit abilities from a dead hero and destroy the dead hero after inheritance
    public void InheritAbilities(int deadHeroIndex, int inheritingHeroIndex)
    {
        Hero deadHero = heroes[deadHeroIndex];
        Hero inheritingHero = heroes[inheritingHeroIndex];

        if (deadHero.isDead && deadHero.abilities.Count > 0)
        {
            inheritedAbilities.AddRange(deadHero.abilities);
            Debug.Log("Inherited abilities from " + deadHero.heroName + " to " + inheritingHero.heroName);

            // Destroy the dead hero instance now that abilities are inherited
            if (deadHero.heroInstance != null)
            {
                Destroy(deadHero.heroInstance);
                deadHero.heroInstance = null;
            }
        }
    }

    // Check if all heroes are dead (for Game Over)
    public bool IsGameOver()
    {
        foreach (var hero in heroes)
        {
            if (!hero.isDead) return false;
        }
        return true;
    }

    // Reset hero states (for a new game or reset), keeping the first defined ability
    public void ResetHeroes()
    {
        foreach (var hero in heroes)
        {
            hero.lives = 3;
            hero.isDead = false;

            // Clear all inherited abilities but keep the first defined ability
            if (hero.abilities.Count > 1)
            {
                string firstAbility = hero.abilities[0]; // Save the first ability
                hero.abilities.Clear();
                hero.abilities.Add(firstAbility); // Re-add the first ability
            }

            // Reset hero instance if necessary (destroy if already exists)
            if (hero.heroInstance != null)
            {
                Destroy(hero.heroInstance);
                hero.heroInstance = null;
            }
        }

        inheritedAbilities.Clear();
    }
}
