using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoNextScene : MonoBehaviour
{
    public float delayTime = 6f;

    void Start()
    {
        StartCoroutine(LoadMainMenuAfterDelayCoroutine());
    }

    IEnumerator LoadMainMenuAfterDelayCoroutine()
    {
        yield return new WaitForSeconds(delayTime);

        SceneManager.LoadScene("MainMenuHeroChoosing");
    }
}
