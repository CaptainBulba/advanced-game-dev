using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Gametime : MonoBehaviour
{
    public float timer = 60;
    bool switchScene = false;
    public Text timerbox;
    


    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        DisplayTime(timer);

        //codition for switch scene
        if (timer <= 50 && !switchScene)
        {
            switchScene = true;
            SceneManager.LoadScene("Scene 2");
        }
        
        //timerbox.text = timer.ToString();
    }
    void DisplayTime(float timefordisplay)
    {
        float minute = Mathf.FloorToInt(timefordisplay / 60);
        float second = Mathf.FloorToInt(timefordisplay % 60);

        timerbox.text = string.Format("{0:00}:{1:00}", minute, second);
    }
}
