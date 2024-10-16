using UnityEngine;
using System.Collections.Generic;

public class PlayerAbility : MonoBehaviour
{
    private List<string> currentAbilities = new List<string>(); // Stores active abilities
    private string activeAbility; // Active ability the player is using

    void Start()
    {
        // Get the selected hero's abilities from the HeroManager
        currentAbilities = HeroManager.instance.GetSelectedHeroAbilities();

        // Set the first ability as the default
        if (currentAbilities.Count > 0)
        {
            activeAbility = currentAbilities[0];
        }
    }

    void Update()
    {
        // Use the active ability
        if (Input.GetKeyDown(KeyCode.P))
        {
            UseAbility();
        }

        // Switch abilities (only if multiple abilities are available)
        if (currentAbilities.Count > 1)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && currentAbilities.Contains("Fire"))
            {
                SwitchAbility("Fire");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && currentAbilities.Contains("Ice"))
            {
                SwitchAbility("Ice");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && currentAbilities.Contains("Thunder"))
            {
                SwitchAbility("Thunder");
            }
        }
    }

    void UseAbility()
    {
        Debug.Log("Using special ability: " + activeAbility);

        // Add logic for each ability
        switch (activeAbility)
        {
            case "Fire":
                Debug.Log("Fire ability used!");
                break;
            case "Ice":
                Debug.Log("Ice ability used!");
                break;
            case "Thunder":
                Debug.Log("Thunder ability used!");
                break;
        }
    }

    void SwitchAbility(string ability)
    {
        activeAbility = ability;
        Debug.Log("Switched to ability: " + activeAbility);
    }

    /*
    // Inherit abilities from dead hero (testing purpose)
    public void InheritHeroAbilities(int deadHeroIndex)
    {
        HeroManager.instance.InheritAbilities(deadHeroIndex);
        currentAbilities = HeroManager.instance.GetSelectedHeroAbilities();
    }
    */
}
