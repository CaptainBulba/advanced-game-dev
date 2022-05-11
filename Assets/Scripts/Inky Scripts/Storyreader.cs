using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

public class Storyreader : MonoBehaviour
{
    [SerializeField]
    private TextAsset inkAsset;
    private Story storyScript;

    public Text dialogBox;
    public Text charName;

    public Image charterIcon;

    void Start()
    {
        LoadStory();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DisplayText();
        }
    }
    void LoadStory()
    {
        storyScript = new Story(inkAsset.text);

        storyScript.BindExternalFunction("Name", (string charName) => ChangeCharNames(charName));
        storyScript.BindExternalFunction("Icon", (string charName) => ChangeIcon(charName));
    }

    public void DisplayText()
    {
        if (storyScript.canContinue) //checking story lines
        {
            string text = storyScript.Continue(); //get next line
            text = text?.Trim();  //remove white space
            dialogBox.text = text;
        }
        else
        {
            dialogBox.text = "End of the story try again";
        }
    }

    public void ChangeCharNames(string name)
    {
        string currentcharName = name;        
        charName.text = currentcharName;
    }

    public void ChangeIcon(string name) 
    {
        
        charterIcon.sprite = Resources.Load<Sprite>("Icons/" + name);

    }
}
