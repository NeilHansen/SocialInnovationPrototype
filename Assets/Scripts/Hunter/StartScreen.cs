using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour {

    public Sprite[] ContinueButton;
    [SerializeField]
    Button CB;
   
  
    

	// Use this for initialization
	void Awake () {
        Time.timeScale = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BeginGame()
    {
       gameObject.SetActive(false);
       Time.timeScale = 1.0f;
       FindObjectOfType<AudioManager>().PlaySound("music");
    }

    public void SwitchContinueButton(bool Hovered)
    {
        if (Hovered)
        {
            CB.GetComponent<Image>().sprite = ContinueButton[1];
        }
        else
        {
            CB.GetComponent<Image>().sprite = ContinueButton[0];
        }
    }
}
