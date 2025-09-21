using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string gameTutorialSceneName = "Tutorial";
    [SerializeField] private string gameLevelSceneName = "Level1";

    public void StartTutorial()
    {
        SceneManager.LoadScene(gameTutorialSceneName);
    }
    public void StartGame()
    {

        SceneManager.LoadScene(gameLevelSceneName);
    }

    public void ExitGame()
    {
        Debug.Log("Non ti arrendere, serve DETERMINAZIONE");

        Application.Quit();

    }
}
