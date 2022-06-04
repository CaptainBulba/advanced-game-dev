using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour
{
    private string startScene = "StartMenu";
    private string levelZeroScene = "Level-0";
    private string CreditsScene = "Credits";

    public void LaunchGame()
    {
        SceneManager.LoadScene(levelZeroScene);
    }

    public void LaunchCredits()
    {
        SceneManager.LoadScene(CreditsScene);
    }

    public void LaunchStart()
    {
        SceneManager.LoadScene(startScene);
    }
}
