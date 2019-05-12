using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField]
    Image[] Stars;
    [SerializeField]
    TextMeshProUGUI ScoreText;
    [SerializeField]
    GameManager GM;
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

        else
        {
            StarAmount = 1;
        }

        for (int i = 0; i < StarAmount; i++)
        {
            Stars[i].enabled = true;
        }
        ScoreText.text = "Your Score Is: " + GM.playerScore;

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
