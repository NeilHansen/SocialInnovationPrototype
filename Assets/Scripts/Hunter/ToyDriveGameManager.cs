﻿using System.Collections;
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

    float TimeTruckStarted;

    //public float timerMultiplier = 0.1f;


    public bool isBonusMultiplierOn = false;
    public TextMeshProUGUI PackageCounter;




    // Use this for initialization
    void Start()
    {
     

        satisfactionMeter.maxValue = 1;
       
      
    }



    // Update is called once per frame
    void Update()
    {
        //Game timer
        timeValue -= 1 * Time.deltaTime;
        TimeRemaining.text =""+(int)timeValue ;

        PackageCounter.text = "packages: " + Truck.packages + "/2";
     
     
        if (timeValue <= 0)
        {
            Time.timeScale = 0.0f;
            EndingScreen.SetActive(true);
            EndingScreen.GetComponent<ToysEndGame>().EndGame();

        }

        TruckWaitMeter();






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
    }






    //Flashes the amount of points the player received

    public void TruckWaitMeter()
    {
        if (hasTruck)
        {
            satisfactionMeter.value = (Truck.TruckWaitTime - (Time.time - TimeTruckStarted))/Truck.TruckWaitTime;

            


           
            if (satisfactionMeter.value > .70f)
            {
                satisfactionMeter.GetComponentInChildren<Image>().color = Colors[0];
                //SatisfactionFace.sprite = Satisfactionemojis[0];
                SatisfactionText.color = Colors[0];

            }

            else if (satisfactionMeter.value > .245f)
            {
                satisfactionMeter.GetComponentInChildren<Image>().color = Colors[1];
               // SatisfactionFace.sprite = Satisfactionemojis[1];
                SatisfactionText.color = Colors[1];

            }

            else
            {
                satisfactionMeter.GetComponentInChildren<Image>().color = Colors[2];
                //SatisfactionFace.sprite = Satisfactionemojis[2];
                SatisfactionText.color = Colors[2];

            }

            if(satisfactionMeter.value <= 0)
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
