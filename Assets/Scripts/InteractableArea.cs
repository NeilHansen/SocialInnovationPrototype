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
    [SerializeField]
    Transform MovePoint2;

    public GameObject giftboxPrefab;

    public GameObject objectToplace;
    private bool hasCompleted = false;
    public GameObject ConsoleCanvas;
    public GameObject CharacterCreator;

    public TutorialManager tm;

    public bool TutorialComplete;
    public GameObject tv;
    public GameObject comp;

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
        this.gameObject.GetComponent<Outline>().enabled = false;

        if (TutorialComplete)
        {
            switch (areaType)
            {
                case AreaType.TvStand:
                    tv.SetActive(true);
                    break;
                case AreaType.ComputerDesk:
                    comp.SetActive(true);
                    break;
            }
        }else
        {
            switch (areaType)
            {
                case AreaType.TvStand:
                    tv.SetActive(false);
                    break;
                case AreaType.ComputerDesk:
                    comp.SetActive(false);
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //  Debug.Log(isOnCounter + " " + objectPlayerHolding);
        customers = FindObjectsOfType<Customer>();
        if (gameObject.name == "StoveInteractable")
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



        if (TutorialComplete)
        {
            switch (areaType)
            {
                case AreaType.TvStand:
                    tv.SetActive(true);
                    break;
                case AreaType.ComputerDesk:
                    comp.SetActive(true);
                    break;
            }
        }
        else
        {
            switch (areaType)
            {
                case AreaType.TvStand:
                    tv.SetActive(false);
                    break;
                case AreaType.ComputerDesk:
                    comp.SetActive(false);
                    break;
            }
        }

        OnMouseOver();
    }

    void Complete(AreaType type, PlayerUI UI)
    {
        //    Debug.Log("COMPLETE");
        isInteracting = false;
        //isComplete = true;
        interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = false;
        interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = false;
        interactingUnit.gameObject.GetComponent<UnitTaskController>().IsComplete = true;

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
                    //this.gameObject.GetComponentInChildren<ToyOrders>().ReceiveItem(ToyOrders.ToyList.Doll);
                    this.gameObject.GetComponent<GiftboxSpawn>().giftboxPrefab.GetComponent<ToyOrders>().ReceiveItem(ToyOrders.ToyList.Doll);
                    Debug.Log("Added Doll!!");
                }
                else if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.Ball)
                {
                    interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.None;
                   // this.gameObject.GetComponentInChildren<ToyOrders>().ReceiveItem(ToyOrders.ToyList.Ball);
                    this.gameObject.GetComponent<GiftboxSpawn>().giftboxPrefab.GetComponent<ToyOrders>().ReceiveItem(ToyOrders.ToyList.Ball);
                    Debug.Log("Added Ball!!");
                }
                else if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.Robot)
                {
                    interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.None;
                   // this.gameObject.GetComponentInChildren<ToyOrders>().ReceiveItem(ToyOrders.ToyList.Robot);
                    this.gameObject.GetComponent<GiftboxSpawn>().giftboxPrefab.GetComponent<ToyOrders>().ReceiveItem(ToyOrders.ToyList.Robot);
                    Debug.Log("Added Robot!!");
                }
                else if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.BaseballBat)
                {
                    interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.None;
                   // this.gameObject.GetComponentInChildren<ToyOrders>().ReceiveItem(ToyOrders.ToyList.BaseballBat);
                    this.gameObject.GetComponent<GiftboxSpawn>().giftboxPrefab.GetComponent<ToyOrders>().ReceiveItem(ToyOrders.ToyList.BaseballBat);
                    Debug.Log("Added Robot!!");
                }
                else if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.None)
                {
                    interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.GiftBox;
                    //Destroy(this.gameObject.transform.GetChild(2).gameObject);
                    this.gameObject.GetComponent<GiftboxSpawn>().hasBox = false;
                   // this.gameObject.SetActive(false);

                    this.gameObject.GetComponent<GiftboxSpawn>().giftboxPrefab.SetActive(false);


                }
                break;

            case AreaType.Truck:
                interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.None;
                //Add Score to truck
                GetComponent<ToyTruck>().AddPackage();
                //this.gameObject.GetComponent<GiftboxSpawn>().hasBox = false;
                //Respawn box   
                GiftboxSpawn[] boxSpawnnerArray = FindObjectsOfType<GiftboxSpawn>();
                foreach (GiftboxSpawn item in boxSpawnnerArray)
                {
                    if (item.hasBox == false)
                    {
                        //this.gameObject.GetComponent<GiftboxSpawn>().giftboxPrefab.SetActive(true);
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
                if(!hasCompleted && !TutorialComplete)
                {
                    if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.Console)
                    {
                        interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.None;
                        objectToplace.SetActive(true);
                        hasCompleted = true;
                        Debug.Log("placed console!!");
                        tv.SetActive(true);
                        TutorialComplete = true;
                        tm.NextTutorialPeice();
                        ExitInteraction();
                    }
                }
                else 
                {
                    if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.None)
                    {
                    ConsoleCanvas.SetActive(true);
                    ExitInteraction();
                    }
                }
                break;
            case AreaType.TvBox:
                if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.None )
                {
                    interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.Console;
                    //Destroy(this.gameObject);
                    ExitInteraction();
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
                        TutorialComplete = true;
                        tm.NextTutorialPeice();
                        comp.SetActive(true);
                    }
                }
                else
                {

                }
                break;
            case AreaType.ComputerBox:
                if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.None )
                {
                    interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.Computer;
                       this.gameObject.SetActive(false);
                    ExitInteraction();
                  //  Destroy(this.gameObject);
                    Debug.Log("Picked up Computer!!");

                }
                break;
            case AreaType.Closet:
                if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.Clothes)
                {
                    interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.None;
                    tm.NextTutorialPeice();
                    TutorialComplete = true;
                    //this.gameObject.SetActive(false);
                    Debug.Log("placed clothes!!");
                }
                else
                {
                    if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.None)
                    {
                        CharacterCreator.SetActive(true);
                        ExitInteraction();
                    }
                }
                break;
            case AreaType.ClothesBox:
                if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.None )
                {
                    interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.Clothes;
                    // Destroy(this.gameObject);
                    ExitInteraction();
                    //  Destroy(this.gameObject);
                    this.gameObject.SetActive(false);
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
                    if (!isInteracting && interactingUnit.gameObject.GetComponent<UnitTaskController>().IsComplete == false)
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
                    if (!isInteracting && interactingUnit.gameObject.GetComponent<UnitTaskController>().IsComplete==false)
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
                    if (/*!isInteracting &&*/ interactingUnit.gameObject.GetComponent<UnitTaskController>().IsComplete == false)
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
                    if (!isInteracting && interactingUnit.gameObject.GetComponent<UnitTaskController>().IsComplete == false)
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
                    if (!isInteracting && interactingUnit.gameObject.GetComponent<UnitTaskController>().IsComplete == false)
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
                    if (!isInteracting && interactingUnit.gameObject.GetComponent<UnitTaskController>().IsComplete == false)
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
                    if (!isInteracting && interactingUnit.gameObject.GetComponent<UnitTaskController>().IsComplete == false)
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
                    if (!isInteracting && interactingUnit.gameObject.GetComponent<UnitTaskController>().IsComplete == false)
                    {

                        //for picking Up the box
                        if (  this.gameObject.GetComponent<GiftboxSpawn>().giftboxPrefab.GetComponent<ToyOrders>().canPickUp)
                        {
                            if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.None
                            && this.gameObject.GetComponent<GiftboxSpawn>().giftboxPrefab.GetComponent<ToyOrders>().canPickUp == true)
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
                            objectPlayerHolding = interactingUnit.gameObject.GetComponent<UnitTaskController>().objectHolding;
                            ToyOrders TO = FindObjectOfType<ToyOrders>();
                            bool FoundItem = false;

                            foreach(ToyOrders.ItemInfo Item in TO.finalItemList)
                            {
                                if (Item.toy.ToString() == objectPlayerHolding.ToString()&&Item.amount>0)
                                {
                                    FoundItem = true;
                                    break;
                                }
                                else
                                {
                                    FoundItem = false;
                                }
                            }


                            if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType != UnitTaskController.TaskType.None&& FoundItem)
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
                    if (!isInteracting && interactingUnit.gameObject.GetComponent<UnitTaskController>().IsComplete == false)
                    {
                        if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.GiftBox && interactingUnit.gameObject.name == "Unit4")
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
                    if (!isInteracting && interactingUnit.gameObject.GetComponent<UnitTaskController>().IsComplete == false)
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
                    if (!isInteracting && interactingUnit.gameObject.GetComponent<UnitTaskController>().IsComplete == false)
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
                    if (!isInteracting && interactingUnit.gameObject.GetComponent<UnitTaskController>().IsComplete == false)
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
                    if (!isInteracting && interactingUnit.gameObject.GetComponent<UnitTaskController>().IsComplete == false)
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
                   
                    if (!isInteracting && interactingUnit.gameObject.GetComponent<UnitTaskController>().IsComplete == false)
                    {
                        if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.None && TutorialComplete)
                        {
                            OnInteraction(interactingUnit);
                        }
                        else if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.Console )
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
                    if (!isInteracting && interactingUnit.gameObject.GetComponent<UnitTaskController>().IsComplete == false)
                    {
                        if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.None && tm.tutorialProgress == 3)
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
                    if (!isInteracting && interactingUnit.gameObject.GetComponent<UnitTaskController>().IsComplete == false)
                    {
                        if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.Computer || this.TutorialComplete)
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
                    if (!isInteracting && interactingUnit.gameObject.GetComponent<UnitTaskController>().IsComplete == false)
                    {
                        if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.None && tm.tutorialProgress == 1)
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
                    if (!isInteracting && interactingUnit.gameObject.GetComponent<UnitTaskController>().IsComplete == false)
                    {
                        if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.None || this.TutorialComplete)
                        {
                            OnInteraction(interactingUnit);
                        }
                        //else
                        //{
                        //    if (FeedBackFiredAlready == false)
                        //    {
                        //        NegativeFeedback(other);
                        //    }
                        //}
                         else if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.Clothes)
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
                    if (!isInteracting && interactingUnit.gameObject.GetComponent<UnitTaskController>().IsComplete == false)
                    {
                        if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.None && tm.tutorialProgress == 2)
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

    private void ExitInteraction()
    {
        isInteracting = false;
        isComplete = false;
        interactingUnit.gameObject.GetComponent<UnitTaskController>().IsComplete = false;
        UnitToMoveTo = null;
        if (interactingUnit.GetComponentInChildren<PlayerUI>())
        {
            interactingUnit.GetComponentInChildren<PlayerUI>().TurnOffUI();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInteracting = false;
            isComplete = false;
            interactingUnit.gameObject.GetComponent<UnitTaskController>().IsComplete = false;
            UnitToMoveTo = null;
            if (other.GetComponentInChildren<PlayerUI>())
            {
                other.GetComponentInChildren<PlayerUI>().TurnOffUI();
            }

            //if(this.areaType == AreaType.TvBox && this.TutorialComplete || this.areaType == AreaType.ComputerBox && this.TutorialComplete || this.areaType == AreaType.ClothesBox && this.TutorialComplete)
            //{
            //    Destroy(this.gameObject);
            //}
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
        if (rtsMover.ActiveUnit != null)
        {
            UnitToMoveTo = rtsMover.ActiveUnit;
            if (MovePoint != null)
            {
                if (areaType == AreaType.Counter)
                {
                    if ((UnitToMoveTo.transform.position - transform.parent.parent.position).x >= 0)
                    {
                        rtsMover.MovePlayer(MovePoint2);
                        //MovePoint2 = null;
                    }
                    else
                    {
                        rtsMover.MovePlayer(MovePoint);
                        //MovePoint = null;
                    }
                }
                else
                {
                    rtsMover.MovePlayer(MovePoint);
                    //MovePoint = null;
                }
            }

            else
            {
                rtsMover.GroundMove();
            }


        }



    }

    //public IEnumerator FlashFeedback(Image Player, Sprite image)
    //{
    //    Player.gameObject.SetActive(true);
    //    Player.sprite = image;
    //    yield return new WaitForSeconds(1);

    //    Player.gameObject.SetActive(false);
    //    StopCoroutine("FlashFeedback");
    //}

    void TestDebug()
    {

    }

    private void OnMouseOver()
    {
        //hoverSpriteObject.SetActive(true);
        this.gameObject.GetComponent<Outline>().enabled = true;
        // this.gameObject.transform.GetChildCount.<Outline>().enabled = true;
        //if (areaType == AreaType.GiftBox)
        //{
        //    Debug.Log("Here");
        //    this.gameObject.GetComponentInChildren<Outline>().enabled = true;
        //}
        if (rtsMover.ActiveUnit == null)
        {
            this.GetComponent<Outline>().OutlineColor = Color.red;
        }
        else
        {


            switch (areaType)
            {
                case AreaType.None:

                    break;

                case AreaType.PreperationArea:
                    if (rtsMover.ActiveUnit.GetComponent<UnitTaskController>().currentTaskType == UnitTaskController.TaskType.None)
                    {
                        this.GetComponent<Outline>().OutlineColor = Color.green;
                        this.gameObject.GetComponent<Outline>().enabled = true;
                    }
                    else
                    {
                        this.GetComponent<Outline>().OutlineColor = Color.red;
                        this.gameObject.GetComponent<Outline>().enabled = false;
                    }
                    break;

                case AreaType.CookingArea:
                    if (rtsMover.ActiveUnit.GetComponent<UnitTaskController>().currentTaskType == UnitTaskController.TaskType.RawFood ||
                        rtsMover.ActiveUnit.GetComponent<UnitTaskController>().currentTaskType == UnitTaskController.TaskType.CleanPlate)
                    {
                        this.GetComponent<Outline>().OutlineColor = Color.green;
                        this.gameObject.GetComponent<Outline>().enabled = true;
                    }
                    else
                    {
                        this.GetComponent<Outline>().OutlineColor = Color.red;
                        this.gameObject.GetComponent<Outline>().enabled = false;
                    }
                    break;

                case AreaType.SinkArea:
                    if (rtsMover.ActiveUnit.GetComponent<UnitTaskController>().currentTaskType == UnitTaskController.TaskType.DirtyPlate )
                    {
                        this.GetComponent<Outline>().OutlineColor = Color.green;
                        this.gameObject.GetComponent<Outline>().enabled = true;
                    }
                    else
                    {
                        this.GetComponent<Outline>().OutlineColor = Color.red;
                        this.gameObject.GetComponent<Outline>().enabled = false;
                    }
                    break;

                case AreaType.ServingArea:
                    if (rtsMover.ActiveUnit.GetComponent<UnitTaskController>().currentTaskType == UnitTaskController.TaskType.CookedFood)
                    {
                        this.GetComponent<Outline>().OutlineColor = Color.green;
                        this.gameObject.GetComponent<Outline>().enabled = true;
                    }
                    else
                    {
                        this.GetComponent<Outline>().OutlineColor = Color.red;
                        this.gameObject.GetComponent<Outline>().enabled = false;
                    }
                    break;

                case AreaType.DirtyDishReturn:
                    if (rtsMover.ActiveUnit.GetComponent<UnitTaskController>().currentTaskType == UnitTaskController.TaskType.None)
                    {
                        this.GetComponent<Outline>().OutlineColor = Color.green;
                        this.gameObject.GetComponent<Outline>().enabled = true;
                    }
                    else
                    {
                        this.GetComponent<Outline>().OutlineColor = Color.red;
                        this.gameObject.GetComponent<Outline>().enabled = false;
                    }
                    break;

                case AreaType.TrashCan:
                    if (rtsMover.ActiveUnit.GetComponent<UnitTaskController>().currentTaskType != UnitTaskController.TaskType.None)
                    {
                        this.GetComponent<Outline>().OutlineColor = Color.green;
                        this.gameObject.GetComponent<Outline>().enabled = true;
                    }
                    else
                    {
                        this.GetComponent<Outline>().OutlineColor = Color.red;
                        this.gameObject.GetComponent<Outline>().enabled = false;
                    }
                    break;

                case AreaType.Counter:
                    if (rtsMover.ActiveUnit.GetComponent<UnitTaskController>().currentTaskType != UnitTaskController.TaskType.None||
                        rtsMover.ActiveUnit.GetComponent<UnitTaskController>().currentTaskType == UnitTaskController.TaskType.None)
                    {
                        this.GetComponent<Outline>().OutlineColor = Color.green;
                        this.gameObject.GetComponent<Outline>().enabled = true;
                    }
                    else
                    {
                        this.GetComponent<Outline>().OutlineColor = Color.red;
                        this.gameObject.GetComponent<Outline>().enabled = false;
                    }
                    break;

                case AreaType.GiftBox:
                    if (rtsMover.ActiveUnit.GetComponent<UnitTaskController>().currentTaskType != UnitTaskController.TaskType.None ||
                        rtsMover.ActiveUnit.GetComponent<UnitTaskController>().currentTaskType == UnitTaskController.TaskType.None && rtsMover.ActiveUnit.gameObject.name == "Unit4")
                    {
                        this.GetComponent<Outline>().OutlineColor = Color.green;
                        this.gameObject.GetComponent<Outline>().enabled = true;
                    }
                    else
                    {
                        this.GetComponent<Outline>().OutlineColor = Color.red;
                        this.gameObject.GetComponent<Outline>().enabled = false;
                    }
                    break;

                case AreaType.Truck:
                        if (rtsMover.ActiveUnit.GetComponent<UnitTaskController>().currentTaskType == UnitTaskController.TaskType.GiftBox )
                        {
                            this.GetComponent<Outline>().OutlineColor = Color.green;
                        this.gameObject.GetComponent<Outline>().enabled = true;
                    }
                        else
                        {
                            this.GetComponent<Outline>().OutlineColor = Color.red;
                        this.gameObject.GetComponent<Outline>().enabled = false;
                    }
                    break;

                case AreaType.DollBin:
                        if (rtsMover.ActiveUnit.GetComponent<UnitTaskController>().currentTaskType == UnitTaskController.TaskType.None)
                        {
                            this.GetComponent<Outline>().OutlineColor = Color.green;
                        this.gameObject.GetComponent<Outline>().enabled = true;
                    }
                        else
                        {
                            this.GetComponent<Outline>().OutlineColor = Color.red;
                        this.gameObject.GetComponent<Outline>().enabled = false;
                    }
                    
                    break;

                case AreaType.BallBin:
                    if (rtsMover.ActiveUnit.GetComponent<UnitTaskController>().currentTaskType == UnitTaskController.TaskType.None)
                    {
                        this.GetComponent<Outline>().OutlineColor = Color.green;
                        this.gameObject.GetComponent<Outline>().enabled = true;
                    }
                    else
                    {
                        this.GetComponent<Outline>().OutlineColor = Color.red;
                        this.gameObject.GetComponent<Outline>().enabled = false;
                    }
                    break;

                case AreaType.RobotBin:
                    if (rtsMover.ActiveUnit.GetComponent<UnitTaskController>().currentTaskType == UnitTaskController.TaskType.None)
                    {
                        this.GetComponent<Outline>().OutlineColor = Color.green;
                        this.gameObject.GetComponent<Outline>().enabled = true;
                    }
                    else
                    {
                        this.GetComponent<Outline>().OutlineColor = Color.red;
                        this.gameObject.GetComponent<Outline>().enabled = false;
                    }
                    break;

                case AreaType.BaseballBatBin:
                    if (rtsMover.ActiveUnit.GetComponent<UnitTaskController>().currentTaskType == UnitTaskController.TaskType.None)
                    {
                        this.GetComponent<Outline>().OutlineColor = Color.green;
                        this.gameObject.GetComponent<Outline>().enabled = true;
                    }
                    else
                    {
                        this.GetComponent<Outline>().OutlineColor = Color.red;
                        this.gameObject.GetComponent<Outline>().enabled = false;
                    }
                    break;

                case AreaType.TvStand:

                    if (rtsMover.ActiveUnit.GetComponent<UnitTaskController>().currentTaskType == UnitTaskController.TaskType.Console && tm.tutorialProgress >= 3
                         || rtsMover.ActiveUnit.GetComponent<UnitTaskController>().currentTaskType == UnitTaskController.TaskType.None && tm.tutorialProgress == 4)
                    {
                        this.GetComponent<Outline>().OutlineColor = Color.green;
                        this.gameObject.GetComponent<Outline>().enabled = true;
                    }
                    else
                    {
                        this.GetComponent<Outline>().OutlineColor = Color.red;
                        this.gameObject.GetComponent<Outline>().enabled = false;
                    }
                    break;

                case AreaType.TvBox:
                    if (rtsMover.ActiveUnit.GetComponent<UnitTaskController>().currentTaskType == UnitTaskController.TaskType.None && tm.tutorialProgress == 3)
                    {
                        this.GetComponent<Outline>().OutlineColor = Color.green;
                        this.gameObject.GetComponent<Outline>().enabled = true;
                    }
                    else
                    {
                        this.GetComponent<Outline>().OutlineColor = Color.red;
                        this.gameObject.GetComponent<Outline>().enabled = false;
                    }
                    break;

                case AreaType.ComputerDesk:
                    if (rtsMover.ActiveUnit.GetComponent<UnitTaskController>().currentTaskType == UnitTaskController.TaskType.Computer && tm.tutorialProgress >= 1
                        || rtsMover.ActiveUnit.GetComponent<UnitTaskController>().currentTaskType == UnitTaskController.TaskType.None && tm.tutorialProgress == 4 )
                    {
                        this.GetComponent<Outline>().OutlineColor = Color.green;
                        this.gameObject.GetComponent<Outline>().enabled = true;
                    }
                    else
                    {
                        this.GetComponent<Outline>().OutlineColor = Color.red;
                        this.gameObject.GetComponent<Outline>().enabled = false;
                    }
                    break;

                case AreaType.ComputerBox:
                    if (rtsMover.ActiveUnit.GetComponent<UnitTaskController>().currentTaskType == UnitTaskController.TaskType.None && tm.tutorialProgress == 1)
                    {
                        this.GetComponent<Outline>().OutlineColor = Color.green;
                        this.gameObject.GetComponent<Outline>().enabled = true;
                    }
                    else
                    {
                        this.GetComponent<Outline>().OutlineColor = Color.red;
                        this.gameObject.GetComponent<Outline>().enabled = false;
                    }
                    break;

                case AreaType.Closet:
                    if (rtsMover.ActiveUnit.GetComponent<UnitTaskController>().currentTaskType == UnitTaskController.TaskType.Clothes && tm.tutorialProgress >= 2
                         || rtsMover.ActiveUnit.GetComponent<UnitTaskController>().currentTaskType == UnitTaskController.TaskType.None && tm.tutorialProgress == 4)
                    {
                        this.GetComponent<Outline>().OutlineColor = Color.green;
                        this.gameObject.GetComponent<Outline>().enabled = true;

                    }
                    else
                    {
                        this.GetComponent<Outline>().OutlineColor = Color.red;
                        this.gameObject.GetComponent<Outline>().enabled = false;
                    }
                    break;

                case AreaType.ClothesBox:
                    if (rtsMover.ActiveUnit.GetComponent<UnitTaskController>().currentTaskType == UnitTaskController.TaskType.None && tm.tutorialProgress == 2)
                    {
                        this.GetComponent<Outline>().OutlineColor = Color.green;
                        this.gameObject.GetComponent<Outline>().enabled = true;
                    }
                    else
                    {
                        this.GetComponent<Outline>().OutlineColor = Color.red;
                        this.gameObject.GetComponent<Outline>().enabled = false;
                    }
                    break;

            }
        }

    }

    private void OnMouseExit()
    {
       // hoverSpriteObject.SetActive(false);
        this.gameObject.GetComponent<Outline>().enabled = false;
    //    if (this.areaType == AreaType.GiftBox)
    //    {

    //    }
    }
}
