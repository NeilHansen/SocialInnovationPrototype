using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour {


    public Text timerText;
    public Text timerTextBG;
    public float timeValue;
    public TextMeshProUGUI TimeText;

    public Slider satisfactionMeter;
    public bool hasCustomer = false;
    public float defautlSatisfactionLevel;
    public float defaultFoodValue;

    //public float satisfactionLevel;
    public Text scoreText;
    private float currentFoodValue;
    public float playerScore;
    public Sprite StarFill;
    public Image[] Star;

    //satisfaction visual Mods
    [SerializeField]
    Color[] Colors;
    [SerializeField]
    Sprite[] BarColors;
    [SerializeField]
    Sprite[] Satisfactionemojis;
    [SerializeField]
    Image SatisfactionFace;
    [SerializeField]
    Text SatisfactionText;
    [SerializeField]
    Text ScoreAdded;
    //endScreen
    [SerializeField]
    GameObject EndingScreen;
    [SerializeField]
    GameObject COMPLETESCREEN;
    [SerializeField]
    bool EndGameInitiated=false;

    [SerializeField]
    Image Fill;

    public float timerMultiplier = 0.1f;

    public GameObject specialCustomerBonusText;
    public float specialCustomerBonusMultiplier = 1.0f;
    public bool isBonusMultiplierOn = false;

    public GameObject convoCanvas;


    //  private JSONPlayerSaver JSONSave;

    // Use this for initialization
    void Start() {
        // timerText.text = "" + (int)timeValue;
        Time.timeScale = 0;
       // timerTextBG.text = "" + (int)timeValue;
        TimeText.text =""+(int)timeValue;
        
        //satisfactionMeter.maxValue = defautlSatisfactionLevel;
        //StartNewCustomer();
        //specialCustomerBonusText.SetActive(false);
        scoreText.text = "SCORE: " + (int)playerScore;
    }



    // Update is called once per frame
    void Update() {
        //Game timer
        if (timeValue > 0)
        {
            timeValue -= 1 * Time.deltaTime;
        }
        //timerText.text = "" + (int)timeValue;
        //timerTextBG.text = "" + (int)timeValue;
        TimeText.text = "" + (int)timeValue;
        //specialCustomerBonusText.GetComponent<TextMeshProUGUI>().text = "x" + specialCustomerBonusMultiplier;
        if (timeValue <= 0&&!EndGameInitiated)
        {
            EndGameInitiated = true;
            Time.timeScale = 0.0f;
            COMPLETESCREEN.SetActive(true);
            COMPLETESCREEN.GetComponent<CompleteGameCooks>().RoundComplete();
            //EndingScreen.SetActive(true);
            //EndingScreen.GetComponent<Questionaire>().gameScoreText.text = playerScore.ToString();
            //EndingScreen.GetComponent<Questionaire>().isPostGameQuestionnaire = true;
            //EndingScreen.GetComponent<Questionaire>().InitializeQuestionaire();
            

            SaveGameScore();
        }

        //if (hasCustomer)
        //{
        //    satisfactionMeter.value -= Time.deltaTime * timerMultiplier;
        //    if (satisfactionMeter.value > 0.991f)
        //    {
        //        Fill.color = Colors[0];
        //        SatisfactionFace.sprite = Satisfactionemojis[0];
        //        //SatisfactionText.color = Colors[0];

        //    }

        //    else if (satisfactionMeter.value > .515f)
        //    {
        //        Fill.color = Colors[1];
        //        SatisfactionFace.sprite = Satisfactionemojis[1];
        //        //SatisfactionText.color = Colors[1];

        //    }

        //    else
        //    {
        //        Fill.color = Colors[2];
        //        SatisfactionFace.sprite = Satisfactionemojis[2];
        //        //SatisfactionText.color = Colors[2];

        //    }

        //}

        //if (isBonusMultiplierOn)
        //{
        //    //specialCustomerBonusText.SetActive(true);
        //    //specialCustomerBonusMultiplier = 2.0f;
        //}
        //else
        //{
        //    //specialCustomerBonusText.SetActive(false);
        //    specialCustomerBonusMultiplier = 1.0f;
        //}


    }

    void SaveGameScore()
    {
        //  JSONSave = FindObjectOfType<JSONPlayerSaver>();
        //PlayerData data = JSONSave.LoadData(JSONSave.dataPath);
         int gameScoreCooks =  PlayerPrefs.GetInt("gameScoreCooks");
        if (playerScore > gameScoreCooks)
        {
            gameScoreCooks = (int)playerScore;
            PlayerPrefs.SetInt("gameScoreCooks", gameScoreCooks) ;
            //JSONSave.SaveData(data, JSONSave.dataPath);
           
        }
    }

    public void Test()
    {
        Debug.Log("test");
    }

    public void StartNewCustomer()
    {
       // satisfactionMeter.value = defautlSatisfactionLevel;
        hasCustomer = true;
    }

    public void AddScore()
    {
        //currentFoodValue = defaultFoodValue * satisfactionMeter.value;

        currentFoodValue = 50;

        //ScoreAdded.text = "+" + currentFoodValue;
        playerScore += currentFoodValue;
        scoreText.text = "SCORE: " + (int)playerScore;


        int StarAmount = 3;

        //Debug.Log("EndGameScreen" + GM.playerScore);
        if (playerScore >= 299)
        {
            StarAmount = 3;
        }

        else if (playerScore >= 200)
        {
            StarAmount = 2;
        }

        else if (playerScore > 0)
        {
            StarAmount = 1;
        }

        else
        {
            StarAmount = 0;
        }

        for (int sf = 0; sf < StarAmount; sf++)
        {
            Star[sf].sprite = StarFill;
        }


    }

    public void SpecialCustomerScore()
    {
        //StartCoroutine(FlashScoreAdded(0));
        //ScoreAdded.text = "+" + 75;
        playerScore += 75;
        scoreText.text = "SCORE: " + (int)playerScore;
        int StarAmount = 3;

        //Debug.Log("EndGameScreen" + GM.playerScore);
        if (playerScore >= 299)
        {
            StarAmount = 3;
        }

        else if (playerScore >= 200)
        {
            StarAmount = 2;
        }

        else if (playerScore > 0)
        {
            StarAmount = 1;
        }

        else
        {
            StarAmount = 0;
        }

        for (int sf = 0; sf < StarAmount; sf++)
        {
            Star[sf].sprite = StarFill;
        }

    }


    public void ClickingSound()
    {
        FindObjectOfType<AudioManager>().PlaySound("click");
    }



    //Flashes the amount of points the player received



    IEnumerator FlashScoreAdded(int index)
    {
        Debug.Log("COROUTINE");
        ScoreAdded.enabled = true;
        ScoreAdded.color = Colors[index];
        yield return new WaitForSeconds(1);
        ScoreAdded.enabled = false;
        StopCoroutine("FlashScoreAdded");
    }


    public void GoToQuiz()
    {
        //int progress = JSONSave.LoadData(JSONSave.dataPath).cooksIntroProgress;
        int progress = convoCanvas.GetComponent<CooksConversationManager>().Progress;
        if (progress != 0)
        {
            EndingScreen.GetComponent<Questionaire>().gameScoreText.text = playerScore.ToString();
            EndingScreen.GetComponent<Questionaire>().isPostGameQuestionnaire = true;
            EndingScreen.GetComponent<Questionaire>().InitializeQuestionaire();
        }
        else
        {
            EndingScreen.GetComponent<Questionaire>().gameScoreText.text = playerScore.ToString();
            EndingScreen.GetComponent<Questionaire>().isPostGameQuestionnaire = false;
            EndingScreen.GetComponent<Questionaire>().InitializeQuestionaire();
        }
    }


}



