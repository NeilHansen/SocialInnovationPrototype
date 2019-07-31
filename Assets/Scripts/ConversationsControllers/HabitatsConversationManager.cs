﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HabitatsConversationManager : MonoBehaviour {

    [System.Serializable]
    public struct Conversation{
        [Tooltip("Enter a sting for part of conversation")]
        public string Convo;
        [Tooltip("Enter a bool for person speaking(true for Player, false for NPC)")]
        public bool PlayerSpeaking;

    }

    [System.Serializable]
    public struct FullConvo
    {
        public Conversation[] convoParts;
        public int convoEnd;
    }

    public HabitatTutorialManager tm;
    public FullConvo[] Tutorial;
    //public Conversation[] convoParts;
    private int convoIndex = 0;
   // public int convoEnd = 0;
    public TextMeshProUGUI convoText;
    public GameObject playerImage;
    public GameObject NPCImage;

    public Sprite[] NextStates;
    public Button NextButton;

 //   private JSONPlayerSaver JSONSave;

    private int progress;

    // Use this for initialization
    void Start()
    {
      //  JSONSave = FindObjectOfType<JSONPlayerSaver>();
        // progress = JSONSave.LoadData(JSONSave.dataPath).habitatIntroProgress;
        PlayerPrefs.GetInt("habitatIntroProgress");
        if (progress == 0)
        {
            NextConvoPeice();
        }
        

    
    }
	
	// Update is called once per frame
	void Update () {
    
    }

    public void RestartTutorial()
    {
        convoIndex = 0;
        NextConvoPeice();
    }

    public void NextTutorialPeice()
    {
        convoIndex = 0;
        NextConvoPeice();
    }

    public void NextConvoPeice()
    {
        int ii = PlayerPrefs.GetInt("habitatIntroProgress");
       // int ii = JSONSave.LoadData(JSONSave.dataPath).habitatIntroProgress;
        Debug.Log(ii);
        if(ii > tm.tutorialEnd)
        {
            Debug.Log("finishedTutorial");
        }
        convoIndex++;
        if (convoIndex >= Tutorial[ii].convoEnd)
            {
                this.gameObject.SetActive(false);
                tm.TurnOffControls(false);
                PlayerPrefs.SetInt("habitatIntroProgress", ii+=1);

               // ii += 1;
              //  JSONSave.playerData.habitatIntroProgress = ii;
               // PlayerData data = JSONSave.LoadData(JSONSave.dataPath);
               // data.habitatIntroProgress = ii;
               // JSONSave.SaveData(data, JSONSave.dataPath);
            }
            else
            {
                for (int i = 0; i < convoIndex; i++)
                {
                    convoText.text = Tutorial[ii].convoParts[i].Convo;
                    if (Tutorial[ii].convoParts[i].PlayerSpeaking == true)
                    {
                        playerImage.SetActive(true);
                        NPCImage.SetActive(false);
                        convoText.alignment = TMPro.TextAlignmentOptions.Left;
                    }
                    else
                    {
                        playerImage.SetActive(false);
                        NPCImage.SetActive(true);
                        convoText.alignment = TMPro.TextAlignmentOptions.Right;
                    }
                }

            }
        
        
    }


    public void ChangeNextButton(bool Hover)
    {
        if (Hover)
        {
            NextButton.image.sprite = NextStates[1];
        }

        else
        {
            NextButton.image.sprite = NextStates[0];
        }
    }

}
