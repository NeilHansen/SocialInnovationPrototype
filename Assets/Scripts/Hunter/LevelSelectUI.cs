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

        if (PlayerPrefs.GetInt("totalQuizScoreCooks") >= quizStarsRequires && PlayerPrefs.GetInt("gameScoreCooks") >= gameScoreRequired)
        {
            //DisableButton
            HabitatsButton.interactable = true;
            LockImage[1].SetActive(false);
            // SetHabitatsScore();
        }

        //Debug.Log()
        if (PlayerPrefs.GetInt("totalQuizScoreHabitats") >= quizStarsRequires && PlayerPrefs.GetInt("gameScoreHabitats") >= gameScoreRequired)
        {
//            Debug.Log("UngateQuiz");
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
        int GameCook = PlayerPrefs.GetInt("gameScoreCooks");
        CooksScore.ScoreText.text = "" + GameCook;
        

        //ScoreStars
        int CookStarAmount = 0;

        if (GameCook >= 299)
        {
            
            CookStarAmount = 3;
        }

        else if (GameCook >= 200)
        {
            CookStarAmount = 2;
        }

        else if(GameCook > 0)
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
        int QuizCook = PlayerPrefs.GetInt("totalQuizScoreCooks");
        CooksScore.QuizScore.text = "" + QuizCook;

        //ScoreStars
        int CookquizStarAmount = 0;

        if (QuizCook >= 400)
        {
            CookquizStarAmount = 3;
        }

        else if (QuizCook >= 200)
        {
            CookquizStarAmount = 2;
        }

        else if (QuizCook > 0)
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
        float CooksTotal= GameCook + QuizCook;
        CooksTOTALSCORE.text = "" + CooksTotal;




    }



    public void SetHabitatsScore()
    {
        int GameHabitat = PlayerPrefs.GetInt("gameScoreHabitats");
        //GameScore
       habitatScore.ScoreText.text = "" + GameHabitat;

        //ScoreStars
        int habitatStarAmount = 0;

        if (GameHabitat >= 299)
        {
           
            habitatStarAmount = 3;
        }

        else if (GameHabitat >= 200)
        {
            habitatStarAmount = 2;
        }

        else if (GameHabitat> 0)
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
        int QuizHabitat = PlayerPrefs.GetInt("totalQuizScoreHabitats");
        habitatScore.QuizScore.text = "" + QuizHabitat;

        //ScoreStars
        int habitatquizStarAmount = 0;

        if (QuizHabitat >= 400)
        {
            habitatquizStarAmount = 3;
        }

        else if (QuizHabitat>= 200)
        {
            habitatquizStarAmount = 2;
        }

        else if (QuizHabitat > 0)
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
        float TotalHome = QuizHabitat+ GameHabitat;
        HabitatsTOTALSCORE.text = "" +TotalHome;





    }

    public void SetToysScore()
    {
        //GameScore

        int GameToys= PlayerPrefs.GetInt("gameScoreToys");
        toysScore.ScoreText.text = "" + GameToys;

        //ScoreStars
        int toysStarAmount = 0;

        if (GameToys >= 299)
        {
            
            toysStarAmount = 3;
        }

        else if (GameToys >= 200)
        {
            toysStarAmount = 2;
        }

        else if (GameToys > 0)
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
        int QuizToys = PlayerPrefs.GetInt("totalQuizScoreToys");
       toysScore.QuizScore.text = "" + QuizToys;
        

        //ScoreStars
        int toysquizStarAmount = 0;

        if (QuizToys >= 400)
        {
           
            toysquizStarAmount = 3;
        }

        else if (QuizToys >= 200)
        {
            toysquizStarAmount = 2;
        }

        else if (QuizToys> 0)
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


        float TotalToys= GameToys+ QuizToys;
        ToysTOTALSCORE.text = "" +TotalToys ;


    }


    public void clickSound()
    {
        FindObjectOfType<AudioManager>().PlaySound("click");
    }
    
}
