using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {


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



    // Use this for initialization
    void Start() {
        timerText.text = "" + (int)timeValue;
        timerTextBG.text = "" + (int)timeValue;
        satisfactionMeter.maxValue = defautlSatisfactionLevel;
        StartNewCustomer();

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
        }

        if (hasCustomer)
        {
            satisfactionMeter.value -= Time.deltaTime * 0.35f;
        }

    }

    public void StartNewCustomer()
    {
        satisfactionMeter.value = defautlSatisfactionLevel;
        hasCustomer = true;
    }

    public void AddScore()
    {
        currentFoodValue = defaultFoodValue * satisfactionMeter.value;
        playerScore += currentFoodValue;
        scoreText.text = "Score: " + (int)playerScore;
    }

}
