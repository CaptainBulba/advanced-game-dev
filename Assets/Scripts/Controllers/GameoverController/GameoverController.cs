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
        Destroy(GameTime.Instance.gameObject);
    }

    public void LaunchStart()
    {
        SceneManager.LoadScene(startScene);
    }
}
