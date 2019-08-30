using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class ToysEndGame : MonoBehaviour {
    public ToyDriveGameManager GM;
    [SerializeField]
    Image[] Stars;
    [SerializeField]
    Image[] StarsQuiz;
    [SerializeField]
    TextMeshProUGUI ScoreText;
  
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
    public Sprite FilledStar;
    public TextMeshProUGUI TotalScore;

    //BadgeScore Stuff
    int CumulativeQuizScore;
    int BadgeRequiredAmount = 1000;
    public Slider BadgeBar;
    public TextMeshProUGUI BadgeText;



    // Use this for initialization
    void Start()
    {
        EndGame();
    }

    // Update is called once per frame
    void Update()
    {

    }



    public void EndGame()
    {
        CumulativeQuizScore = QuizData.GetOverallQuizScore();
        //Fill out Badge Progress info
        BadgeBar.value = (float)CumulativeQuizScore / (float)BadgeRequiredAmount;
        BadgeText.text = "" + CumulativeQuizScore;

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
        ScoreText.text = "" + GM.playerScore;

        //Quiz Score Stuff
        if (QuizData != null)
        {
            int QS = QuizData.score;
            int QuizStarAmount = 0;

            if (QS >= 400)
            {
                //Gold
                QuizStarAmount = 3;
            }

            else if (QS >= 200)
            {
                //Silver
                QuizStarAmount = 2;
            }

            else if (QS > 0)
            {
                //Bronze
                QuizStarAmount = 1;
            }

            else
            {
                QuizStarAmount = 0;
            }


            for (int q = 0; q < QuizStarAmount; q++)
            {
                StarsQuiz[q].sprite = FilledStar;
            }

            QuizScoreText.text = "" + QS;

            float TS = QS + GM.playerScore;
            TotalScore.text = "" + Mathf.RoundToInt(TS);
        }

    }













    public void RestartGame()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoaddDorm()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }



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


