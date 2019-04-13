using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConversationManager : MonoBehaviour {

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

    public TutorialManager tm;
    public FullConvo[] Tutorial;
    //public Conversation[] convoParts;
    private int convoIndex = 0;
   // public int convoEnd = 0;
    public TextMeshProUGUI convoText;
    public GameObject playerImage;
    public GameObject NPCImage;
    // Use this for initialization
    void Start () {
        NextConvoPeice();

        
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
        int ii = PlayerPrefs.GetInt("tutorialProgress");
        if(ii > tm.tutorialEnd)
        {
            Debug.Log("finishedTutorial");
        }
        convoIndex++;
        if (convoIndex == Tutorial[ii].convoEnd)
        {
            this.gameObject.SetActive(false);
            tm.TurnOffControls(false);
            PlayerPrefs.SetInt("tutorialProgress", ii+=1);
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
}
