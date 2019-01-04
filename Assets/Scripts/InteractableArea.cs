using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableArea : MonoBehaviour {

    public float timer = 0;
    public float startTime = 4;
    public bool isInteracting;
    public bool isComplete;
    public AreaType areaType;
    public int foodServings;

    private GameObject interactingUnit;

    public Slider feedbackSlider;
    public GameObject fillLevel1;
    public GameObject fillLevel2;
    public GameObject fillLevel3;

    public GameManager Gm;
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
                foodServings = 0;
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
            case AreaType.DirtyDishReturn:
                startTime = 0.5f;
                feedbackSlider.maxValue = 0.5f;
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
        if (this.gameObject.name == "CookingInteractableArea")
        {
            switch (foodServings)
            {
                case 0:
                    fillLevel1.SetActive(false);
                    fillLevel2.SetActive(false);
                    fillLevel3.SetActive(false);
                    break;
                case 1:
                    fillLevel1.SetActive(true);
                    fillLevel2.SetActive(false);
                    fillLevel3.SetActive(false);
                    break;
                case 2:
                    fillLevel1.SetActive(false);
                    fillLevel2.SetActive(true);
                    fillLevel3.SetActive(false);
                    break;
                case 3:
                    fillLevel1.SetActive(false);
                    fillLevel2.SetActive(false);
                    fillLevel3.SetActive(true);
                    break;
            }
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
        yield return new WaitForSeconds(1);
        interactingUnit.gameObject.GetComponent<UnitTaskController>().exclamationPoint.SetActive(false);
        StopCoroutine("FlashFeedback");
    }



    private void OnTriggerStay(Collider other)
    { 
        if(other.gameObject.tag == "Player")
        {
            interactingUnit = other.gameObject;
            //if (!isInteracting && !isComplete)//maybe have to move inside of cases
            //{
                switch (areaType)
                {
                    case AreaType.None:
                       // StartCoroutine("FlashFeedback");
                        break;
                    case AreaType.PreperationArea:
                        if (!isInteracting && !isComplete)
                        {
                            if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.None)
                            {
                                isInteracting = true;
                                interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = true;
                                interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = true;
                                interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.RawFood;
                            }
                        }
                        break;
                    case AreaType.CookingArea:
                    if (!isInteracting && !isComplete)
                    {
                         if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.RawFood && foodServings < 3)
                        {
                            isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.None;
                            Debug.Log("Added Food!!");
                            foodServings += 1;
                        }
                        else if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.CleanPlate && foodServings > 0)
                        {
                            isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.CookedFood;
                            foodServings -= 1;
                        }
                    }
                    //else if(!isInteracting && !isComplete)
                    //{
                    //    if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.CleanPlate && foodServings > 0)
                    //    {
                    //        isInteracting = true;
                    //        interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = true;
                    //        interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = true;
                    //        interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.CookedFood;
                    //        foodServings -= 1;
                    //        isComplete = false;
                    //    }
                    //}
                    break;
                    case AreaType.SinkArea:
                        if (!isInteracting && !isComplete)
                        {
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
                        }
                        break;
                    case AreaType.ServingArea:
                    if (!isInteracting && !isComplete)
                    {
                        if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.CookedFood)
                        {
                            Gm.AddScore();
                            Gm.StartNewCustomer();
                            isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.None;
                        }
                    }
                    break;
                    case AreaType.DirtyDishReturn:
                        if (!isInteracting && !isComplete)
                        {
                            isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.DirtyPlate;
                        }
                        break;
                }
           // }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInteracting = false;
            isComplete = false;
        }
   
    }
}
