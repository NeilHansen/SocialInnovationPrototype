using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelectUI : MonoBehaviour {

    [System.Serializable]
    struct GameModeButtons
    {
        
       public Button[] GameModes;

       
       public  Sprite[] NormalImages;

        
       public Sprite[] HoverImages;

       public Animator[] Anims;
    }

    [SerializeField]
    Button ExitButton;
   
    [SerializeField]
    Sprite[] ExitSprites;

   [SerializeField]
    GameModeButtons MenuObjects;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void HoverButton(int bn)
    {

        MenuObjects.GameModes[bn].image.sprite = MenuObjects.HoverImages[bn];

        MenuObjects.Anims[bn].SetBool("Hovered", true);

       // GameModes[Gamemode].image.sprite = HoverImages[Gamemode];


    }

    public void UnhoverButtons(int unhoverNum)
    {
        MenuObjects.GameModes[unhoverNum].image.sprite = MenuObjects.NormalImages[unhoverNum];
        MenuObjects.Anims[unhoverNum].SetBool("Hovered", false);
    }

    public void ExitButtonHover()
    {
        ExitButton.image.sprite = ExitSprites[1];
    }

    public void ExitButtonUnhover()
    {
        ExitButton.image.sprite = ExitSprites[0];
    }
    
}
