using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManagerConstruction : MonoBehaviour {


   
    public float timeValue;
    public float MaxTime;
    public TextMeshProUGUI TimeLeft;

    public Slider satisfactionMeter;
    public bool hasCustomer = false;
    public float defautlSatisfactionLevel;
    public float defaultFoodValue;

    //public float satisfactionLevel;
    public Text scoreText;
    private float currentFoodValue;
    public int playerScore;
    public int HouseAmount;

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
    public Sprite StarFill;
    public Image[] Star;

    //endScreen
    [SerializeField]
    GameObject EndingScreen;
    [SerializeField]
    GameObject QuizWidget;
    [SerializeField]
    ConstructionUI HouseUI;
    [SerializeField]
    Image Fill;

    //HOUSECHANGE
    [SerializeField]
    GameObject[] HouseObject;

    bool GameComplete = false;


    

    public float timerMultiplier = 0.1f;

    private JSONPlayerSaver JSONSave;

    // Use this for initialization
    void Start() {
        timeValue = MaxTime;
        //HouseUI.AddToHouse(playerScore);
        SwapHouse();

        scoreText.text = "Score:" + playerScore;
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.Space))
        {
           // AddScore();
            
        }


        timeValue -= 1 * Time.deltaTime;
        TimeLeft.text = "" + (int)timeValue;
        PreformanceMeter();

        if(timeValue<=0 && !GameComplete)
        {
            
            EndGame();
        }
        

       
        
        //timerText.text = "" + (int)timeValue;
        //timerTextBG.text = "" + (int)timeValue;



    }

    void SaveGameScore()
    {
        
        JSONSave = FindObjectOfType<JSONPlayerSaver>();
        PlayerData data = JSONSave.LoadData(JSONSave.dataPath);
        if (playerScore > data.gameScoreHabitats)
        {
            data.gameScoreHabitats = (int)playerScore;
            JSONSave.SaveData(data, JSONSave.dataPath);
        }
    }


    public void EndGame()
    {
        GameComplete = true;
        //Ending quiz screen
        EndingScreen.SetActive(true);
        EndingScreen.GetComponent<CompleteScreenHabitats>().RoundComplete();
        Time.timeScale = 0f;
        SaveGameScore();
    }

    public void Test()
    {
        Debug.Log("test");
    }

    public void StartNewCustomer()
    {
       // satisfactionMeter.value = defautlSatisfactionLevel;
        //hasCustomer = true;
    }

    public void AddScore()
    {
        HouseAmount++;
        playerScore += 75;
        scoreText.text = "Score:"+playerScore;
        //SwapHouse();

        //HouseUI.AddToHouse(HouseAmount);
        timeValue = timeValue + 15;
        if (timeValue > MaxTime)
        {
            timeValue = MaxTime;
        }

        //if (HouseAmount == 3)
        //{
        //    EndGame();
        //}
        //ScoreAdded.text = "+" + currentFoodValue;

        //scoreText.text = "Score: " + (int)playerScore;


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


    public void PreformanceMeter()
    {
        satisfactionMeter.value = timeValue / MaxTime;
        if (satisfactionMeter.value > .66f)
        {
            Fill.sprite= BarColors[0];
            SatisfactionFace.sprite = Satisfactionemojis[0];
            SatisfactionText.color = Colors[0];

        }

        else if (satisfactionMeter.value > .327f)
        {
            Fill.sprite = BarColors[1];
            SatisfactionFace.sprite = Satisfactionemojis[1];
            SatisfactionText.color = Colors[1];

        }

        else
        {
            Fill.sprite = BarColors[2];
            SatisfactionFace.sprite = Satisfactionemojis[2];
            SatisfactionText.color = Colors[2];

        }
    }

    public void QuizScreen()
    {
        QuizWidget.SetActive(true);
        FindObjectOfType<Questionaire>().gameScoreText.text = playerScore.ToString();
        FindObjectOfType<Questionaire>().isPostGameQuestionnaire = true;
        FindObjectOfType<Questionaire>().InitializeQuestionaire();
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



    public void SwapHouse()
    {
        switch (HouseAmount)
        {
            case 0:
                
                HouseObject[0].SetActive(true);
                break;

            case 1:
                HouseObject[0].SetActive(false);
                HouseObject[1].SetActive(true);
                break;

            case 2:
                HouseObject[1].SetActive(false);
                HouseObject[2].SetActive(true);
                break;

            case 3:
                HouseObject[2].SetActive(false);
                HouseObject[3].SetActive(true);
                break;
        }
    }

}



