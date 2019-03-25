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

    public Conversation[] convoParts;
    private int convoIndex = 0;
    public int convoEnd = 0;
    public TextMeshProUGUI convoText;
    public GameObject playerImage;
    public GameObject NPCImage;
    // Use this for initialization
    void Start () {
        NextConvoPeice();
	}
	
	// Update is called once per frame
	void Update () {
        //for (int i = 0; i > convoIndex; i++)
        //{
        //    convoText.text = convoParts[i].Convo;
        //    if (convoParts[i].PlayerSpeaking == true)
        //    {
        //        playerImage.SetActive(true);
        //        NPCImage.SetActive(false);
        //    }
        //    else
        //    {
        //        playerImage.SetActive(false);
        //        NPCImage.SetActive(true);
        //    }
        //}
    }

    public void NextConvoPeice()
    {
        convoIndex++;
        if (convoIndex == convoEnd)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            for (int i = 0; i < convoIndex; i++)
            {
                convoText.text = convoParts[i].Convo;
                if (convoParts[i].PlayerSpeaking == true)
                {
                    playerImage.SetActive(true);
                    NPCImage.SetActive(false);
                }
                else
                {
                    playerImage.SetActive(false);
                    NPCImage.SetActive(true);
                }
            }

        }       
    }
}
