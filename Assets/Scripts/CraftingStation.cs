using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CraftingStation : MonoBehaviour {

    public InteractableAreaConstructionSite areaManager;

    public List<Sprite> images;

    public GameObject woodImage;
    public GameObject plusImage;
    public GameObject connectorImage;
    public GameObject requiredText;

    public GameObject woodCompleteButton;
    public GameObject pipeCompleteButton;



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //for woood
        if (areaManager.numberOfBoards >= 1)
        {
            woodImage.SetActive(true);
            plusImage.SetActive(true);
            requiredText.SetActive(true);
            connectorImage.SetActive(true);
            woodImage.GetComponent<Image>().sprite = images[0];
            connectorImage.GetComponent<Image>().sprite = images[2];
            if (areaManager.numberOfBoards == 2)
            {
                requiredText.SetActive(false);
            }
        }
        if (areaManager.numberOfNails >= 1 && areaManager.numberOfBoards >= 0) 
        {
            connectorImage.SetActive(true);
            connectorImage.GetComponent<Image>().sprite = images[2];
            plusImage.SetActive(true);
            woodImage.SetActive(true);
            woodImage.GetComponent<Image>().sprite = images[0];
            requiredText.SetActive(true);
            if (areaManager.numberOfBoards == 0)
            {
                plusImage.SetActive(true);
            }
        }
  

        //complete check
        if (areaManager.numberOfBoards == 2 && areaManager.numberOfNails == 1)
        {
            // woodCompleteButton.SetActive(true);
            CompleteWood();
        }


        //for pipes
        if (areaManager.numberOfPipes >= 1)
        {
            woodImage.SetActive(true);
            plusImage.SetActive(true);
            requiredText.SetActive(true);
            woodImage.GetComponent<Image>().sprite = images[1];
            connectorImage.SetActive(true);
            connectorImage.GetComponent<Image>().sprite = images[3];
            if (areaManager.numberOfPipes == 2)
            {
                requiredText.SetActive(false);
            }
        }
        if (areaManager.numberOfConnectors >= 1 && areaManager.numberOfPipes >= 0)
        {
            woodImage.SetActive(true);
            plusImage.SetActive(true);
            requiredText.SetActive(true);
            woodImage.GetComponent<Image>().sprite = images[1];
            connectorImage.SetActive(true);
            connectorImage.GetComponent<Image>().sprite = images[3];
            if (areaManager.numberOfPipes == 0)
            {
                plusImage.SetActive(true);
            }
        }

        //complete check
        if (areaManager.numberOfPipes == 2 && areaManager.numberOfConnectors == 1)
        {
            //pipeCompleteButton.SetActive(true);
            CompletePipe();
          
        }

         
    }

    public void CompleteWood()
    {
        areaManager.numberOfBoards = 0;
        areaManager.numberOfPipes = 0;
        areaManager.numberOfNails = 0;
        areaManager.numberOfConnectors = 0;
        requiredText.SetActive(false);
        woodImage.GetComponent<Image>().sprite = images[4];
        plusImage.SetActive(false);
        connectorImage.SetActive(false);
        areaManager.interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.ComboWood;
        Debug.Log("Crafted Wood ");
       
    }

    public void CompletePipe()
    {
        areaManager.numberOfBoards = 0;
        areaManager.numberOfNails = 0;
        areaManager.numberOfPipes = 0;
        areaManager.numberOfConnectors = 0;
        requiredText.SetActive(false);
        woodImage.GetComponent<Image>().sprite = images[4];
        plusImage.SetActive(false);
        connectorImage.SetActive(false);
        areaManager.interactingUnit.gameObject.GetComponent<UnitTaskController>().CurrentTaskType = UnitTaskController.TaskType.ComboPipe;
        Debug.Log("Crafted Pipe ");
       
    }



}
