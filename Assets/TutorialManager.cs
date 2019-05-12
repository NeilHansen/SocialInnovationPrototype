using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TutorialManager : MonoBehaviour {

    public GameObject player;
    public GameObject convoCanvas;
    public int tutorialProgress;
    public int tutorialEnd = 0;
    public GameObject compBox;
    public GameObject clothesBox;
    public GameObject tvBox;
    public GameObject comp;
    public GameObject clothes;
    public GameObject tv;


    public GameObject console;
    public GameObject laptop;


    private JSONPlayerSaver JSONSave;


    private void Awake()
    {
        Time.timeScale = 1.0f;
    }

    // Use this for initialization
    void Start () {
        JSONSave = FindObjectOfType<JSONPlayerSaver>();

        //tutorialProgress = PlayerPrefs.GetInt("tutorialProgress");
        tutorialProgress = JSONSave.LoadData(JSONSave.dataPath).progress;
        if (tutorialProgress == 0)
        {
            StartTutorial();
            TurnOffControls(true);
        }
        else if (tutorialProgress == tutorialEnd)
        {
            comp.GetComponent<InteractableArea>().TutorialComplete = false;
            tv.GetComponent<InteractableArea>().TutorialComplete = false;
            clothes.GetComponent<InteractableArea>().TutorialComplete = false;
            
        }
        //else if(tutorialProgress <= tutorialEnd)
        //{
        //    TurnOffControls(false);
        //    convoCanvas.SetActive(false);
        //    FinishTutorial();
        //}
        //else if (tutorialProgress != 0 && tutorialProgress <= 4)
        //{
        //    ResetTutorial();
        //    TurnOffControls(true);
        //}


    }

    public void StartTutorial()
    {
        convoCanvas.SetActive(true);
        TurnOffControls(true);
    }

    public void TurnOffControls(bool off)
    {
        if (off)
        { 
            player.GetComponent<NavMeshAgent>().enabled = false;
        }
        else
        {
            player.GetComponent<NavMeshAgent>().enabled = true;
        }
   
    }

    public void ResetTutorial()
    {
        //PlayerPrefs.SetInt("tutorialProgress", 0);
        //PlayerData data = new PlayerData();
        PlayerData data = JSONSave.LoadData(JSONSave.dataPath);
        data.progress = 0;
        JSONSave.SaveData(data, JSONSave.dataPath);

        convoCanvas.GetComponent<ConversationManager>().RestartTutorial();
        Debug.Log("ResetTutorial");
        TurnOffControls(true);
        compBox.SetActive(true);
        tvBox.SetActive(true);
        clothesBox.SetActive(true);
        comp.GetComponent<InteractableArea>().TutorialComplete = false;
        tv.GetComponent<InteractableArea>().TutorialComplete = false;
        clothes.GetComponent<InteractableArea>().TutorialComplete = false;
        //tv.GetComponent<InteractableArea>().isComplete = false;
    }

    public void FinishTutorial()
    {
        //PlayerPrefs.SetInt("tutorialProgress", 4);
        PlayerData data = JSONSave.LoadData(JSONSave.dataPath);
        data.progress = 4;
        JSONSave.SaveData(data, JSONSave.dataPath);

        TurnOffControls(false);
        //Destroy(compBox);
        //Destroy(tvBox);
        //Destroy(clothesBox);
        compBox.SetActive(false);
        tvBox.SetActive(false);
        clothesBox.SetActive(false);
        comp.GetComponent<InteractableArea>().TutorialComplete = true;
        tv.GetComponent<InteractableArea>().TutorialComplete = true;
        clothes.GetComponent<InteractableArea>().TutorialComplete = true;
       // tv.GetComponent<InteractableArea>().isComplete = true;
        convoCanvas.SetActive(false);

    }

    public void NextTutorialPeice()
    {
        TurnOffControls(true);
        convoCanvas.gameObject.SetActive(true);
        convoCanvas.GetComponent<ConversationManager>().NextTutorialPeice();
        Debug.Log("NextTutorialPeice");
    }

    // Update is called once per frame
    void Update()
    {
        //tutorialProgress = PlayerPrefs.GetInt("tutorialProgress");
        Debug.Log(tutorialProgress);
        tutorialProgress = JSONSave.LoadData(JSONSave.dataPath).progress;

        if (tutorialProgress == 0)
        {
            StartTutorial();
            TurnOffControls(true);
        }

        //switch(tutorialProgress)
        //{
        //    case 0:
        //        console.SetActive(false);
        //        laptop.SetActive(false);
        //        break;
        //    case 1:
        //        console.SetActive(false);
        //        laptop.SetActive(false);
        //        break;
        //    case 2:
        //        console.SetActive(false);
        //        laptop.SetActive(true);
        //        break;
        //    case 3:
        //        console.SetActive(false);
        //        laptop.SetActive(true);
        //        break;
        //    case 4:
        //        console.SetActive(true);
        //        laptop.SetActive(true);
        //        break;
        //}
        

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetTutorial();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            FinishTutorial();
        }

        if (tutorialProgress < tutorialEnd)
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                //PlayerPrefs.SetInt("tutorialProgress", tutorialProgress+=1);
                NextTutorialPeice();
            }
        }

    }

}
