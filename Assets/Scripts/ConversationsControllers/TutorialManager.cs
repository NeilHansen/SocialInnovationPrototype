using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.IO;
using UnityEngine.Networking;


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

    public GameObject CharecterCreationPanel;
    public GameObject ConsolePanel;

    private LoadFromDjango ld;
    private bool DoOnce =false;
    //   private JSONPlayerSaver JSONSave;


    private void Awake()
    {
        Time.timeScale = 1.0f;
       
    }

    // Use this for initialization
    void Start () {
        //    JSONSave = FindObjectOfType<JSONPlayerSaver>();
        //tutorialProgress = JSONSave.LoadData(JSONSave.dataPath).progress;
        ld = GameObject.FindObjectOfType<LoadFromDjango>();
       // tutorialProgress = PlayerPrefs.GetInt("tutorialProgress");

        if (ld.Tutorial == 0)
        {
            StartCoroutine(afterLoad());
        }
    }

    IEnumerator afterLoad()

    {
        yield return new WaitForSeconds(0.01f);
        if (ld.Tutorial == 1)
        {
            comp.GetComponent<InteractableArea>().TutorialComplete = false;
            tv.GetComponent<InteractableArea>().TutorialComplete = false;
            clothes.GetComponent<InteractableArea>().TutorialComplete = false;
            FinishTutorial();
            StopCoroutine(afterLoad());
        }
        else
        {
            Debug.Log("FUCK shit" + ld.Tutorial);
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
                FinishTutorial();
            }
            else if (tutorialProgress != tutorialEnd)
            {
                ResetTutorial();
                StartTutorial();
                TurnOffControls(true);
            }
        }
        
       
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
        convoCanvas.GetComponent<ConversationManager>().progress = 0;
       // PlayerData data = new PlayerData();
       // PlayerData data = JSONSave.LoadData(JSONSave.dataPath);
       //data.progress = 0;
       // JSONSave.SaveData(data, JSONSave.dataPath);

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
        convoCanvas.GetComponent<ConversationManager>().progress = 4;
        StartCoroutine(SaveTutorialComplete());
       // PlayerData data = JSONSave.LoadData(JSONSave.dataPath);
       // data.progress = 4;
       // JSONSave.SaveData(data, JSONSave.dataPath);

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

    IEnumerator SaveTutorialComplete()
    {
       // Deb
        //string score = "1000000";
        UnityWebRequest www = UnityWebRequest.Get("http://startgbc.georgebrown.ca/savedorm/");

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
           
            // Show results as text
            Debug.Log(www.downloadHandler.text);

            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;

            string temp = www.downloadHandler.text;

            // responseText.text = temp;

        }
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
        tutorialProgress = convoCanvas.GetComponent<ConversationManager>().progress;
        //  Debug.Log(tutorialProgress);
        //  tutorialProgress = JSONSave.LoadData(JSONSave.dataPath).progress;


        if (tutorialProgress == 4 && !DoOnce)
        {
            DoOnce = true; 
            FinishTutorial();
            TurnOffControls(false);
        }

        if (CharecterCreationPanel.activeSelf || ConsolePanel.activeSelf)
        {
            FindObjectOfType<RtsMover>().ActiveUnit = null;
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
        

        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    ResetTutorial();
        //}
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    FinishTutorial();
        //}

        //if (tutorialProgress < tutorialEnd)
        //{
        //    if (Input.GetKeyDown(KeyCode.N))
        //    {
        //        //PlayerPrefs.SetInt("tutorialProgress", tutorialProgress+=1);
        //        NextTutorialPeice();
        //    }
        //}

    }

}
