using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerConstruction : MonoBehaviour {


    public Text timerText;
    public Text timerTextBG;
    public float timeValue;
    public float MaxTime;

    public Slider satisfactionMeter;
    public bool hasCustomer = false;
    public float defautlSatisfactionLevel;
    public float defaultFoodValue;

    //public float satisfactionLevel;
    public Text scoreText;
    private float currentFoodValue;
    public int playerScore;

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
        PreformanceMeter();
        
       
        
        //timerText.text = "" + (int)timeValue;
        //timerTextBG.text = "" + (int)timeValue;



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
        playerScore++;

        HouseUI.AddToHouse(playerScore);
        timeValue = timeValue + 15;
        if (timeValue > MaxTime)
        {
            timeValue = MaxTime;
        }

        if (playerScore == 4)
        {
            EndingScreen.SetActive(true);
            EndingScreen.GetComponent<EndScreenCOnstruction>().EndGame();
            Time.timeScale = 0f;
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





}



