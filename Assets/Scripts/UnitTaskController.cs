using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTaskController : MonoBehaviour {

    public bool isInteracting = false;
    public TaskType currentTaskType;

    public GameObject exclamationPoint;

    public TaskType CurrentTaskType
    {
        get
        {
            return currentTaskType;
        }

        set
        {
            currentTaskType = value;
            StartCoroutine("FlashFeedback");
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
       Serving
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
                isInteracting = false;
                break;
            case TaskType.CleanPlate:
                break;
            case TaskType.DirtyPlate:
                break;
            case TaskType.Interacting:
                isInteracting = true;
                break;
            case TaskType.CookedFood:
                break;
            case TaskType.Washing:
                break;
            case TaskType.RawFood:
                break;
            case TaskType.Serving:
                break;
        }
    }
}
