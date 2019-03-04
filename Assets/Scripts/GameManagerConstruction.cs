using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerConstruction : MonoBehaviour {


    public Text timerText;
    public Text timerTextBG;
    public float timeValue;

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

    public float timerMultiplier = 0.1f;


    // Use this for initialization
    void Start() {
        timerText.text = "" + (int)timeValue;
        timerTextBG.text = "" + (int)timeValue;
        satisfactionMeter.maxValue = defautlSatisfactionLevel;
        //StartNewCustomer();
       
    }

    // Update is called once per frame
    void Update() {
        //Game timer
        timeValue -= 1 * Time.deltaTime;
        timerText.text = "" + (int)timeValue;
        timerTextBG.text = "" + (int)timeValue;

        if (timeValue <= 0)
        {
            Time.timeScale = 0.0f;
            EndingScreen.SetActive(true);
            EndingScreen.GetComponent<EndScreen>().EndGame();
            
        }
        if (hasCustomer)
        {
            satisfactionMeter.value -= Time.deltaTime * timerMultiplier;
            if (satisfactionMeter.value > 1.04f)
            {
                satisfactionMeter.GetComponentInChildren<Image>().color = Colors[0];
                SatisfactionFace.sprite = Satisfactionemojis[0];
                SatisfactionText.color = Colors[0];

            }

            else if (satisfactionMeter.value > .38f)
            {
                satisfactionMeter.GetComponentInChildren<Image>().color = Colors[1];
                SatisfactionFace.sprite = Satisfactionemojis[1];
                SatisfactionText.color = Colors[1];

            }

            else
            {
                satisfactionMeter.GetComponentInChildren<Image>().color = Colors[2];
                SatisfactionFace.sprite = Satisfactionemojis[2];
                SatisfactionText.color = Colors[2];

            }

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
        playerScore++;


        ScoreAdded.text = "+" + currentFoodValue;
        playerScore += currentFoodValue;
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



