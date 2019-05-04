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
    //endScreen
    [SerializeField]
    GameObject EndingScreen;
    [SerializeField]
    ConstructionUI HouseUI;
    [SerializeField]
    Image Fill;

    //HOUSECHANGE
    [SerializeField]
    GameObject[] HouseObject;

    bool GameComplete = false;


    

    public float timerMultiplier = 0.1f;


    // Use this for initialization
    void Start() {
        timeValue = MaxTime;
        HouseUI.AddToHouse(playerScore);
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddScore();
            
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


    public void EndGame()
    {
        GameComplete = true;
        //Ending quiz screen
        EndingScreen.GetComponent<Questionaire>().InitializeQuestionaire();
        //EndingScreen.GetComponent<EndScreenCOnstruction>().EndGame();
        Time.timeScale = 0f;

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
        SwapHouse();

        HouseUI.AddToHouse(HouseAmount);
        timeValue = timeValue + 15;
        if (timeValue > MaxTime)
        {
            timeValue = MaxTime;
        }

        if (HouseAmount == 4)
        {
            EndGame();
        }
        //ScoreAdded.text = "+" + currentFoodValue;
      
        //scoreText.text = "Score: " + (int)playerScore;
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
        switch (HouseAmount-1)
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



