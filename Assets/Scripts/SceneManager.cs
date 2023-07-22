using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SceneManager : MonoBehaviour
{
    public void LoadNextScene()
    {
        var nextSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1;
        var scenesAmount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;

        if (nextSceneIndex < scenesAmount)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
