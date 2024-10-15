using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    private string currentAbility = "Fire"; // Default ability

    void Update()
    {
        // Use special ability
        if (Input.GetKeyDown(KeyCode.P))
        {
            UseAbility();
        }

        // Switch abilities using number keys
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchAbility("Fire");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchAbility("Ice");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchAbility("Thunder");
        }
    }

    // Use the currently selected ability
    void UseAbility()
    {
        Debug.Log("Using special ability: " + currentAbility);

        // You can add more logic based on the current ability
        switch (currentAbility)
        {
            case "Fire":
                // Implement Fire ability logic
                Debug.Log("Fire ability used!");
                break;
            case "Ice":
                // Implement Ice ability logic
                Debug.Log("Ice ability used!");
                break;
            case "Thunder":
                // Implement Thunder ability logic
                Debug.Log("Thunder ability used!");
                break;
        }
    }

    // Switch abilities based on input
    void SwitchAbility(string ability)
    {
        currentAbility = ability;
        Debug.Log("Switched to ability: " + currentAbility);
    }
}
