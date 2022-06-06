using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelZeroController : MonoBehaviour
{
    //private Vector2 startPosition;
    //public Vector2 targetPosition;

    //public float moveTime;
    

    //private float timeElapsed;

    public AudioClip levelMusic;
    public GameObject continueButton;
    public RectTransform storyTextRect;
    public float wordPause;

    private Text contentText;
    private string levelOneScene = "level-1";
    private string introText;

    //private bool moveText = true;



    void Start()
    {
        MusicController.Instance.ChangeMusic(levelMusic);
        
        continueButton.SetActive(false);

        PrepareIntroText();

        //continueButton.SetActive(true);
    }


    public void PrepareIntroText()
    {
        contentText = storyTextRect.GetComponent<Text>();
        introText = contentText.text;

        StartCoroutine(AnimateWords());

        

    }

    IEnumerator AnimateWords()
    {
        string[] words = introText.Split(' ');
        contentText.text = words[0];
        for (int i = 1; i < words.Length; ++i)
        {
            yield return new WaitForSeconds(wordPause);
            contentText.text += " " + words[i];
        }

        continueButton.SetActive(true);
    }
    

    public void ContinueButton()
    {
        SceneManager.LoadScene(levelOneScene);
    }
}
