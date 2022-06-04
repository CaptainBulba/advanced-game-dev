using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelZeroController : MonoBehaviour
{
    private Vector2 startPosition;
    public Vector2 targetPosition;

    public float moveTime;
    private float timeElapsed;

    public GameObject continueButton;
    public RectTransform storyText;

    public AudioClip levelMusic;

    private bool moveText = true;

    private string levelOneScene = "level-1";

    void Start()
    {
        MusicController.Instance.ChangeMusic(levelMusic);
        startPosition = storyText.anchoredPosition;
    }

    void Update()
    {
        if (moveText)
        {
            if (timeElapsed < moveTime)
            {
                storyText.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, timeElapsed / moveTime);
                timeElapsed += Time.deltaTime;
            }
            else
            {
                timeElapsed = 0f;
                continueButton.SetActive(true);
                moveText = false;
            }
        }
    }

    public void ContinueButton()
    {
        SceneManager.LoadScene(levelOneScene);
    }
}
