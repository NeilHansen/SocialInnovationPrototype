using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

    // Player task UI
    public Slider TaskProgressSlider;
    public Image Status;
    public Sprite[] FeedbackSprites;
    public float CurrentProgress=0;
    

    public bool FeedBackFire = false;
    public UnitTaskController Unit;
    

    



	// Use this for initialization
	void Start () {
        TaskProgressSlider.gameObject.SetActive(false);
        Status.gameObject.SetActive(false);
		
	}
	
	// Update is called once per frame
	void Update () {


		
	}


    public void TaskInProgress(float TimeToComplete)
    {
        TaskProgressSlider.gameObject.SetActive(true);
        Status.gameObject.SetActive(true);
        TaskProgressSlider.maxValue = TimeToComplete;
        Status.sprite = FeedbackSprites[0];
        TaskProgressSlider.value = CurrentProgress;
        //Handles task bar
  



    }


    public void TurnOffUI()
    {
        TaskProgressSlider.gameObject.SetActive(false);
        Status.gameObject.SetActive(false);
        CurrentProgress = 0;
        FeedBackFire = false;

    }

    



    //IENUMERATORS 

    public IEnumerator BeginProgressBar()
    {

        while (CurrentProgress < TaskProgressSlider.maxValue)
        {
            CurrentProgress += Time.deltaTime;
            TaskProgressSlider.value = CurrentProgress;

            if (!Unit.isInteracting)
            {
                //If the player stops interacting shut it down
                TurnOffUI();
                yield break;
            }

        }
        //On complete flash postive feedback and stop progress bar
        CurrentProgress = 0;
        StartCoroutine(FlashFeedback(true));

        yield break;
    }


    public IEnumerator FlashFeedback(bool IsPositive)
    {
        TaskProgressSlider.gameObject.SetActive(false);
        Status.gameObject.SetActive(true);

        if (IsPositive)
        {
            Status.sprite = FeedbackSprites[1];
        }
        else
        {
            Status.sprite = FeedbackSprites[2];
        }
        CurrentProgress = 0;
        yield return new WaitForSeconds(1);

        Status.gameObject.SetActive(false);
       

    }

    



 



}
