using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameTime : MonoBehaviour
{
    public float timer = 60;
    public Text timerText;

    public static GameTime Instance { get; private set; }

    private string gameoverScene = "Gameover";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
            Destroy(gameObject);
    }

    void Update()
    {
        timer -= Time.deltaTime;
        DisplayTime(timer);
    }

    void DisplayTime(float timefordisplay)
    {
        float minute = Mathf.FloorToInt(timefordisplay / 60);
        float second = Mathf.FloorToInt(timefordisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minute, second);

        if (timer <= 1)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(gameoverScene);
        }
    }
}
