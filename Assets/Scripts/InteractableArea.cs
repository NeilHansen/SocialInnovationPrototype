using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InteractableArea : MonoBehaviour
{

    public float timer = 0;
    public float startTime = 4;
    public bool isInteracting;
    public bool isComplete;
    public AreaType areaType;
    public int foodServings;

    private GameObject interactingUnit;
    private Customer[] customers;
    public GameObject UnitToMoveTo;
    private RtsMover rtsMover;

    public Slider feedbackSlider;
    public Slider feedbackSlider2;
    public Slider feedbackSlider3;
    public Slider feedbackSlider4;


    //Player UI status
    public Image Status;
    public Image Status2;
    public Image Status3;
    public Image Status4;
    public Sprite[] FeedbackSprites;
    bool FeedBackFiredAlready = false;


    public GameObject fillLevel1;
    public GameObject fillLevel2;
    public GameObject fillLevel3;

    public GameManager Gm;

    public GameObject carryWood;
    public GameObject CarryPipe;

    public Sprite hoverSprite;
    public GameObject hoverSpriteObject;

    [SerializeField]
    Transform MovePoint;

    public GameObject giftboxPrefab;

    public GameObject objectToplace;
    private bool hasCompleted = false;
    public GameObject ConsoleCanvas;

    public enum AreaType
    {
        None,
        PreperationArea,
        CookingArea,
        SinkArea,
        ServingArea,
        DirtyDishReturn,
        TrashCan,
        Counter,
        GiftBox,
        Truck,
        DollBin,
        BallBin,
        RobotBin,
        BaseballBatBin,
        TvStand,
        TvBox,
        ComputerBox,
        ClothesBox,
        ComputerDesk,
        Closet
    }

    //Put things on the counters
    private bool isOnCounter = false;
    private CounterSpace counterSpace;
    public bool dirtyPlateOn, cleanPlateOn, filledPlateOn, rawFoodOn;
    UnitTaskController.ObjectHeld objectPlayerHolding;

    // Use this for initialization
    void Start()
    {
        rtsMover = Camera.main.GetComponent<RtsMover>();
        Gm = GameObject.FindObjectOfType<GameManager>();
        counterSpace = gameObject.GetComponent<CounterSpace>();
        InvokeRepeating("TestDebug", 0.0f, 1.0f);
     
        feedbackSlider = GameObject.FindGameObjectWithTag("PlayerCanvas1").transform.GetChild(0).GetComponent<Slider>();
        Status = GameObject.FindGameObjectWithTag("PlayerCanvas1").transform.GetChild(1).GetComponent<Image>();
        if (rtsMover.numberofunits > 2)
        {
            feedbackSlider2 = GameObject.FindGameObjectWithTag("Player Canvas2").transform.GetChild(0).GetComponent<Slider>();
            feedbackSlider3 = GameObject.FindGameObjectWithTag("Player Canvas3").transform.GetChild(0).GetComponent<Slider>();
            feedbackSlider4 = GameObject.FindGameObjectWithTag("Player Canvas4").transform.GetChild(0).GetComponent<Slider>();
            Status2 = GameObject.FindGameObjectWithTag("Player Canvas2").transform.GetChild(1).GetComponent<Image>();
            Status3 = GameObject.FindGameObjectWithTag("Player Canvas3").transform.GetChild(1).GetComponent<Image>();
            Status4 = GameObject.FindGameObjectWithTag("Player Canvas4").transform.GetChild(1).GetComponent<Image>();
        }
       
       
        

        hoverSpriteObject.GetComponent<Image>().sprite = hoverSprite;
        hoverSpriteObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        //  Debug.Log(isOnCounter + " " + objectPlayerHolding);
        customers = FindObjectsOfType<Customer>();
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

        if (cleanPlateOn)
            counterSpace.ObjectCleanPlate();

        if (dirtyPlateOn || cleanPlateOn || filledPlateOn || rawFoodOn)
            isOnCounter = true;
        else
            isOnCounter = false;
    }

    void Complete(AreaType type, PlayerUI UI)
    {
        //    Debug.Log("COMPLETE");
        isInteracting = false;
        isComplete = true;
        interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = false;
        interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = false;
        StartCoroutine(UI.FlashFeedback(true));

        switch (type)
        {
            case AreaType.None:
                break;

            case AreaType.PreperationArea:
                interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.RawFood;
                break;

            case AreaType.CookingArea:

                if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.RawFood)
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
                interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.None;
                foreach (Customer c in customers)
                {
                    c.isMoving = true;
                }
                break;

            case AreaType.DirtyDishReturn:
                interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.DirtyPlate;
                break;

            case AreaType.TrashCan:
                interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.None;
                break;

            case AreaType.GiftBox:
                if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.Doll)
                {
                    interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.None;
                    this.gameObject.GetComponentInChildren<ToyOrders>().ReceiveItem(ToyOrders.ToyList.Doll);
                    Debug.Log("Added Doll!!");
                }
                else if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.Ball)
                {
                    interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.None;
                    this.gameObject.GetComponentInChildren<ToyOrders>().ReceiveItem(ToyOrders.ToyList.Ball);
                    Debug.Log("Added Ball!!");
                }
                else if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.Robot)
                {
                    interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.None;
                    this.gameObject.GetComponentInChildren<ToyOrders>().ReceiveItem(ToyOrders.ToyList.Robot);
                    Debug.Log("Added Robot!!");
                }
                else if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.BaseballBat)
                {
                    interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.None;
                    this.gameObject.GetComponentInChildren<ToyOrders>().ReceiveItem(ToyOrders.ToyList.BaseballBat);
                    Debug.Log("Added Robot!!");
                }
                else if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.None)
                {
                    interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.GiftBox;
                    Destroy(this.gameObject.transform.GetChild(2).gameObject);
                    this.gameObject.GetComponent<GiftboxSpawn>().hasBox = false;
                }
                break;

            case AreaType.Truck:
                interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.None;
                //Add Score to truck
                GetComponent<ToyTruck>().AddPackage();
                //Respawn box   
                GiftboxSpawn[] boxSpawnnerArray = FindObjectsOfType<GiftboxSpawn>();
                foreach (GiftboxSpawn item in boxSpawnnerArray)
                {
                    if (item.hasBox == false)
                    {
                        item.Respawn();
                    }
                }
                break;
            case AreaType.DollBin:
                interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.Doll;
                break;
            case AreaType.BallBin:
                interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.Ball;
                break;
            case AreaType.RobotBin:
                interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.Robot;
                break;
            case AreaType.BaseballBatBin:
                interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.BaseballBat;
                break;
            case AreaType.TvStand:
                if(!hasCompleted)
                {
                    if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.Console)
                    {
                        interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.None;
                        objectToplace.SetActive(true);
                        hasCompleted = true;
                        Debug.Log("placed console!!");
                    }
                }
                else
                {
                    //if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.None)
                    //{
                    ConsoleCanvas.SetActive(true);
                    //}
                }
                break;
            case AreaType.TvBox:
                if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.None)
                {
                    interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.Console;
                    Destroy(this.gameObject);
                    Debug.Log("Picked up console!!");
                }
                break;
            case AreaType.ComputerDesk:
                if (!hasCompleted)
                {
                    if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.Computer)
                    {
                        interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.None;
                        objectToplace.SetActive(true);
                        hasCompleted = true;
                        Debug.Log("placed computer!!");
                    }
                }
                else
                {

                }
                break;
            case AreaType.ComputerBox:
                if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.None)
                {
                    interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.Computer;
                    Destroy(this.gameObject);
                    Debug.Log("Picked up Computer!!");
                }
                break;
            case AreaType.Closet:
                if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.Clothes)
                {
                    interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.None;

                    Debug.Log("placed clothes!!");
                }
                break;
            case AreaType.ClothesBox:
                if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.None)
                {
                    interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.Clothes;
                    Destroy(this.gameObject);
                    Debug.Log("Picked up clothes!!");
                }
                break;
            #region
            case AreaType.Counter:
                //Nothing is on the counter
                if (!isOnCounter)
                {
                    switch (objectPlayerHolding)
                    {
                        case UnitTaskController.ObjectHeld.DirtyPlate:
                            counterSpace.ObjectDirtyPlate();
                            dirtyPlateOn = true;
                            break;
                        case UnitTaskController.ObjectHeld.CleanPlate:
                            counterSpace.ObjectCleanPlate();
                            cleanPlateOn = true;
                            break;
                        case UnitTaskController.ObjectHeld.FilledPlate:
                            counterSpace.ObjectFilledPlate();
                            filledPlateOn = true;
                            break;
                        case UnitTaskController.ObjectHeld.RawFood:
                            counterSpace.ObjectRawFood();
                            rawFoodOn = true;
                            break;
                    }
                    interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.None;
                }
                //Counter has something
                else
                {
                    //Player has nothing
                    if (objectPlayerHolding == UnitTaskController.ObjectHeld.None)
                    {
                        if (dirtyPlateOn)
                        {
                            dirtyPlateOn = false;
                            interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.DirtyPlate;
                        }
                        if (cleanPlateOn)
                        {
                            cleanPlateOn = false;
                            interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.CleanPlate;
                        }
                        if (filledPlateOn)
                        {
                            filledPlateOn = false;
                            interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.CookedFood;
                        }
                        if (rawFoodOn)
                        {
                            rawFoodOn = false;
                            interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.RawFood;
                        }
                        counterSpace.ObjectNone();
                    }
                    //Player has something, gotta go through which object it is
                    else
                    {
                        switch (objectPlayerHolding)
                        {
                            case UnitTaskController.ObjectHeld.DirtyPlate:
                                //Same object dirty plate, do nothing
                                if (dirtyPlateOn)
                                    break;
                                //Different object, switch place
                                else
                                {
                                    if (cleanPlateOn)
                                    {
                                        cleanPlateOn = false;
                                        interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.CleanPlate;
                                    }
                                    else if (filledPlateOn)
                                    {
                                        filledPlateOn = false;
                                        interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.CookedFood;
                                    }
                                    else if (rawFoodOn)
                                    {
                                        rawFoodOn = false;
                                        interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.RawFood;
                                    }
                                    dirtyPlateOn = true;
                                    counterSpace.ObjectDirtyPlate();
                                }
                                break;
                            case UnitTaskController.ObjectHeld.CleanPlate:
                                //Same object clean plate, do nothing
                                if (cleanPlateOn)
                                    break;
                                //Different object, switch place
                                else
                                {
                                    if (dirtyPlateOn)
                                    {
                                        dirtyPlateOn = false;
                                        interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.DirtyPlate;
                                    }
                                    else if (filledPlateOn)
                                    {
                                        filledPlateOn = false;
                                        interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.CookedFood;
                                    }
                                    else if (rawFoodOn)
                                    {
                                        rawFoodOn = false;
                                        interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.RawFood;
                                    }
                                    cleanPlateOn = true;
                                    counterSpace.ObjectCleanPlate();
                                }
                                break;
                            case UnitTaskController.ObjectHeld.FilledPlate:
                                //Same object filled plate, do nothing
                                if (filledPlateOn)
                                    break;
                                //Different object, switch place
                                else
                                {
                                    if (dirtyPlateOn)
                                    {
                                        dirtyPlateOn = false;
                                        interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.DirtyPlate;
                                    }
                                    else if (cleanPlateOn)
                                    {
                                        cleanPlateOn = false;
                                        interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.CleanPlate;
                                    }
                                    else if (rawFoodOn)
                                    {
                                        rawFoodOn = false;
                                        interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.RawFood;
                                    }
                                    filledPlateOn = true;
                                    counterSpace.ObjectFilledPlate();
                                }
                                break;
                            case UnitTaskController.ObjectHeld.RawFood:
                                //Same object raw food, do nothing
                                if (rawFoodOn)
                                    break;
                                //Different object, switch place
                                else
                                {
                                    if (dirtyPlateOn)
                                    {
                                        dirtyPlateOn = false;
                                        interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.DirtyPlate;
                                    }
                                    else if (cleanPlateOn)
                                    {
                                        cleanPlateOn = false;
                                        interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.CleanPlate;
                                    }
                                    else if (filledPlateOn)
                                    {
                                        filledPlateOn = false;
                                        interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.CookedFood;
                                    }
                                    rawFoodOn = true;
                                    counterSpace.ObjectRawFood();
                                }
                                break;
                        }
                    }
                }
                break;
                #endregion
        }


    }



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject == UnitToMoveTo)
        {
            interactingUnit = other.gameObject;

            switch (areaType)
            {
                case AreaType.None:
                    interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.None;
                    break;

                case AreaType.PreperationArea:
                    if (!isInteracting && !isComplete)
                    {
                        if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.None)
                        {
                            OnInteraction(interactingUnit);
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
                        if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.RawFood && foodServings < 3)
                        {
                            OnInteraction(interactingUnit);
                        }
                        else if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.CleanPlate && foodServings > 0)
                        {
                            OnInteraction(interactingUnit);
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

                case AreaType.SinkArea:
                    if (!isInteracting && !isComplete)
                    {
                        if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.DirtyPlate)
                        {
                            OnInteraction(interactingUnit);
                            //isInteracting = true;
                            //interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = true;
                            //interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = true;
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
                        if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.CookedFood)
                        {
                            OnInteraction(interactingUnit);
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
                        if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.None)
                        {
                            OnInteraction(interactingUnit);
                            interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.DirtyPlate;
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

                case AreaType.TrashCan:
                    if (!isInteracting && !isComplete)
                    {
                        if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType != UnitTaskController.TaskType.None)
                        {
                            OnInteraction(interactingUnit);
                            interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.None;
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

                case AreaType.Counter:
                    if (!isInteracting && !isComplete)
                    {
                        objectPlayerHolding = interactingUnit.gameObject.GetComponent<UnitTaskController>().objectHolding;
                        //Nothing on counter
                        ///
                        if (isOnCounter || objectPlayerHolding != UnitTaskController.ObjectHeld.None)
                        {
                            OnInteraction(interactingUnit);
                        }
                    }
                    break;

                case AreaType.GiftBox:
                    if (!isInteracting && !isComplete)
                    {
                        if (interactingUnit.gameObject.name == "Unit1" && GetComponentInChildren<ToyOrders>().canPickUp)
                        {
                            if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.None
                            && this.gameObject.GetComponentInChildren<ToyOrders>().canPickUp == true)
                            {
                                OnInteraction(interactingUnit);
                            }
                            else
                            {
                                if (FeedBackFiredAlready == false)
                                {
                                    NegativeFeedback(other);
                                }
                            }
                        }
                        else //if (interactingUnit.gameObject.name != "Unit1")
                        {
                            if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType != UnitTaskController.TaskType.None)
                            {
                                OnInteraction(interactingUnit);
                            }
                            else
                            {
                                if (FeedBackFiredAlready == false)
                                {
                                    NegativeFeedback(other);
                                }
                            }
                        }

                    }
                    break;

                case AreaType.Truck:
                    if (!isInteracting && !isComplete)
                    {
                        if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.GiftBox && interactingUnit.gameObject.name == "Unit1")
                        {
                            OnInteraction(interactingUnit);
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

                case AreaType.DollBin:
                    if (!isInteracting && !isComplete)
                    {
                        if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.None)
                        {
                            OnInteraction(interactingUnit);
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

                case AreaType.BallBin:
                    if (!isInteracting && !isComplete)
                    {
                        if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.None)
                        {
                            OnInteraction(interactingUnit);
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

                case AreaType.RobotBin:
                    if (!isInteracting && !isComplete)
                    {
                        if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.None)
                        {
                            OnInteraction(interactingUnit);
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

                case AreaType.BaseballBatBin:
                    if (!isInteracting && !isComplete)
                    {
                        if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.None)
                        {
                            OnInteraction(interactingUnit);
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

                case AreaType.TvStand:
                    if (!isInteracting && !isComplete)
                    {
                        if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.Console)
                        {
                            OnInteraction(interactingUnit);
                        }
                        else if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.None && hasCompleted)
                        {

                            OnInteraction(interactingUnit);

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

                case AreaType.TvBox:
                    if (!isInteracting && !isComplete)
                    {
                        if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.None)
                        {
                            OnInteraction(interactingUnit);
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

                case AreaType.ComputerDesk:
                    if (!isInteracting && !isComplete)
                    {
                        if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.Computer)
                        {
                            OnInteraction(interactingUnit);
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

                case AreaType.ComputerBox:
                    if (!isInteracting && !isComplete)
                    {
                        if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.None)
                        {
                            OnInteraction(interactingUnit);
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

                case AreaType.Closet:
                    if (!isInteracting && !isComplete)
                    {
                        if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.Clothes)
                        {
                            OnInteraction(interactingUnit);
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

                case AreaType.ClothesBox:
                    if (!isInteracting && !isComplete)
                    {
                        if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.None)
                        {
                            OnInteraction(interactingUnit);
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

            }


            if (other.gameObject.GetComponentInChildren<PlayerUI>())
            {
                PlayerUI UI = other.gameObject.GetComponentInChildren<PlayerUI>();

                if (isInteracting && !isComplete)
                {
                    UI.TaskInProgress(startTime);
                    UI.CurrentProgress += Time.deltaTime;

                    if (UI.CurrentProgress >= startTime)
                    {
                        Complete(areaType, UI);
                    }
                }
            }
        }
    }


    

    private void OnInteraction(GameObject interactingunit)
    {
        isInteracting = true;
        interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = true;
        interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInteracting = false;
            isComplete = false;
            UnitToMoveTo = null;
            if (other.GetComponentInChildren<PlayerUI>())
            {
                other.GetComponentInChildren<PlayerUI>().TurnOffUI();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            interactingUnit = other.gameObject;
            switch (areaType)
            {
                case AreaType.Counter:
                    objectPlayerHolding = interactingUnit.gameObject.GetComponent<UnitTaskController>().objectHolding;
                    break;
            }
        }
    }

    public void SwitchImage(Sprite newimage, Image Player)
    {
        Player.sprite = newimage;
    }

    void NegativeFeedback(Collider target)
    {
        if (target.gameObject.GetComponentInChildren<PlayerUI>() && !target.gameObject.GetComponentInChildren<PlayerUI>().FeedBackFire)
        {
            StartCoroutine(target.GetComponentInChildren<PlayerUI>().FlashFeedback(false));
            target.gameObject.GetComponentInChildren<PlayerUI>().FeedBackFire = true;

        }
    }

    public void SetInteractorAndMove()
    {

        UnitToMoveTo = rtsMover.ActiveUnit;
        if (MovePoint != null)
        {
            rtsMover.MovePlayer(MovePoint);
        }




    }

    public IEnumerator FlashFeedback(Image Player, Sprite image)
    {
        Player.gameObject.SetActive(true);
        Player.sprite = image;
        yield return new WaitForSeconds(1);

        Player.gameObject.SetActive(false);
        StopCoroutine("FlashFeedback");
    }

    void TestDebug()
    {

    }

    private void OnMouseOver()
    {
        hoverSpriteObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        hoverSpriteObject.SetActive(false);
    }
}
