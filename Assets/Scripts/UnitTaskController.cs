using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTaskController : MonoBehaviour {

    public bool isInteracting = false;
    public TaskType currentTaskType;
    
    public GameObject exclamationPoint;

    public GameObject dirtyPlate;
    public GameObject cleanPlate;
    public GameObject filledPlate;
    public GameObject rawFood;
    public Canvas convoCanvas;

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

    public enum TaskType {
       None,
       CleanPlate,
       DirtyPlate,
       Interacting,
       Washing,
       CookedFood,
       RawFood,
       Serving,
       Counter
    }



	// Use this for initialization
	void Start () {
        
	}

    // Update is called once per frame
    void Update()
    {
        if (!isInteracting)
        {
            DoTask();
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
                cleanPlate.SetActive(false);
                dirtyPlate.SetActive(false);
                filledPlate.SetActive(false);
                rawFood.SetActive(false);
                isInteracting = false;
                break;
            case TaskType.CleanPlate:
                cleanPlate.SetActive(true);
                dirtyPlate.SetActive(false);
                filledPlate.SetActive(false);
                rawFood.SetActive(false);
                break;
            case TaskType.DirtyPlate:
                cleanPlate.SetActive(false);
                dirtyPlate.SetActive(true);
                filledPlate.SetActive(false);
                rawFood.SetActive(false);
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
                break;
            case TaskType.Washing:
                cleanPlate.SetActive(false);
                dirtyPlate.SetActive(false);
                filledPlate.SetActive(false);
                rawFood.SetActive(false);
                break;
            case TaskType.RawFood:
                cleanPlate.SetActive(false);
                dirtyPlate.SetActive(false);
                filledPlate.SetActive(false);
                rawFood.SetActive(true);
                break;
            case TaskType.Serving:
                cleanPlate.SetActive(false);
                dirtyPlate.SetActive(false);
                filledPlate.SetActive(false);
                rawFood.SetActive(false);
                break;
            case TaskType.Counter:
                cleanPlate.SetActive(false);
                dirtyPlate.SetActive(false);
                filledPlate.SetActive(false);
                rawFood.SetActive(false);
                break;
        }
    }
}
