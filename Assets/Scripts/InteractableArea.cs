﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableArea : MonoBehaviour {

    public float timer = 0;
    public float startTime = 4;
    public bool isInteracting;
    public bool isComplete;
    public AreaType areaType;

    private GameObject interactingUnit;

    public Slider feedbackSlider;
    
    public enum AreaType
    {
        None,
        PreperationArea,
        CookingArea,
        SinkArea,
        ServingArea,
        DirtyDishReturn
    }


    // Use this for initialization
    void Start () {
        switch (areaType)
        {
            case AreaType.None:
                startTime = 0;
                isComplete = false;
                break;
            case AreaType.PreperationArea:
                startTime = 4;
                feedbackSlider.maxValue = 4;
                isComplete = false;
                break;
            case AreaType.CookingArea:
                startTime = 3;
                feedbackSlider.maxValue = 3;
                isComplete = false;
                break;
            case AreaType.SinkArea:
                startTime = 3;
                feedbackSlider.maxValue = 3;
                isComplete = false;
                break;
            case AreaType.ServingArea:
                startTime = 2;
                feedbackSlider.maxValue = 2;
                isComplete = false;
                break;
        }


    }

    // Update is called once per frame
    void Update () {
		if(isInteracting && !isComplete)
        {
            feedbackSlider.gameObject.SetActive(true);
            feedbackSlider.value = timer;
            timer += Time.deltaTime;
            if(timer >= startTime)
            {
                Complete();
            }
        }
        else
        {
            feedbackSlider.gameObject.SetActive(false);

            timer = 0.0f;
        }
	}

    void Complete()
    {
        Debug.Log("COMPLETE");
        isInteracting = false;
        isComplete = true;
        interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = false;
        interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = false;

        if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.Washing)
        {
            interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.CleanPlate;
        }
        else if(interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.RawFood)
        {

           // interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.Serving;
           // StartCoroutine("FlashFeedback");
        }

        //if(interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.CookedFood)
        //{
        //    interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.Serving;
        //}
        //else
        //{
        //    interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.None;
        //    StartCoroutine("FlashFeedback");
        //}
    }

    public IEnumerator FlashFeedback()
    {
        interactingUnit.gameObject.GetComponent<UnitTaskController>().exclamationPoint.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        interactingUnit.gameObject.GetComponent<UnitTaskController>().exclamationPoint.SetActive(false);
        StopCoroutine("FlashFeedback");
    }



    private void OnTriggerStay(Collider other)
    { 
        if(other.gameObject.tag == "Player")
        {
            interactingUnit = other.gameObject;
            if (!isInteracting && !isComplete)//maybe have to move inside of cases
            {
                switch (areaType)
                {
                    case AreaType.None:
                       // StartCoroutine("FlashFeedback");
                        break;
                    case AreaType.PreperationArea:
                        if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.None)
                        {
                            isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.RawFood;
                        }
                        break;
                    case AreaType.CookingArea:
                        if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.CleanPlate)
                        {
                            isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.CookedFood;
                        }
                        else if(interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.RawFood)
                        {
                            interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.None;
                            Debug.Log("RawFood!!");
                        }
                        break;
                    case AreaType.SinkArea:
                        if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.DirtyPlate)
                        {
                            isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.Washing;
                        }
                        else
                        {
                            Debug.Log("Cant wash nothing!!");
                        }
                        break;
                    case AreaType.ServingArea:
                        break;
                    case AreaType.DirtyDishReturn:
                        interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = true;
                        interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = true;
                        interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.DirtyPlate;
                        break;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInteracting = false;
        }
   
    }
}
