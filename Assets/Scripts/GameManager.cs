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
    bool EndGameInitiated=false;

    [SerializeField]
    Image Fill;

    public float timerMultiplier = 0.1f;

    public GameObject specialCustomerBonusText;
    public float specialCustomerBonusMultiplier = 1.0f;
    public bool isBonusMultiplierOn = false;


    private JSONPlayerSaver JSONSave;

    // Use this for initialization
    void Start() {
       // timerText.text = "" + (int)timeValue;
       // timerTextBG.text = "" + (int)timeValue;
        TimeText.text =""+(int)timeValue;
        
        satisfactionMeter.maxValue = defautlSatisfactionLevel;
        //StartNewCustomer();
        specialCustomerBonusText.SetActive(false);
    }



    // Update is called once per frame
    void Update() {
        //Game timer
        timeValue -= 1 * Time.deltaTime;
        //timerText.text = "" + (int)timeValue;
        //timerTextBG.text = "" + (int)timeValue;
        TimeText.text = "" + (int)timeValue;
        specialCustomerBonusText.GetComponent<TextMeshProUGUI>().text = "x" + specialCustomerBonusMultiplier;
        if (timeValue <= 0&&!EndGameInitiated)
        {
            EndGameInitiated = true;
            Time.timeScale = 0.0f;
            //EndingScreen.SetActive(true);
            EndingScreen.GetComponent<Questionaire>().gameScoreText.text = playerScore.ToString();
            EndingScreen.GetComponent<Questionaire>().isPostGameQuestionnaire = true;
            EndingScreen.GetComponent<Questionaire>().InitializeQuestionaire();
            SaveGameScore();
        }

        if (hasCustomer)
        {
            satisfactionMeter.value -= Time.deltaTime * timerMultiplier;
            if (satisfactionMeter.value > 0.991f)
            {
                Fill.sprite = BarColors[0];
                SatisfactionFace.sprite = Satisfactionemojis[0];
                //SatisfactionText.color = Colors[0];

            }

            else if (satisfactionMeter.value > .515f)
            {
                Fill.sprite = BarColors[1];
                SatisfactionFace.sprite = Satisfactionemojis[1];
                //SatisfactionText.color = Colors[1];

            }

            else
            {
                Fill.sprite = BarColors[2];
                SatisfactionFace.sprite = Satisfactionemojis[2];
                //SatisfactionText.color = Colors[2];

            }

        }

        if (isBonusMultiplierOn)
        {
            specialCustomerBonusText.SetActive(true);
            //specialCustomerBonusMultiplier = 2.0f;
        }
        else
        {
            specialCustomerBonusText.SetActive(false);
            specialCustomerBonusMultiplier = 1.0f;
        }


    }

    void SaveGameScore()
    {
        JSONSave = FindObjectOfType<JSONPlayerSaver>();
        PlayerData data = JSONSave.LoadData(JSONSave.dataPath);
        if (playerScore > data.gameScoreCooks)
        {
            data.gameScoreCooks = (int)playerScore;
            JSONSave.SaveData(data, JSONSave.dataPath);
           
        }
    }

    public void Test()
    {
        Debug.Log("test");
    }

    public void StartNewCustomer()
    {
        satisfactionMeter.value = defautlSatisfactionLevel;
        hasCustomer = true;
    }

    public void AddScore()
    {
        //currentFoodValue = defaultFoodValue * satisfactionMeter.value;

        if (satisfactionMeter.value > 1.04f)
        {
            currentFoodValue = 100 * specialCustomerBonusMultiplier;
            StartCoroutine(FlashScoreAdded(0));
        }

        else if (satisfactionMeter.value > .38f)
        {
            currentFoodValue = 50 * specialCustomerBonusMultiplier;
            StartCoroutine(FlashScoreAdded(1));
        }
        else
        {
            currentFoodValue = 25 * specialCustomerBonusMultiplier;
            StartCoroutine(FlashScoreAdded(2));
        }

        isBonusMultiplierOn = false;

        ScoreAdded.text = "+" + currentFoodValue;
        playerScore += currentFoodValue;
        scoreText.text = "Score: " + (int)playerScore;
    }

    public void SpecialCustomerScore()
    {
        StartCoroutine(FlashScoreAdded(0));
        ScoreAdded.text = "+" + 75;
        playerScore += 75;
        scoreText.text = "Score: " + (int)playerScore;
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





}



