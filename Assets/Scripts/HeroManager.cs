using UnityEngine;
using System.Collections.Generic;

public class HeroManager : MonoBehaviour
{
    public static HeroManager instance;

    [System.Serializable]
    public class Hero
    {
        public string heroName;
        public GameObject heroPrefab;
        public int lives = 3;
        public List<string> abilities = new List<string>();
        public GameObject heroInstance;
    }

    public Hero[] heroes;
    public Hero selectedHero;
    private Vector2 heroSpawnPosition = new Vector2(0, -0.75f);

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SelectHero(int heroIndex)
    {
        if (selectedHero != null && selectedHero.heroInstance != null)
        {
            Destroy(selectedHero.heroInstance);
            selectedHero.heroInstance = null;
        }

        selectedHero = heroes[heroIndex];

        if (selectedHero.heroInstance == null)
        {
            selectedHero.heroInstance = Instantiate(selectedHero.heroPrefab, heroSpawnPosition, Quaternion.identity);
            DontDestroyOnLoad(selectedHero.heroInstance);

            CameraFollow cameraFollow = Camera.main.GetComponent<CameraFollow>();
            if (cameraFollow != null)
            {
                cameraFollow.player = selectedHero.heroInstance.transform;
            }

            // Attach the HeroCollisionHandler to the instantiated hero for collision handling
            HeroCollisionHandler collisionHandler = selectedHero.heroInstance.AddComponent<HeroCollisionHandler>();
            collisionHandler.heroIndex = heroIndex;
        }

        Debug.Log("Selected hero: " + selectedHero.heroName);
    }

    public List<string> GetSelectedHeroAbilities()
    {
        return new List<string>(selectedHero.abilities);
    }

    public void ReduceHeroLife(int heroIndex)
    {
        Hero hero = heroes[heroIndex];

        if (hero.lives > 1)
        {
            hero.lives--;
            Debug.Log(hero.heroName + " lost a life. Remaining lives: " + hero.lives);
        }
        else
        {
            Debug.Log(hero.heroName + " is at minimum life and cannot lose more lives.");
        }
    }

    public bool IsGameOver()
    {
        foreach (var hero in heroes)
        {
            if (hero.lives > 1) return false;
        }
        return true;
    }

    public void ResetHeroes()
    {
        foreach (var hero in heroes)
        {
            hero.lives = 3;

            if (hero.abilities.Count > 1)
            {
                string firstAbility = hero.abilities[0];
                hero.abilities.Clear();
                hero.abilities.Add(firstAbility);
            }

            if (hero.heroInstance != null)
            {
                Destroy(hero.heroInstance);
                hero.heroInstance = null;
            }
        }
    }
}

// This script handles the collision detection for the hero
public class HeroCollisionHandler : MonoBehaviour
{
    public int heroIndex;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hero collided with enemy: " + collision.gameObject.name);

            // Call the HeroManager to reduce the hero's life
            HeroManager.instance.ReduceHeroLife(heroIndex);
        }
    }
}
