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

    private GameObject interactingUnit;
 

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

    public int numberOfBoards;
    public int numberOfPipes;
    //For heavy objects
    public List<GameObject> Heavycarriers;
    public List<GameObject> WoodHoldPositions;
    public GameObject carryWood;
    public List<GameObject> PipeHoldPositions;
    public GameObject CarryPipe;
    
   






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
        PipePile
    }

    //Put things on the counters
    private bool isOnCounter = false;
    private CounterSpace counterSpace;
    public bool dirtyPlateOn, cleanPlateOn, filledPlateOn, rawFoodOn;
    UnitTaskController.ObjectHeld objectPlayerHolding;

    // Use this for initialization
    void Start()
    {
        Gm = GameObject.FindObjectOfType<GameManager>();
        counterSpace = gameObject.GetComponent<CounterSpace>();
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

    void Complete(AreaType type, Image Player)
    {
        
        isInteracting = false;
        isComplete = true;
        interactingUnit.gameObject.GetComponent<UnitHighlight>().isInteracting = false;
        interactingUnit.gameObject.GetComponent<UnitTaskController>().isInteracting = false;
        StartCoroutine(FlashFeedback(Player, FeedbackSprites[2]));

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
                    //if(interactingUnit.transform.parent!= null)
                    //{
                    //    interactingUnit.transform.parent = null;
                    //}

                    //else
                    //{
                    //   // interactingUnit.GetComponent<UnitTaskController>().companion.transform.parent = null;
                    //}

                   
                    interactingUnit.GetComponent<UnitTaskController>().currentTaskType = UnitTaskController.TaskType.None;
                    interactingUnit.GetComponent<UnitTaskController>().companion.GetComponent<UnitTaskController>().currentTaskType = UnitTaskController.TaskType.None;
                    carryWood.SetActive(false);

                    //CHECK IF REERENCES ARE CORRECT
                    //Debug.Log("The interacting unit is" + interactingUnit.name);
                  //  Debug.Log("The interacting unit is" + interactingUnit.GetComponent<UnitTaskController>().companion.name);


                    BenchHasWood = true;
                }
                else if (interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType == UnitTaskController.TaskType.LargePipe)
                {
                    interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.None;
                    interactingUnit.GetComponent<UnitTaskController>().companion.GetComponent<UnitTaskController>().currentTaskType = UnitTaskController.TaskType.None;
                    CarryPipe.SetActive(false);

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
                else
                {

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
            case AreaType.WoodPile:
                for (int i = 0; i < Heavycarriers.Count; i++)
                {
                    //Set up the players to carry the wood
                    carryWood.SetActive(true);
                    carryWood.transform.position = transform.position;

                    if (i == 0)
                    {
                        //Heavycarriers[0].gameObject.GetComponent<UnitTaskController>().BigwoodOBJ.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, transform.rotation.eulerAngles.y - 90, 0));
                        Heavycarriers[0].gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.LargeWood;
           
                        Heavycarriers[0].gameObject.GetComponent<UnitTaskController>().companion = Heavycarriers[1].gameObject;
                        //Setting hold position
                        Heavycarriers[0].gameObject.GetComponent<UnitTaskController>().HeavyHoldPosition = WoodHoldPositions[0];

                    }
                    else
                    {
                        Heavycarriers[1].gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.LargeWood;
                        //Heavycarriers[1].gameObject.transform.parent = Heavycarriers[0].gameObject.transform;
                        Heavycarriers[1].gameObject.GetComponent<UnitTaskController>().companion = Heavycarriers[0].gameObject;
                        //Setting hold position
                        Heavycarriers[1].gameObject.GetComponent<UnitTaskController>().HeavyHoldPosition = WoodHoldPositions[1];
                    }
                }
                break;



            case AreaType.PipePile:
                for(int n=0; n < Heavycarriers.Count; n++)
                {
                    CarryPipe.SetActive(true);
                    CarryPipe.transform.position = transform.position;

                    if (n == 0)
                    {
                        Heavycarriers[0].gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.LargePipe;

                        Heavycarriers[0].gameObject.GetComponent<UnitTaskController>().companion = Heavycarriers[1].gameObject;
                        //Setting hold position
                        Heavycarriers[0].gameObject.GetComponent<UnitTaskController>().HeavyHoldPosition = PipeHoldPositions[0];

                    }

                    else
                    {
                        Heavycarriers[1].gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.LargePipe;
                        //Heavycarriers[1].gameObject.transform.parent = Heavycarriers[0].gameObject.transform;
                        Heavycarriers[1].gameObject.GetComponent<UnitTaskController>().companion = Heavycarriers[0].gameObject;
                        //Setting hold position
                        Heavycarriers[1].gameObject.GetComponent<UnitTaskController>().HeavyHoldPosition = PipeHoldPositions[1];
                    }
                }

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

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
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
                    if (!isInteracting && !isComplete )
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
                    if (Heavycarriers.Count == 2)
                    {
                        Debug.Log("pickup Log");
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
                    if(Heavycarriers.Count == 2)
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

            }
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
            }
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

        if(areaType == AreaType.WoodPile || areaType == AreaType.PipePile)
        {
            if (Heavycarriers.Contains(other.gameObject))
            {
                Heavycarriers.Remove(other.gameObject);
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
                case AreaType.WoodPile:

                    if (interactingUnit.gameObject.GetComponent<UnitTaskController>().currentTaskType== UnitTaskController.TaskType.None)
                    {
                        Heavycarriers.Add(other.gameObject);
                    }
                    break;

                case AreaType.PipePile:
                    if(interactingUnit.gameObject.GetComponent<UnitTaskController>().currentTaskType== UnitTaskController.TaskType.None)
                    {
                        Heavycarriers.Add(other.gameObject);
                    }

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
        FeedBackFiredAlready = true;
        if (target.gameObject.name == "Unit1")
        {
            StartCoroutine(FlashFeedback(Status, FeedbackSprites[1]));
        }
        else if (target.gameObject.name == "Unit2")
        {
            StartCoroutine(FlashFeedback(Status2, FeedbackSprites[1]));
        }
        else if (target.gameObject.name == "Unit3")
        {
            StartCoroutine(FlashFeedback(Status3, FeedbackSprites[1]));
        }
    }
}
