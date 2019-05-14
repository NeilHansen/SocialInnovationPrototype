using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CompleteGameCooks : MonoBehaviour {



    [SerializeField]
    GameManager GM;

    [SerializeField]
    Image[] Stars;

    [SerializeField]
    TextMeshProUGUI ScoreText;

    public Sprite FilledStar;

    public GameObject convoCanvas;
    public GameObject scoreScreen;


    private JSONPlayerSaver JSONSave;

    private int progress;

    // Use this for initialization
    void Start()
    {
        JSONSave = FindObjectOfType<JSONPlayerSaver>();
        progress = JSONSave.LoadData(JSONSave.dataPath).cooksIntroProgress;
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void RoundComplete()
    {
        int StarAmount = 3;

        //Debug.Log("EndGameScreen" + GM.playerScore);
        if (GM.playerScore >= 299)
        {
            StarAmount = 3;
        }

        else if (GM.playerScore >= 200)
        {
            StarAmount = 2;
        }

        else if (GM.playerScore > 0)
        {
            StarAmount = 1;
        }

        else
        {
            StarAmount = 0;
        }

        for (int i = 0; i < StarAmount; i++)
        {
            Stars[i].sprite = FilledStar;
        }
        ScoreText.text = "Score:" + GM.playerScore;




    }
    //YUK
    public void GoToStory()
    {
        if (progress < Camera.main.GetComponent<CooksTutorialManager>().tutorialEnd)
        {
            convoCanvas.SetActive(true);
            Camera.main.GetComponent<CooksTutorialManager>().NextTutorialPeice();
            scoreScreen.SetActive(false);
        }
        else
        {
            scoreScreen.SetActive(false);
            GM.GoToQuiz();
        }
    }




}
