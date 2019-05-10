using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTaskController : MonoBehaviour
{

    public bool isInteracting = false;
    public bool IsComplete = false;
    public TaskType currentTaskType;

    public GameObject exclamationPoint;

    //for cooks in kitchen
    public GameObject dirtyPlate;
    public GameObject cleanPlate;
    public GameObject filledPlate;
    public GameObject rawFood;
    public Canvas convoCanvas;
    [HideInInspector]
    public GameObject companion;
    //For heavy object
    //for construction 
    public GameObject BigwoodOBJ;
    public GameObject BigPipe;
    public GameObject smallWood;
    public GameObject smallPipe;
    public GameObject nails;
    public GameObject connector;
    public GameObject comboWood;
    public GameObject comboPipe;

    // for toys for tots
    public GameObject gift;
    public GameObject robot;
    public GameObject ball;
    public GameObject bat;
    public GameObject doll;

    // for dorm room tutorial
    public GameObject cardboardBox;

    public TaskType CurrentTaskType
    {
        get
        {
            return currentTaskType;
        }

        set
        {
            currentTaskType = value;
            //StartCoroutine("FlashFeedback");
        }
    }

    public enum TaskType
    {
        None,
        CleanPlate,
        DirtyPlate,
        Interacting,
        Washing,
        CookedFood,
        RawFood,
        Serving,
        Counter,
        LargeWood,
        LargePipe,
        SmallWood,
        SmallPipe,
        PipeConnector,
        ComboWood,
        ComboPipe,
        Nails,
        GiftBox,
        Doll,
        Ball,
        Robot,
        BaseballBat,
        Computer,
        Console,
        Clothes,
        Box

    }

    // Put things on the table
    public enum ObjectHeld
    {
        None,
        DirtyPlate,
        CleanPlate,
        FilledPlate,
        RawFood,
        SmallWood,
        SmallPipe,
        ComboWood,
        ComboPipe,
        Nails,
        PipeConnector,
        LargePipe,
        LargeWood,
        GiftBox,
        Doll,
        Ball,
        Robot,
        BaseballBat,
        Computer,
        Console,
        Clothes,
        Box
    }
    public ObjectHeld objectHolding;

    // Use this for initialization
    void Start()
    {
        objectHolding = ObjectHeld.None;
        currentTaskType = TaskType.None;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInteracting)
        {
            DoTask();
        }
        //  Debug.Log(objectHolding);
    }


    public IEnumerator FlashFeedback()
    {
        exclamationPoint.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        exclamationPoint.SetActive(false);
        StopCoroutine("FlashFeedback");
    }


    void DoTask()
    {
        switch (currentTaskType)
        {
            case TaskType.None:
                cleanPlate.SetActive(false);
                dirtyPlate.SetActive(false);
                filledPlate.SetActive(false);
                rawFood.SetActive(false);
                comboWood.SetActive(false);
                comboPipe.SetActive(false);
                smallWood.SetActive(false);
                smallPipe.SetActive(false);
                BigwoodOBJ.SetActive(false);
                BigPipe.SetActive(false);
                nails.SetActive(false);
                connector.SetActive(false);
                cardboardBox.SetActive(false);
                gift.SetActive(false);
                doll.SetActive(false);
                robot.SetActive(false);
                ball.SetActive(false);
                bat.SetActive(false);
                objectHolding = ObjectHeld.None;
                isInteracting = false;
                break;
            case TaskType.CleanPlate:
                cleanPlate.SetActive(true);
                dirtyPlate.SetActive(false);
                filledPlate.SetActive(false);
                rawFood.SetActive(false);
                objectHolding = ObjectHeld.CleanPlate;
                break;
            case TaskType.DirtyPlate:
                cleanPlate.SetActive(false);
                dirtyPlate.SetActive(true);
                filledPlate.SetActive(false);
                rawFood.SetActive(false);
                objectHolding = ObjectHeld.DirtyPlate;
                break;
            case TaskType.Interacting:
                cleanPlate.SetActive(false);
                dirtyPlate.SetActive(false);
                filledPlate.SetActive(false);
                rawFood.SetActive(false);
                comboWood.SetActive(false);
                comboPipe.SetActive(false);
                smallWood.SetActive(false);
                smallPipe.SetActive(false);
                BigwoodOBJ.SetActive(false);
                BigPipe.SetActive(false);
                nails.SetActive(false);
                connector.SetActive(false);
                cardboardBox.SetActive(false);
                isInteracting = true;
                objectHolding = ObjectHeld.None;
                break;
            case TaskType.CookedFood:
                cleanPlate.SetActive(false);
                dirtyPlate.SetActive(false);
                filledPlate.SetActive(true);
                rawFood.SetActive(false);
                objectHolding = ObjectHeld.FilledPlate;
                break;
            case TaskType.Washing:
                cleanPlate.SetActive(false);
                dirtyPlate.SetActive(false);
                filledPlate.SetActive(false);
                rawFood.SetActive(false);
                objectHolding = ObjectHeld.None;
                break;
            case TaskType.RawFood:
                cleanPlate.SetActive(false);
                dirtyPlate.SetActive(false);
                filledPlate.SetActive(false);
                rawFood.SetActive(true);
                objectHolding = ObjectHeld.RawFood;
                break;
            case TaskType.Serving:
                cleanPlate.SetActive(false);
                dirtyPlate.SetActive(false);
                filledPlate.SetActive(false);
                rawFood.SetActive(false);
                objectHolding = ObjectHeld.None;
                break;
            case TaskType.ComboWood:
                comboWood.SetActive(true);
                comboPipe.SetActive(false);
                smallWood.SetActive(false);
                smallPipe.SetActive(false);
                BigwoodOBJ.SetActive(false);
                BigPipe.SetActive(false);
                nails.SetActive(false);
                connector.SetActive(false);
                objectHolding = ObjectHeld.ComboWood;
                break;
            case TaskType.ComboPipe:
                comboWood.SetActive(false);
                comboPipe.SetActive(true);
                smallWood.SetActive(false);
                smallPipe.SetActive(false);
                BigwoodOBJ.SetActive(false);
                BigPipe.SetActive(false);
                nails.SetActive(false);
                connector.SetActive(false);
                objectHolding = ObjectHeld.ComboPipe;
                break;
            case TaskType.SmallWood:
                comboWood.SetActive(false);
                comboPipe.SetActive(false);
                smallWood.SetActive(true);
                smallPipe.SetActive(false);
                BigwoodOBJ.SetActive(false);
                BigPipe.SetActive(false);
                nails.SetActive(false);
                connector.SetActive(false);
                objectHolding = ObjectHeld.SmallWood;
                break;
            case TaskType.SmallPipe:
                comboWood.SetActive(false);
                comboPipe.SetActive(false);
                smallWood.SetActive(false);
                smallPipe.SetActive(true);
                BigwoodOBJ.SetActive(false);
                BigPipe.SetActive(false);
                nails.SetActive(false);
                connector.SetActive(false);
                objectHolding = ObjectHeld.SmallPipe;
                break;
            case TaskType.Nails:
                comboWood.SetActive(false);
                comboPipe.SetActive(false);
                smallWood.SetActive(false);
                smallPipe.SetActive(false);
                BigwoodOBJ.SetActive(false);
                BigPipe.SetActive(false);
                nails.SetActive(true);
                connector.SetActive(false);
                objectHolding = ObjectHeld.Nails;
                break;
            case TaskType.Counter:
                cleanPlate.SetActive(false);
                dirtyPlate.SetActive(false);
                filledPlate.SetActive(false);
                rawFood.SetActive(false);
                comboWood.SetActive(false);
                comboPipe.SetActive(false);
                smallWood.SetActive(false);
                smallPipe.SetActive(false);
                BigwoodOBJ.SetActive(false);
                BigPipe.SetActive(false);
                nails.SetActive(false);
                connector.SetActive(false);
                objectHolding = ObjectHeld.None;
                break;
            case TaskType.LargeWood:
                comboWood.SetActive(false);
                comboPipe.SetActive(false);
                smallWood.SetActive(false);
                smallPipe.SetActive(false);
                BigwoodOBJ.SetActive(true);
                BigPipe.SetActive(false);
                nails.SetActive(false);
                connector.SetActive(false);
                objectHolding = ObjectHeld.LargeWood;
                break;
            case TaskType.LargePipe:
                comboWood.SetActive(false);
                comboPipe.SetActive(false);
                smallWood.SetActive(false);
                smallPipe.SetActive(false);
                BigwoodOBJ.SetActive(false);
                BigPipe.SetActive(true);
                nails.SetActive(false);
                connector.SetActive(false);
                objectHolding = ObjectHeld.LargePipe;
                break;
            case TaskType.PipeConnector:
                comboWood.SetActive(false);
                comboPipe.SetActive(false);
                smallWood.SetActive(false);
                smallPipe.SetActive(false);
                BigwoodOBJ.SetActive(false);
                BigPipe.SetActive(false);
                nails.SetActive(false);
                connector.SetActive(true);
                objectHolding = ObjectHeld.PipeConnector;
                break;
            case TaskType.GiftBox:
                comboWood.SetActive(false);
                comboPipe.SetActive(false);
                smallWood.SetActive(false);
                smallPipe.SetActive(false);
                BigwoodOBJ.SetActive(false);
                BigPipe.SetActive(false);
                nails.SetActive(false);
                connector.SetActive(false);
                gift.SetActive(true);
                objectHolding = ObjectHeld.GiftBox;

                break;
            case TaskType.Doll:
                comboWood.SetActive(false);
                comboPipe.SetActive(false);
                smallWood.SetActive(false);
                smallPipe.SetActive(false);
                BigwoodOBJ.SetActive(false);
                BigPipe.SetActive(false);
                nails.SetActive(false);
                connector.SetActive(false);
                gift.SetActive(false);
                doll.SetActive(true);
                robot.SetActive(false);
                ball.SetActive(false);
                bat.SetActive(false);
                objectHolding = ObjectHeld.Doll;
                break;
            case TaskType.Ball:
                comboWood.SetActive(false);
                comboPipe.SetActive(false);
                smallWood.SetActive(false);
                smallPipe.SetActive(false);
                BigwoodOBJ.SetActive(false);
                BigPipe.SetActive(false);
                nails.SetActive(false);
                connector.SetActive(false);
                gift.SetActive(false);
                doll.SetActive(false);
                robot.SetActive(false);
                ball.SetActive(true);
                bat.SetActive(false);
                objectHolding = ObjectHeld.Ball;
                break;
            case TaskType.Robot:
                comboWood.SetActive(false);
                comboPipe.SetActive(false);
                smallWood.SetActive(false);
                smallPipe.SetActive(false);
                BigwoodOBJ.SetActive(false);
                BigPipe.SetActive(false);
                nails.SetActive(false);
                connector.SetActive(false);
                gift.SetActive(false);
                doll.SetActive(false);
                robot.SetActive(true);
                ball.SetActive(false);
                bat.SetActive(false);
                objectHolding = ObjectHeld.Robot;
                break;
            case TaskType.BaseballBat:
                comboWood.SetActive(false);
                comboPipe.SetActive(false);
                smallWood.SetActive(false);
                smallPipe.SetActive(false);
                BigwoodOBJ.SetActive(false);
                BigPipe.SetActive(false);
                nails.SetActive(false);
                connector.SetActive(false);
                gift.SetActive(false);
                doll.SetActive(false);
                robot.SetActive(false);
                ball.SetActive(false);
                bat.SetActive(true);
                objectHolding = ObjectHeld.BaseballBat;
                break;
            case TaskType.Computer:
                cardboardBox.SetActive(true);
                objectHolding = ObjectHeld.Box;
                break;
            case TaskType.Console:
                cardboardBox.SetActive(true);
                objectHolding = ObjectHeld.Box;
                break;
            case TaskType.Clothes:
                cardboardBox.SetActive(true);
                objectHolding = ObjectHeld.Box;
                break;
        }
    }
}