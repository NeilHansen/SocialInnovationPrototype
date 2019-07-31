﻿using System.Collections;
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

    [System.Serializable]
    struct GameModeTopScore
    {
       public Image[] StarsScore;
       public TextMeshProUGUI ScoreText;
       public Image[] StarsQuiz;
       public TextMeshProUGUI QuizScore;
    }

    public GameObject[] LevelButtons;



    [SerializeField]
    Sprite FilledInStar;

    [SerializeField]
    GameModeTopScore CooksScore;

    [SerializeField]
    GameModeTopScore habitatScore;

    [SerializeField]
    GameModeTopScore toysScore;

    public TextMeshProUGUI CooksTOTALSCORE;
    public TextMeshProUGUI HabitatsTOTALSCORE;
    public TextMeshProUGUI ToysTOTALSCORE;



    [SerializeField]
    Button ExitButton;
   
    [SerializeField]
    Sprite[] ExitSprites;

   [SerializeField]
    GameModeButtons MenuObjects;

   // JSONPlayerSaver JSON;

    PlayerData data;

    //Buttons
    public Button CooksButton;
    public Button HabitatsButton;
    public Button ToysButton;

    public GameObject[] LockImage;

    public int gameScoreRequired = 0;
    public int quizStarsRequires = 0;


    public GameObject[] LearnMoreButtons;





    // Use this for initialization
    void Start () {
        //Load Player Data
     //   JSON= FindObjectOfType<JSONPlayerSaver>();

      //  data = JSON.LoadData(JSON.dataPath);

        

        SetCooksScore();
        SetHabitatsScore();
        SetToysScore();

        if (data.totalQuizScoreCooks >= quizStarsRequires && data.gameScoreCooks >= gameScoreRequired)
        {
            //DisableButton
            HabitatsButton.interactable = true;
            LockImage[1].SetActive(false);
            // SetHabitatsScore();
        }

        //Debug.Log()
        if (data.totalQuizScoreHabitats >= quizStarsRequires && data.gameScoreHabitats >= gameScoreRequired)
        {
            Debug.Log("UngateQuiz");
            //UnlockToys
            ToysButton.interactable = true;

            LockImage[2].SetActive(false);
            
        }

        

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void HoverButton(int bn)
    {
        if (LevelButtons[bn].GetComponent<Button>().interactable == true)
        {
            LevelButtons[bn].GetComponent<Animator>().SetBool("Hover", true);
        }
        
        
    
       // GameModes[Gamemode].image.sprite = HoverImages[Gamemode];


    }

    public void UnhoverButtons(int unhoverNum)
    {
        LevelButtons[unhoverNum].GetComponent<Animator>().SetBool("Hover", false);
    }

    public void ExitButtonHover()
    {
       
        ExitButton.image.sprite = ExitSprites[1];
    }

    public void ExitButtonUnhover()
    {
        ExitButton.image.sprite = ExitSprites[0];
    }


    //Learn More Buttons

    public void LearnMoreHover(int LH)
    {
        LearnMoreButtons[LH].GetComponent<Animator>().SetBool("Hover", true);
    }

    public void LearnMoreUnhover(int UH)
    {
        LearnMoreButtons[UH].GetComponent<Animator>().SetBool("Hover", false);
    }




    public void SetCooksScore()
    {
        //GameScore
        CooksScore.ScoreText.text = "" + data.gameScoreCooks;

        //ScoreStars
        int CookStarAmount = 0;

        if (data.gameScoreCooks >= 299)
        {
            
            CookStarAmount = 3;
        }

        else if (data.gameScoreCooks >= 200)
        {
            CookStarAmount = 2;
        }

        else if(data.gameScoreCooks > 0)
        {
            CookStarAmount = 1;
        }

        else
        {
            CookStarAmount = 0;
        }

        for(int C = 0; C <CookStarAmount; C++)
        {
            CooksScore.StarsScore[C].sprite = FilledInStar;
        }


        //QuizScore

        CooksScore.QuizScore.text = "" + data.totalQuizScoreCooks;

        //ScoreStars
        int CookquizStarAmount = 0;

        if (data.totalQuizScoreCooks>=400)
        {
            CookquizStarAmount = 3;
        }

        else if (data.totalQuizScoreCooks >= 200)
        {
            CookquizStarAmount = 2;
        }

        else if (data.totalQuizScoreCooks> 0)
        {
            CookquizStarAmount = 1;
        }

        else
        {
            CookquizStarAmount = 0;
        }

        for (int CQ = 0; CQ < CookquizStarAmount; CQ++)
        {
            CooksScore.StarsQuiz[CQ].sprite = FilledInStar;
        }
        float CooksTotal= data.gameScoreCooks + data.totalQuizScoreCooks;
        CooksTOTALSCORE.text = "" + CooksTotal;




    }



    public void SetHabitatsScore()
    {
        //GameScore
       habitatScore.ScoreText.text = "" + data.gameScoreHabitats;

        //ScoreStars
        int habitatStarAmount = 0;

        if (data.gameScoreHabitats >= 299)
        {
           
            habitatStarAmount = 3;
        }

        else if (data.gameScoreHabitats >= 200)
        {
            habitatStarAmount = 2;
        }

        else if (data.gameScoreHabitats > 0)
        {
            habitatStarAmount = 1;
        }

        else
        {
            habitatStarAmount = 0;
        }

        for (int C = 0; C < habitatStarAmount; C++)
        {
            habitatScore.StarsScore[C].sprite = FilledInStar;
        }


        //QuizScore

        habitatScore.QuizScore.text = "" + data.totalQuizScoreHabitats;

        //ScoreStars
        int habitatquizStarAmount = 0;

        if (data.totalQuizScoreHabitats >= 400)
        {
            habitatquizStarAmount = 3;
        }

        else if (data.totalQuizScoreHabitats >= 200)
        {
            habitatquizStarAmount = 2;
        }

        else if (data.totalQuizScoreHabitats > 0)
        {
            habitatquizStarAmount = 1;
        }

        else
        {
            habitatquizStarAmount = 0;
        }

        for (int CQ = 0; CQ < habitatquizStarAmount; CQ++)
        {
            habitatScore.StarsQuiz[CQ].sprite = FilledInStar;
        }
        float TotalHome = data.totalQuizScoreHabitats + data.gameScoreHabitats;
        HabitatsTOTALSCORE.text = "" +TotalHome;





    }

    public void SetToysScore()
    {
        //GameScore
        toysScore.ScoreText.text = "" + data.gameScoreToys;

        //ScoreStars
        int toysStarAmount = 0;

        if (data.gameScoreToys >= 299)
        {
            
            toysStarAmount = 3;
        }

        else if (data.gameScoreToys >= 200)
        {
            toysStarAmount = 2;
        }

        else if (data.gameScoreToys > 0)
        {
            toysStarAmount = 1;
        }

        else
        {
            toysStarAmount = 0;
        }

        for (int C = 0; C < toysStarAmount; C++)
        {
           toysScore.StarsScore[C].sprite = FilledInStar;
        }


        //QuizScore

       toysScore.QuizScore.text = "" + data.totalQuizScoreToys;

        //ScoreStars
        int toysquizStarAmount = 0;

        if (data.totalQuizScoreToys >= 400)
        {
           
            toysquizStarAmount = 3;
        }

        else if (data.totalQuizScoreToys >= 200)
        {
            toysquizStarAmount = 2;
        }

        else if (data.totalQuizScoreToys > 0)
        {
            toysquizStarAmount = 1;
        }

        else
        {
            toysquizStarAmount = 0;
        }

        for (int TQ = 0; TQ < toysquizStarAmount; TQ++)
        {
            toysScore.StarsQuiz[TQ].sprite = FilledInStar;
        }


        float TotalToys= data.gameScoreToys + data.totalQuizScoreToys;
        ToysTOTALSCORE.text = "" +TotalToys ;


    }



    
}
