using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScreenCOnstruction : MonoBehaviour {
    //Regular Score Stuff
    [SerializeField]
    Image[] Stars;
    [SerializeField]
    TextMeshProUGUI ScoreText;
    [SerializeField]
    GameManagerConstruction GM;
    [SerializeField]
    Button TryAgain;
    [SerializeField]
    Button ReturnToDorm;
    [SerializeField]
    Sprite[] TryAgainSprite;
    [SerializeField]
    Sprite[] RTDSprite;

    //For quiz score stuff
    [SerializeField]
    Image Medal;
    [SerializeField]
    Sprite[] MedalType;
    [SerializeField]
    TextMeshProUGUI QuizScoreText;
    public Questionaire QuizData;



    // Use this for initialization
    void Start () {
        EndGame();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EndGame()
    {
        //Handles Game Score
        int StarAmount = 3;
        Debug.Log("The time value is" + GM.timeValue / GM.MaxTime);

        if (GM.timeValue/GM.MaxTime>=.70f )
        {
            StarAmount = 3;
        }

        else if (GM.timeValue / GM.MaxTime >= .245f)
        {
            StarAmount = 2;
        }

        else
        {
            StarAmount = 1;
        }

        for (int i = 0; i < StarAmount; i++)
        {
            Stars[i].enabled = true;
        }

        //Set Score Text If Any Here
        ScoreText.text = "Your Score Is: " + (GM.timeValue / GM.MaxTime) * 100;

        //Quiz Score Stuff
        if (QuizData != null)
        {
            int QS = QuizData.score;

            if (QS >= 8)
            {
                //Gold
                Medal.sprite = MedalType[0];
            }

            else if (QS >= 6)
            {
                //Silver
                Medal.sprite = MedalType[1];
            }

            else
            {
                //Bronze
                Medal.sprite = MedalType[2];
            }

            QuizScoreText.text = "Your Quiz Score Is: " + QS;
        }



    }








    public void RestartGame(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void LoadDorm()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }






    //Hover Methods
    public void ChangeRestartButton(bool Hover)
    {
        if (Hover)
        {
            TryAgain.GetComponent<Image>().sprite = TryAgainSprite[1];
        }
        else
        {
            TryAgain.GetComponent<Image>().sprite = TryAgainSprite[0];
        }
    }

    public void changeReturnToDormButton(bool H)
    {
        if (H)
        {
            ReturnToDorm.GetComponent<Image>().sprite = RTDSprite[1];
        }
        else
        {
            ReturnToDorm.GetComponent<Image>().sprite = RTDSprite[0];
        }
    }

}
