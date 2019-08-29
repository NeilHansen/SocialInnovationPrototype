using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CooksTutorialManager : MonoBehaviour {

    public GameObject player;
    public GameObject convoCanvas;
    public int tutorialProgress;
    public int tutorialEnd = 0;

    public GameObject dormBG;


    private bool doOnce = false;
    private bool doOnce1 = false;
    private bool doOnce2 = false;

    //private JSONPlayerSaver JSONSave;


    private GameManager gm;

    private GameObject UICanvas;

    public GameObject ui1;
    public GameObject ui2;
    public GameObject ui3;
    private LoadFromDjango ld;
    private void Awake()
    {
        Time.timeScale = 1.0f;
    }

    // Use this for initialization
    void Start () {
      //  JSONSave = FindObjectOfType<JSONPlayerSaver>();

        gm = FindObjectOfType<GameManager>();
        ld = GameObject.FindObjectOfType<LoadFromDjango>();
        UICanvas = GameObject.FindGameObjectWithTag("UI");

       // tutorialProgress = PlayerPrefs.GetInt("cooksIntroProgress");
        //tutorialProgress = JSONSave.LoadData(JSONSave.dataPath).cooksIntroProgress;

        

        if (ld.CooksScore == 0)
        {
            StartCoroutine(afterLoad());
           
        }
       
    }


    IEnumerator afterLoad()
    {
        yield return new WaitForSeconds(0.001f);
       
        if (ld.CooksScore != 0)
        {
            FinishTutorial();
            StopCoroutine(afterLoad());
           
        }
        else
        {
            StartTutorial();
            TurnOffControls(true);

           
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
            Time.timeScale = 0.0f;
            //ui1.SetActive(false);
            //ui2.SetActive(false);
            //ui3.SetActive(false);
        }
        else
        {
            player.GetComponent<NavMeshAgent>().enabled = true;
            Time.timeScale = 1.0f;
            ui1.SetActive(true);
            ui2.SetActive(true);
            ui3.SetActive(true);
        }
   
    }

    public void ResetTutorial()
    {
        convoCanvas.GetComponent<CooksConversationManager>().Progress = 0;
        // PlayerPrefs.SetInt("cooksIntroProgress", 0);
        //PlayerData data = new PlayerData();
        //PlayerData data = JSONSave.LoadData(JSONSave.dataPath);
        //data.cooksIntroProgress = 0;
        //JSONSave.SaveData(data, JSONSave.dataPath);

        convoCanvas.GetComponent<CooksConversationManager>().RestartTutorial();
        Debug.Log("ResetTutorial");
        TurnOffControls(true);
    
        //tv.GetComponent<InteractableArea>().isComplete = false;
    }

    public void FinishTutorial()
    {
        convoCanvas.GetComponent<CooksConversationManager>().Progress = 4;
       // PlayerPrefs.SetInt("cooksIntroProgress", 4);
        //PlayerData data = JSONSave.LoadData(JSONSave.dataPath);
        //data.cooksIntroProgress = 3;
        //JSONSave.SaveData(data, JSONSave.dataPath);

        TurnOffControls(false);
       
      
       // tv.GetComponent<InteractableArea>().isComplete = true;
        convoCanvas.SetActive(false);

    }

    public void NextTutorialPeice()
    {
       // TurnOffControls(true);
        convoCanvas.gameObject.SetActive(true);
        convoCanvas.GetComponent<CooksConversationManager>().NextTutorialPeice();
        Debug.Log("NextTutorialPeice");

    }

    public IEnumerator DoOnce()
    {
       //fade panel here!!!


        yield return new WaitForSeconds(0.001f);

     

       NextTutorialPeice();
        StopCoroutine("DoOnce");
       

    }

    public IEnumerator Outro()
    {

        yield return new WaitForSeconds(0.001f);

        // NextTutorialPeice();
        FinishTutorial();
        gm.GoToQuiz();
       
        StopCoroutine("Outro");

    }




    // Update is called once per frame
    void Update()
    {
        tutorialProgress = convoCanvas.GetComponent<CooksConversationManager>().Progress;
        // Debug.Log(tutorialProgress);
        //tutorialProgress = JSONSave.LoadData(JSONSave.dataPath).cooksIntroProgress;


        //if (tutorialProgress == 0)
        //{
        //    StartTutorial();
        //    TurnOffControls(true);
        //    dormBG.SetActive(true);

        //}


        if (tutorialProgress == 1 && !doOnce)
        {
            doOnce = true;
            StartCoroutine("DoOnce");
            dormBG.SetActive(false);

            // NextTutorialPeice();
        }

        if (tutorialProgress == 3 && !doOnce1)
        {
            doOnce1 = true;
            StartCoroutine("Outro");
            dormBG.SetActive(false);

            // NextTutorialPeice();
        }



        if (convoCanvas.gameObject.activeSelf )
        {
            FindObjectOfType<RtsMover>().ActiveUnit = null;
            Time.timeScale = 0.0f;
            TurnOffControls(true);
            ui1.SetActive(false);
            ui2.SetActive(false);
            ui3.SetActive(false);
        }
        //else if(doOnce2==false)
        //{
        //    Time.timeScale = 1.0f;
        //    doOnce2 = true;
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
