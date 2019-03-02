using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTaskController : MonoBehaviour
{

    public bool isInteracting = false;
    public TaskType currentTaskType;

    public GameObject exclamationPoint;

    public GameObject dirtyPlate;
    public GameObject cleanPlate;
    public GameObject filledPlate;
    public GameObject rawFood;
    public Canvas convoCanvas;
    [HideInInspector]
    public GameObject companion;
    //For heavy object
    public GameObject BigwoodOBJ;
    public GameObject HeavyHoldPosition;

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
        PipeConnector
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

    private void FixedUpdate()
    {
        if (currentTaskType == TaskType.LargeWood || currentTaskType == TaskType.LargePipe)
        {
            transform.position = new Vector3(HeavyHoldPosition.transform.position.x, transform.position.y, HeavyHoldPosition.transform.position.z);
        }


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
                //     Debug.Log("DOING None");
                cleanPlate.SetActive(false);
                dirtyPlate.SetActive(false);
                filledPlate.SetActive(false);
                rawFood.SetActive(false);
                BigwoodOBJ.SetActive(false);
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
                isInteracting = true;
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
                objectHolding = ObjectHeld.ComboWood;
                break;
            case TaskType.ComboPipe:
                objectHolding = ObjectHeld.ComboPipe;
                break;
            case TaskType.SmallWood:
                objectHolding = ObjectHeld.SmallWood;
                break;
            case TaskType.SmallPipe:
                objectHolding = ObjectHeld.SmallPipe;
                break;
            case TaskType.Nails:
                objectHolding = ObjectHeld.Nails;
                break;
        }
    }
}