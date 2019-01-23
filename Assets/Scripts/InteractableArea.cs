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
    public Slider feedbackSlider2;

    //Player UI status
    public Image Status;
    public Image Status2;
    public Sprite[] FeedbackSprites;
    bool FeedBackFiredAlready = false;


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
        DirtyDishReturn,
        TrashCan
    }


    // Use this for initialization
    void Start () {
        // Move the set slider value to collision area
        //switch (areaType)
        //{
        //    case AreaType.None:
        //        startTime = 0;
        //        isComplete = false;
        //        break;
        //    case AreaType.PreperationArea:
        //        startTime = 4;
        //        feedbackSlider.maxValue = 4;
        //        isComplete = false;
        //        break;
        //    case AreaType.CookingArea:
        //        startTime = 3;
        //        feedbackSlider.maxValue = 3;
        //        foodServings = 0;
        //        isComplete = false;
        //        break;
        //    case AreaType.SinkArea:
        //        startTime = 3;
        //        feedbackSlider.maxValue = 3;
        //        isComplete = false;
        //        break;
        //    case AreaType.ServingArea:
        //        startTime = 2;
        //        feedbackSlider.maxValue = 2;
        //        isComplete = false;
        //        break;
        //    case AreaType.DirtyDishReturn:
        //        startTime = 0.5f;
        //        feedbackSlider.maxValue = 0.5f;
        //        isComplete = false;
        //        break;
        //}


    }

    // Update is called once per frame
    void Update () {
        
        //if (isInteracting && !isComplete)
        //{
        //    feedbackSlider.gameObject.SetActive(true);
        //    feedbackSlider.value = timer;
        //    timer += Time.deltaTime;
        //    if(timer >= startTime)
        //    {
        //        Complete();
        //    }
        //}
        //else
        //{
        //    feedbackSlider.gameObject.SetActive(false);
        //    timer = 0.0f;
        //}
        if (gameObject.name == "CookingInteractableArea")
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

    void Complete(AreaType type , Image Player)
    {
        Debug.Log("COMPLETE");
        isInteracting = false;
        isComplete = true;
        interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = false;
        interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = false;
        StartCoroutine(FlashFeedback(Player, FeedbackSprites[2]));

        switch (type)
        {
            case AreaType.None:
                break;

            case AreaType.PreperationArea:
                interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.RawFood;
                break;

            case AreaType.CookingArea:
               
                if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.RawFood )
                {
                    interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.None;
                    Debug.Log("Added Food!!");
                    foodServings += 1;
                }
                else if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.CleanPlate)
                {
                     interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.CookedFood;
                     foodServings -= 1;
                }
                break;

            case AreaType.SinkArea:
                interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.CleanPlate;
                break;

            case AreaType.ServingArea:
                Gm.AddScore();
                Gm.StartNewCustomer();
                interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.Washing;
                break;

            case AreaType.DirtyDishReturn:
                interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.DirtyPlate;
                break;

            case AreaType.TrashCan:
                interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.None;
                break;
        }

        if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.Washing)
        {
           // interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.CleanPlate;
        }
        else if(interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.None)
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

    public IEnumerator FlashFeedback(Image Player,Sprite image)
    {
        // interactingUnit.gameObject.GetComponent<UnitTaskController>().exclamationPoint.SetActive(true);
        Player.gameObject.SetActive(true);
        Player.sprite = image;
        yield return new WaitForSeconds(1);

        Player.gameObject.SetActive(false);
        //interactingUnit.gameObject.GetComponent<UnitTaskController>().exclamationPoint.SetActive(false);
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
                    interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.None;
                    break;

                case AreaType.PreperationArea:
                    if (!isInteracting && !isComplete)
                    {
                        startTime = 4;
                        feedbackSlider.maxValue = 4;
                        if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.None)
                        {
                            isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = true;
                           // interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.RawFood;
                        }
                        else
                        {
                            if (FeedBackFiredAlready == false)
                            {
                                NegativeFeedback(other);
                            }
                           

                        }
                    }
                    break;
                case AreaType.CookingArea:
                    if (!isInteracting && !isComplete)
                    {
                        startTime = 3;
                        feedbackSlider.maxValue = 3;
                        if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.RawFood && foodServings < 3)
                        {
                            isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = true;
                           // interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.None;
                           // Debug.Log("Added Food!!");
                           // foodServings += 1;
                        }
                        else if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.CleanPlate && foodServings > 0)
                        {
                            isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = true;
                          //  interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.CookedFood;
                           // foodServings -= 1;
                        }

                        else
                        {
                            if (FeedBackFiredAlready == false)
                            {
                                NegativeFeedback(other);
                            }
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
                        startTime = 3;
                        feedbackSlider.maxValue = 3;
                        if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.DirtyPlate)
                        {
                            isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = true;
                           // interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.Washing;
                        }
                        else
                        {
                            Debug.Log("Cant wash nothing!!");
                            if (FeedBackFiredAlready == false)
                            {
                                NegativeFeedback(other);
                            }
                        }

                    }
                    break;
                case AreaType.ServingArea:
                    if (!isInteracting && !isComplete)
                    {
                        startTime = 2;
                        feedbackSlider.maxValue = 2;
                        if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.CookedFood)
                        {
                            Gm.AddScore();
                            Gm.StartNewCustomer();
                            isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = true;
                           // interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.None;
                        }

                        else
                        {
                            if (FeedBackFiredAlready == false)
                            {
                                NegativeFeedback(other);
                            }
                        }
                    }
                    break;
                case AreaType.DirtyDishReturn:
                    if (!isInteracting && !isComplete)
                    {
                        startTime = 0.5f;
                        feedbackSlider.maxValue = 0.5f;
                        isInteracting = true;
                        interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = true;
                        interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = true;
                        interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.DirtyPlate;
                    }
                    break;
                case AreaType.TrashCan:
                    startTime = 0.5f;
                    feedbackSlider.maxValue = 0.5f;
                    isInteracting = true;
                    interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = true;
                    interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = true;
                    interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.None;
                    break;
            }
            if (other.gameObject.name == "Unit1")
            {
                if (isInteracting && !isComplete)
                {


                    feedbackSlider.gameObject.SetActive(true);
                    //setImage
                    Status.gameObject.SetActive(true);
                    SwitchImage(FeedbackSprites[0], Status);

                    feedbackSlider.value = timer;
                    timer += Time.deltaTime;
                    if (timer >= startTime)
                    {
                        Complete(areaType,Status);
                    }
                }
                else
                {
                    feedbackSlider.gameObject.SetActive(false);
                    //Status.gameObject.SetActive(false);

                    timer = 0.0f;
                }
            }else if(other.gameObject.name =="Unit2")
            {
                if (isInteracting && !isComplete)
                {


                    feedbackSlider2.gameObject.SetActive(true);
                    Status2.gameObject.SetActive(true);
                    SwitchImage(FeedbackSprites[0], Status2);

                    feedbackSlider2.value = timer;
                    timer += Time.deltaTime;
                    if (timer >= startTime)
                    {
                        Complete(areaType,Status2);
                    }
                }
                else
                {
                    feedbackSlider2.gameObject.SetActive(false);
                    Status2.gameObject.SetActive(false);
                    timer = 0.0f;
                }
            }
            Debug.Log(feedbackSlider.maxValue);
            // }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInteracting = false;
            isComplete = false;
            FeedBackFiredAlready = false;
            Status.gameObject.SetActive(false);
            Status2.gameObject.SetActive(false);
            feedbackSlider.gameObject.SetActive(false);
            feedbackSlider2.gameObject.SetActive(false);
        }
   
    }


    public void SwitchImage(Sprite newimage,Image Player)
    {
        Player.sprite = newimage;
    }

    void NegativeFeedback(Collider target)
    {
        FeedBackFiredAlready = true;
            if (target.gameObject.name == "Unit1")
            {
                StartCoroutine(FlashFeedback(Status, FeedbackSprites[1]));
            }

            else if (target.gameObject.name == "Unit2")
            {
                StartCoroutine(FlashFeedback(Status2, FeedbackSprites[1]));
            }
        }
    }



