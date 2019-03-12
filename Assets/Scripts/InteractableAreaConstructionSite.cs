using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableAreaConstructionSite : MonoBehaviour
{

    public float timer = 0;
    public float startTime = 4;
    public bool isInteracting;
    public bool isComplete;
    public AreaType areaType;

    public GameObject interactingUnit;
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

    public GameObject woodRecipticalBin;
    public GameObject pipeRecipticalBin;
    public bool BenchHasWood = false;
    public bool BenchHasPipe = false;

    //use this for forman recipe
    public int numberOfBoards;
    public int numberOfPipes;
    public int numberOfNails;
    public int numberOfConnectors;
    public int numberOfComboWood;
    public int numberOfComboPipe;

    //For heavy objects
    public List<GameObject> Heavycarriers;
    public List<GameObject> WoodHoldPositions;
    public GameObject carryWood;
    public List<GameObject> PipeHoldPositions;
    public GameObject CarryPipe;

    public Sprite hoverSprite;
    public GameObject hoverSpriteObject;

    [SerializeField]
    Transform MovePoint;






    public GameManager Gm;
    public enum AreaType
    {
        None,
        TrashCan,
        CuttingArea,
        WoodPile,
        WoodRecipticalBin,
        PipeRecipticalBin,
        Counter,
        PipePile,
        NailsBin,
        PipeConnector,
        CraftingStation,
        FormanReturn
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
        hoverSpriteObject.GetComponent<Image>().sprite = hoverSprite;
        hoverSpriteObject.SetActive(false);
        InvokeRepeating("TestDebug", 0.0f, 1.0f);
        feedbackSlider = GameObject.FindGameObjectWithTag("PlayerCanvas1").transform.GetChild(0).GetComponent<Slider>();
        feedbackSlider2 = GameObject.FindGameObjectWithTag("Player Canvas2").transform.GetChild(0).GetComponent<Slider>();
        feedbackSlider3 = GameObject.FindGameObjectWithTag("Player Canvas3").transform.GetChild(0).GetComponent<Slider>();
        //  feedbackSlider4 = GameObject.FindGameObjectWithTag("PlayerCanvas 4").transform.GetChild(0).GetComponent<Slider>();

        Status = GameObject.FindGameObjectWithTag("PlayerCanvas1").transform.GetChild(1).GetComponent<Image>();
        Status2 = GameObject.FindGameObjectWithTag("Player Canvas2").transform.GetChild(1).GetComponent<Image>();
        Status3 = GameObject.FindGameObjectWithTag("Player Canvas3").transform.GetChild(1).GetComponent<Image>();
        //  Status4 = GameObject.FindGameObjectWithTag("PlayerCanvas2").transform.GetChild(1).GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        //    Debug.Log(isOnCounter + " " + objectPlayerHolding);

        if (cleanPlateOn)
            counterSpace.ObjectCleanPlate();

        if (dirtyPlateOn || cleanPlateOn || filledPlateOn || rawFoodOn)
            isOnCounter = true;
        else
            isOnCounter = false;
    }

    void TestDebug()
    {

    }

    //private void OnMouseDown()
    //{
    //    UnitToMoveTo = rtsMover.ActiveUnit;
    //    if(UnitToMoveTo == carryWood)
    //    {
    //        if( rtsMover.ActiveUnit.GetComponent<VisibilityManager>().Heavycarriers[0].gameObject!= null)
    //        {
    //            UnitToMoveTo = rtsMover.ActiveUnit.GetComponent<VisibilityManager>().Heavycarriers[0].gameObject;
    //        }

            
           
    //    }
    //    else if(UnitToMoveTo == CarryPipe)
    //    {
    //        if(rtsMover.ActiveUnit.GetComponent<VisibilityManager>().Heavycarriers[0].gameObject != null)
    //        {
    //            UnitToMoveTo = rtsMover.ActiveUnit.GetComponent<VisibilityManager>().Heavycarriers[0].gameObject;
    //        }
           
    //    }
    //    //else
    //    //{
    //    //    if (MovePoint != null)
    //    //    {
    //    //        rtsMover.MovePlayer(MovePoint);
    //    //    }
           
    //    //}
    //}

    private void OnMouseOver()
    {
      //  Debug.Log("HOVERED OVER " + gameObject.name);
        hoverSpriteObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        hoverSpriteObject.SetActive(false);
    }

    void Complete(AreaType type, PlayerUI UI)
    {

        isInteracting = false;
        isComplete = true;
        interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = false;
        interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = false;
        StartCoroutine(UI.FlashFeedback(true));

        //check type of Area
        switch (type)
        {
            case AreaType.None:
                break;

            case AreaType.TrashCan:
                interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.None;
                break;
            case AreaType.CuttingArea:
                if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.LargeWood)
                {
                    interactingUnit.GetComponent<UnitTaskController>().currentTaskType = UnitTaskController.TaskType.None;
                    StartCoroutine(interactingUnit.GetComponent<UnitTaskController>().GetComponentInChildren<PlayerUI>().FlashFeedback(true));
                    BenchHasWood = true;
                }
                else if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.LargePipe)
                {
                    interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.None;
                    StartCoroutine(interactingUnit.GetComponent<UnitTaskController>().GetComponentInChildren<PlayerUI>().FlashFeedback(true));
                    BenchHasPipe = true;
                }
                else if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.None && BenchHasWood)
                {
                    if (woodRecipticalBin.GetComponent<InteractableAreaConstructionSite>().numberOfBoards < 4)
                    {
                        woodRecipticalBin.GetComponent<InteractableAreaConstructionSite>().numberOfBoards += 2;
                        BenchHasWood = false;
                    }
                }
                else if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.None && BenchHasPipe)
                {
                    if (pipeRecipticalBin.GetComponent<InteractableAreaConstructionSite>().numberOfPipes < 4)
                    {
                        pipeRecipticalBin.GetComponent<InteractableAreaConstructionSite>().numberOfPipes += 2;
                        BenchHasPipe = false;
                    }
                }

                break;
            case AreaType.WoodRecipticalBin:
                interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.SmallWood;
                this.numberOfBoards -= 1;
                break;
            case AreaType.PipeRecipticalBin:
                interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.SmallPipe;
                this.numberOfPipes -= 1;
                break;
            case AreaType.PipeConnector:
                interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.PipeConnector;
                break;
            case AreaType.WoodPile:

                interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.LargePipe;

                break;



            case AreaType.PipePile:

                interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.LargePipe;
                break;


                




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
            case AreaType.CraftingStation:
                if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.SmallWood)
                {
                    numberOfBoards += 1;
                    interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.None;
                }
                else if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.SmallPipe)
                {
                    numberOfPipes += 1;
                    interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.None;
                }
                else if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.Nails)
                {
                    numberOfNails += 1;
                    interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.None;
                }
                else if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.PipeConnector)
                {
                    numberOfConnectors += 1;
                    interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.None;
                }


                break;
            case AreaType.NailsBin:
                interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.Nails;
                break;

            case AreaType.FormanReturn:

                if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.SmallWood)
                {
                    numberOfBoards += 1;
                }
                else if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.SmallPipe)
                {
                    numberOfPipes += 1;
                }
                else if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.Nails)
                {
                    numberOfNails += 1;
                }
                else if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.PipeConnector)
                {
                    numberOfConnectors += 1;
                }
                else if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.ComboPipe)
                {
                    numberOfComboPipe += 1;
                }
                else if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.ComboWood)
                {
                    numberOfComboWood += 1;
                }

                Foreman foreman = FindObjectOfType<Foreman>();
                objectPlayerHolding = interactingUnit.gameObject.GetComponent<UnitTaskController>().objectHolding;
                foreach (Foreman.itemInfo item in foreman.finalItemList)
                {
                    if (item.tool.ToString() == objectPlayerHolding.ToString())
                    {
                        switch (objectPlayerHolding)
                        {
                            case UnitTaskController.ObjectHeld.ComboPipe:
                                foreman.ReceiveItem(Foreman.ToolList.ComboPipe);
                                break;
                            case UnitTaskController.ObjectHeld.ComboWood:
                                foreman.ReceiveItem(Foreman.ToolList.ComboWood);
                                break;
                            case UnitTaskController.ObjectHeld.SmallWood:
                                foreman.ReceiveItem(Foreman.ToolList.SmallWood);
                                break;
                            case UnitTaskController.ObjectHeld.SmallPipe:
                                foreman.ReceiveItem(Foreman.ToolList.SmallPipe);
                                break;
                            case UnitTaskController.ObjectHeld.Nails:
                                foreman.ReceiveItem(Foreman.ToolList.Nails);
                                break;
                            case UnitTaskController.ObjectHeld.PipeConnector:
                                foreman.ReceiveItem(Foreman.ToolList.PipeConnector);
                                break;
                        }
                    }
                }
                objectPlayerHolding = UnitTaskController.ObjectHeld.None;
                interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.None;
                break;
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


                case AreaType.TrashCan:
                    if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType != UnitTaskController.TaskType.None)
                    {
                        isInteracting = true;
                        interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = true;
                        interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = true;
                        interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.None;
                    }
                    else
                    {
                        if (FeedBackFiredAlready == false)
                        {
                            NegativeFeedback(other);
                        }
                    }
                    break;
                case AreaType.Counter:
                    objectPlayerHolding = interactingUnit.gameObject.GetComponent<UnitTaskController>().objectHolding;
                    //Nothing on counter
                    ///
                    if (isOnCounter || objectPlayerHolding != UnitTaskController.ObjectHeld.None)
                    {
                        isInteracting = true;
                        interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = true;
                        interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = true;
                    }
                    break;
                case AreaType.CuttingArea:
                    if (!isInteracting && !isComplete)
                    {
                        if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.LargeWood && !BenchHasWood)
                        {
                            isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = true;
                        }
                        else if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.LargePipe && !BenchHasPipe)
                        {
                            isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = true;
                        }
                        else if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.None && BenchHasWood)
                        {
                            isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = true;
                        }
                        else if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.None && BenchHasPipe)
                        {
                            isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = true;
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
                case AreaType.WoodRecipticalBin:
                    if (!isInteracting && !isComplete)
                    {
                        if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.None && this.numberOfBoards > 0)
                        {
                            isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = true;
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
                case AreaType.PipeRecipticalBin:
                    if (!isInteracting && !isComplete)
                    {
                        if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.None && this.numberOfPipes > 0)
                        {
                            isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = true;
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
                case AreaType.WoodPile:
                 if(!isInteracting && !isComplete)
                    {
                        isInteracting = true;
                        interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = true;
                        interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = true;
                    }

                    else
                    {
                        if (FeedBackFiredAlready == false)
                        {
                            NegativeFeedback(other);
                        }
                    }

                    break;


                case AreaType.PipePile:
                    if (!isInteracting && !isComplete)
                    {
                        isInteracting = true;
                        interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = true;
                        interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = true;
                    }

                    else
                    {
                        if (FeedBackFiredAlready == false)
                        {
                            NegativeFeedback(other);
                        }
                    }

                    break;

                 
                case AreaType.CraftingStation:
                    if (!isInteracting && !isComplete)
                    {
                        if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.SmallWood && numberOfPipes == 0)
                        {
                            isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = true;
                        }
                        else if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.SmallPipe && numberOfBoards == 0)
                        {
                            isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = true;
                        }
                        else if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.Nails && numberOfPipes == 0)
                        {
                            isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = true;
                        }
                        else if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.PipeConnector && numberOfBoards == 0)
                        {
                            isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = true;
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
                case AreaType.PipeConnector:
                    if (!isInteracting && !isComplete)
                    {
                        if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.None)
                        {
                            isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = true;
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
                case AreaType.NailsBin:
                    if (!isInteracting && !isComplete)
                    {
                        if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.None)
                        {
                            isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = true;
                            interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = true;
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
                case AreaType.FormanReturn:
                    if (!isInteracting && !isComplete)
                    {
                        //if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.SmallPipe 
                        //    || interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.SmallWood
                        //    || interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.PipeConnector
                        //    || interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.Nails
                        //    || interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.ComboWood
                        //    || interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.ComboPipe)
                        objectPlayerHolding = interactingUnit.gameObject.GetComponent<UnitTaskController>().objectHolding;
                        Foreman foreman = FindObjectOfType<Foreman>();
                        objectPlayerHolding = interactingUnit.gameObject.GetComponent<UnitTaskController>().objectHolding;
                        foreach (Foreman.itemInfo item in foreman.finalItemList)
                        {
                            if (item.tool.ToString() == objectPlayerHolding.ToString())
                            {
                                isInteracting = true;
                                interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = true;
                                interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = true;
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







                /*
                            if (other.gameObject.name == "Unit1")
                            {
                                if (isInteracting && !isComplete)
                                {
                                    feedbackSlider.gameObject.SetActive(true);
                                    feedbackSlider.maxValue = startTime;
                                    //setImage
                                    Status.gameObject.SetActive(true);
                                    SwitchImage(FeedbackSprites[0], Status);

                                    feedbackSlider.value = timer;
                                    timer += Time.deltaTime;
                                    if (timer >= startTime)
                                    {
                                        Complete(areaType, Status);
                                    }
                                }
                                else
                                {
                                    feedbackSlider.gameObject.SetActive(false);
                                    timer = 0.0f;
                                }
                            }
                            else if (other.gameObject.name == "Unit2")
                            {
                                if (isInteracting && !isComplete)
                                {
                                    feedbackSlider2.gameObject.SetActive(true);
                                    feedbackSlider2.maxValue = startTime;
                                    Status2.gameObject.SetActive(true);
                                    SwitchImage(FeedbackSprites[0], Status2);

                                    feedbackSlider2.value = timer;
                                    timer += Time.deltaTime;
                                    if (timer >= startTime)
                                    {
                                        Complete(areaType, Status2);
                                    }


                                }
                                else
                                {
                                    feedbackSlider2.gameObject.SetActive(false);
                                    timer = 0.0f;
                                }
                            }
                            else if(other.gameObject.name == "Unit3")
                            {
                                if (isInteracting && !isComplete)
                                {
                                    feedbackSlider3.gameObject.SetActive(true);
                                    feedbackSlider3.maxValue = startTime;
                                    Status3.gameObject.SetActive(true);
                                    SwitchImage(FeedbackSprites[0], Status3);

                                    feedbackSlider3.value = timer;
                                    timer += Time.deltaTime;
                                    if (timer >= startTime)
                                    {
                                        Complete(areaType, Status3);
                                    }
                                }
                                else
                                {
                                    feedbackSlider3.gameObject.SetActive(false);
                                    timer = 0.0f;
                                }
                                */





            }
        }
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

            /* FeedBackFiredAlready = false;
             Status.gameObject.SetActive(false);
             Status2.gameObject.SetActive(false);
             feedbackSlider.gameObject.SetActive(false);
             feedbackSlider2.gameObject.SetActive(false);*/
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")// && this.gameObject.GetComponent<InteractableAreaConstructionSite>().UnitToMoveTo == other.gameObject)
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
        if (UnitToMoveTo == carryWood)
        {
            if (rtsMover.ActiveUnit.GetComponent<VisibilityManager>().Heavycarriers[0].gameObject != null)
            {
                UnitToMoveTo = rtsMover.ActiveUnit.GetComponent<VisibilityManager>().Heavycarriers[0].gameObject;
            }



        }
        else if (UnitToMoveTo == CarryPipe)
        {
            if (rtsMover.ActiveUnit.GetComponent<VisibilityManager>().Heavycarriers[0].gameObject != null)
            {
                UnitToMoveTo = rtsMover.ActiveUnit.GetComponent<VisibilityManager>().Heavycarriers[0].gameObject;
            }

        }
        else
        {
            if (MovePoint != null)
            {
                rtsMover.MovePlayer(MovePoint);
            }

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


}
