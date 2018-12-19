using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableArea : MonoBehaviour {

    public float timer = 0;
    public float startTime = 4;
    public bool isInteracting;
    public bool isComplete;
    public AreaType areaType;
    
    public enum AreaType
    {
        None,
        PreperationArea,
        CookingArea,
        SinkArea,
        ServingArea
    }
    // Use this for initialization
    void Start () {
        switch (areaType)
        {
            case AreaType.None:
                startTime = 0;
                isComplete = true;
                break;
            case AreaType.PreperationArea:
                startTime = 4;
                isComplete = true;
                break;
            case AreaType.CookingArea:
                startTime = 3;
                isComplete = true;
                break;
            case AreaType.SinkArea:
                startTime = 3;
                isComplete = true;
                break;
            case AreaType.ServingArea:
                startTime = 2;
                isComplete = true;
                break;
        }


    }

    // Update is called once per frame
    void Update () {
		if(isInteracting && !isComplete)
        {
            timer += Time.deltaTime;
            if(timer >= startTime)
            {
                Complete();
            }
        }
        else
        {
            timer = 0.0f;
        }
	}

    void Complete()
    {
        Debug.Log("COMPLETE");
        isInteracting = false;
        isComplete = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(!isInteracting)
            {
                isInteracting = true;
                other.gameObject.GetComponent<UnitHighlight>().isInteracting = true;
            }
        }
    }
}
