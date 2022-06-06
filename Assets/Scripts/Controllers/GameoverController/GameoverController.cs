using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverController : MonoBehaviour
{
    private string startScene = "StartMenu";

    public AudioClip gameoverMusic;

    void Start()
    {
        MusicController.Instance.ChangeMusic(gameoverMusic);
    }

    public void LaunchStart()
    {
        SceneManager.LoadScene(startScene);
    }
}
