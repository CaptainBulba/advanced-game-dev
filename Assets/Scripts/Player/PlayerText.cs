using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerText : MonoBehaviour
{
	public GameObject textObject;
	public Text playerText;

	public float showTextSpeed;
	private float hideAfter;


    void Start()
    {
		hideAfter = GetComponent<PlayerMovement>().levelController.startPuzzleTime;
		Debug.Log(hideAfter);
	}

    public IEnumerator PlayText(string text)
	{
		playerText.text = null;
		textObject.SetActive(true);

		foreach (char letter in text)
		{
			playerText.text += letter;
			yield return new WaitForSeconds(showTextSpeed);
		}

		yield return new WaitForSeconds(hideAfter);

		textObject.SetActive(false);

	}
}
