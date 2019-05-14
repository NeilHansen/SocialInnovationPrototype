using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToyDriveGameManager : MonoBehaviour {

    public float timeValue;

    public TextMeshProUGUI TimeRemaining;

    public Slider satisfactionMeter;
    public bool hasTruck = false;
    public float defautlSatisfactionLevel;
    public float defaultFoodValue;

    //public float satisfactionLevel;
    public Text scoreText;
    private float currentFoodValue;
    public float playerScore;
    public ToyTruck Truck;
    public  float PointsPerPackage = 25;
    public int PackageAmount;
    public Sprite StarFill;
    public Image[] Stars;


    //satisfaction visual Mods
    [SerializeField]
    Color[] Colors;
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
    Sprite[] BarColor;
    [SerializeField]
    Image Fill;
    [SerializeField]
    InteractableArea TruckInteractable;
    float TimeTruckStarted;
    bool GameComplete = false;


    //public float timerMultiplier = 0.1f;


    public bool isBonusMultiplierOn = false;
    public TextMeshProUGUI PackageCounter;


    private JSONPlayerSaver JSONSave;

    // Use this for initialization
    void Start()
    {
     

        satisfactionMeter.maxValue = 1;

        scoreText.text = "Score: " + (int)playerScore;


    }



    // Update is called once per frame
    void Update()
    {
        //Game timer
        timeValue -= 1 * Time.deltaTime;
        TimeRemaining.text =""+(int)timeValue ;

        PackageCounter.text = "packages  " + PackageAmount;

     
     
        if (timeValue <= 0&& !GameComplete)
        {
            GameComplete = true;
            Time.timeScale = 0.0f;
            EndingScreen.SetActive(true);
            EndingScreen.GetComponent<Questionaire>().gameScoreText.text = playerScore.ToString();
            EndingScreen.GetComponent<Questionaire>().isPostGameQuestionnaire = true;
            EndingScreen.GetComponent<Questionaire>().InitializeQuestionaire();
            SaveGameScore();
        }

        //TruckWaitMeter();
    }

    void SaveGameScore()
    {
        JSONSave = FindObjectOfType<JSONPlayerSaver>();
        PlayerData data = JSONSave.LoadData(JSONSave.dataPath);
        if (playerScore > data.gameScoreToys)
        {
            data.gameScoreToys = (int)playerScore;
            JSONSave.SaveData(data, JSONSave.dataPath);
        }
    }

    public void StartNewTruck()
    {
        satisfactionMeter.value = 1;
        TimeTruckStarted = Time.time;
        hasTruck = true;
    }

    public void AddScore()
    {
        hasTruck = false;
        Truck.Drive();
        float CurrentScoreValue = PointsPerPackage * Truck.packages;
        PackageAmount++;




        switch (Truck.packages)
        {
             case 0:
                StartCoroutine(FlashScoreAdded(2));
                break;

            case 1:
                StartCoroutine(FlashScoreAdded(2));
                break;

            case 2:
                StartCoroutine(FlashScoreAdded(1));
                break;

            case 3:
                StartCoroutine(FlashScoreAdded(1));
                break;

            case 4:
                StartCoroutine(FlashScoreAdded(0));
                break;


        }
        

        ScoreAdded.text = "+" + CurrentScoreValue;
        playerScore += CurrentScoreValue;
        //Debug.Log("the player score is" + playerScore);

        scoreText.text = "Score: " + (int)playerScore;


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
            Stars[sf].sprite = StarFill;
        }
    }






    //Flashes the amount of points the player received

    public void TruckWaitMeter()
    {
        if (hasTruck)
        {
            satisfactionMeter.value = (Truck.TruckWaitTime - (Time.time - TimeTruckStarted))/Truck.TruckWaitTime;

            


           
            if (satisfactionMeter.value > .70f)
            {
                Fill.sprite = BarColor[0];
                //SatisfactionFace.sprite = Satisfactionemojis[0];
                SatisfactionText.color = Colors[0];

            }

            else if (satisfactionMeter.value > .245f)
            {
                Fill.sprite = BarColor[1];
                // SatisfactionFace.sprite = Satisfactionemojis[1];
                SatisfactionText.color = Colors[1];

            }

            else
            {
                Fill.sprite = BarColor[2];
                //SatisfactionFace.sprite = Satisfactionemojis[2];
                SatisfactionText.color = Colors[2];

            }

            if(satisfactionMeter.value <= 0&& TruckInteractable.isInteracting==false)
            {
                AddScore();
            }

            

        }
    }


    IEnumerator FlashScoreAdded(int index)
    {
        
        ScoreAdded.enabled = true;
        ScoreAdded.color = Colors[index];
        yield return new WaitForSeconds(1);
        ScoreAdded.enabled = false;
        StopCoroutine("FlashScoreAdded");
    }





}
