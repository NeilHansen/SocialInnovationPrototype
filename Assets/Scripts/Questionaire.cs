using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Questionaire : MonoBehaviour {
  
    public Text questionText;
    public Text answerAText;
    public Text answerBText;
    public Text answerCText;
    public Text answerDText;
    public Text resultText;
    public TextAsset questionaireInput;

    public Button aButton;
    public Button bButton;
    public Button cButton;
    public Button dButton;
    public Button nextButton;

    public Text gameScoreText;
    public Text quizScoreText;

    private int questionIndex = 0;
    private string chosenAnswer = null;
    public int score = 0;
    private bool QuizComplete = false;
    private bool StartedGame = false;
    [SerializeField]
     public GameObject QuizScreen;
    [SerializeField]
    GameObject StartGameScreen;
    [SerializeField]
    GameObject QuitGameScreen;
    private string path;

    [SerializeField]
    Sprite[] ButtonStates;

    [SerializeField]
    Sprite[] NextButtonState;

    private LoadFromDjango ld;


   // JSONPlayerSaver JSONSave;
    public bool isPostGameQuestionnaire;
    public GameObject checkMark;
    public GameObject xMark;

    public string questionaireLocation;
    public Text QuestionsforQuiz;

    public class Questions
    {
        public string question;
        public List<string> answers;
		public string correctAnswer;
        public Questions(string q, List<string> a)
        {
            question = q;
			answers = a;
        }
    }
    private List<Questions> questionList;

    private void Awake()
    {
        path = Path.Combine(Application.streamingAssetsPath, questionaireLocation);
       // Time.timeScale = 0;
    }


  

    void ReadTextFile(string file_path)
    {
        StreamReader inp_stm = new StreamReader(file_path);

        questionList = new List<Questions>();

        int index = 0;
        List<string> answersTemp = new List<string>();
        string questionTemp = null;

        while (!inp_stm.EndOfStream)
        {
            string inp_ln = inp_stm.ReadLine();
			string sub = inp_ln.Substring(6, inp_ln.Length - 7);

            if (index == 0)
            {
                questionTemp = sub;
            }
            else 
            {
                answersTemp.Add(sub);
            }
            index++;
            if (index == 5)
            {
                Questions q = new Questions(questionTemp, answersTemp);
				q.correctAnswer = answersTemp[0];
                questionList.Add(q);
                index = 0;
                answersTemp = new List<string>();
            }
        }

        inp_stm.Close();
    }

    void ReadFromTextField (string file_path)
    {
      
        StringReader inp_stm = new StringReader(file_path);

        questionList = new List<Questions>();

        int index = 0;
        List<string> answersTemp = new List<string>();
        string questionTemp = null;

        while (true)
        {
            string inp_ln = inp_stm.ReadLine();
            if(inp_ln== null)
            {
                break;
            }
            string sub = inp_ln.Substring(6, inp_ln.Length - 7);

            if (index == 0)
            {
                questionTemp = sub;
            }
            else
            {
                answersTemp.Add(sub);
            }
            index++;
            if (index == 5)
            {
                Questions q = new Questions(questionTemp, answersTemp);
                q.correctAnswer = answersTemp[0];
                questionList.Add(q);
                index = 0;
                answersTemp = new List<string>();
            }
        }

        inp_stm.Close();
    }

    void ShuffleQuestionaire()
    {
        for (int i = 0; i < questionList.Count; i++)
        {
            Swap(questionList, i, Random.Range(0, questionList.Count));
        }
    }

    List<string> ShuffleAnswers(List<string> a)
    {
        List<string> shuffled = a;

        for (int i = 0; i < a.Count; i++)
        {
            Swap(a, i, Random.Range(0, a.Count));
        }
        return shuffled;
    }

    void Swap<T>(List<T> list, int i, int j)
    {
        var temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }

    public void ChooseAnswer(int a)
	{
		aButton.image.sprite = ButtonStates[0];
        bButton.image.sprite = ButtonStates[0];
        cButton.image.sprite = ButtonStates[0];
        dButton.image.sprite = ButtonStates[0];

		switch (a)
		{ 
			case 0:
                aButton.image.sprite = ButtonStates[1];
				chosenAnswer = answerAText.text;
				break;
			case 1:
				bButton.image.sprite= ButtonStates[1];
                chosenAnswer = answerBText.text;
				break;
			case 2:
				cButton.image.sprite= ButtonStates[1];
                chosenAnswer = answerCText.text;
				break;
			case 3:
				dButton.image.sprite= ButtonStates[1];
                chosenAnswer = answerDText.text;
				break;
			default:
				//Debug.Log("Error clicking button");
				break;
		}
	}

	void DisplayQuestion()
	{
        int questionNumber = 0;
        if (isPostGameQuestionnaire)
            questionNumber = 5;
		questionText.text = "Q" + (questionIndex + 1 + questionNumber).ToString() + ". " + questionList[questionIndex].question;
		answerAText.text = questionList[questionIndex].answers[0];
		answerBText.text = questionList[questionIndex].answers[1];
		answerCText.text = questionList[questionIndex].answers[2];
		answerDText.text = questionList[questionIndex].answers[3];
	}

    void DisplayResult()
	{
        // PlayerData data = JSONSave.LoadData(JSONSave.dataPath);
        int postQuizScoreCooks = ld.CooksQuiz;
        int postQuizScoreHabitats = ld.HomesQuiz;
        int postQuizScoreToys = ld.ToysQuiz;
        string sceneName = SceneManager.GetActiveScene().name;

       // Debug.Log("DisplayResults");
		aButton.gameObject.SetActive(false);
		bButton.gameObject.SetActive(false);
		cButton.gameObject.SetActive(false);
		dButton.gameObject.SetActive(false);
		answerAText.gameObject.SetActive(false);
		answerBText.gameObject.SetActive(false);
		answerCText.gameObject.SetActive(false);
		answerDText.gameObject.SetActive(false);
        if (!StartedGame)
        {
            questionText.text = "Your current score is " + score ;
            
            if(sceneName.Contains("Cook"))
            {
                
               // data.preQuizScoreCooks = score;
                //Debug.Log("pre quiz score is" + data.preQuizScoreCooks);
            }
            else if(sceneName.Contains("Habitat"))
            {
                //if (score > data.preQuizScoreHabitats)
                    //data.preQuizScoreHabitats = score;
            }
            else if(sceneName.Contains("Toy"))
            {
                //if (score > data.preQuizScoreToys)
                    //data.preQuizScoreToys = score;
            }
            else
            {
              //  Debug.Log("Error cant find scene name to save pre quiz score");
            }
        }
        else
        {
            questionText.text = "Your final score is " + score ;

            if (sceneName.Contains("Cook"))
            {
                if (score > postQuizScoreCooks)
                {
                    // data.postQuizScoreCooks = score;
                    ld.CooksQuiz = score;
                    StartCoroutine(SaveCooksQuiz());
                    StartCoroutine(UpdateLeaderBoardScore());

                    //Debug.Log("post quiz score is" + data.postQuizScoreCooks);
                }

            }
            else if (sceneName.Contains("Habitat"))
            {
                if (score > postQuizScoreHabitats)
                {
                    ld.HomesQuiz = score;
                    // data.postQuizScoreHabitats = score;
                    StartCoroutine(SaveHomesQuiz());
                    StartCoroutine(UpdateLeaderBoardScore());

                }
            }
            else if (sceneName.Contains("Toy"))
            {
                if (score > postQuizScoreToys)
                {
                    ld.ToysQuiz = score;
                    // data.postQuizScoreToys = score;
                    StartCoroutine(SaveToysQuiz());
                    StartCoroutine(UpdateLeaderBoardScore());
                }
                
            }
            else
            {
              //  Debug.Log("Error cant find scene name to save post quiz score");
            }
        }
		//questionText.text = "You got " + score * 100 / 10 + "%";
        QuizComplete = true;

       
    }

    //Save QuizScores

    IEnumerator SaveCooksQuiz()
    {
        //string score = "1000000";
        UnityWebRequest www = UnityWebRequest.Get("http://127.0.0.1:8000/savecooksquiz/"+score+"/");

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



    IEnumerator SaveHomesQuiz()
    {
        //string score = "1000000";
        UnityWebRequest www = UnityWebRequest.Get("http://startgbc.georgebrown.ca/savehomesquiz/" + score + "/");

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

    IEnumerator SaveToysQuiz()
    {
        //string score = "1000000";
        UnityWebRequest www = UnityWebRequest.Get("http://startgbc.georgebrown.ca/savetoyssquiz/" + score + "/");

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


    IEnumerator UpdateLeaderBoardScore()
    {
        int NewScore = ld.GiveTotalScore();
        //string score = "1000000";
        UnityWebRequest www = UnityWebRequest.Get("http://startgbc.georgebrown.ca/addscore/" + NewScore + "/");

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

















    void NextQuestion()
	{
       // Debug.Log("The quiz is complete is" + QuizComplete);
        aButton.image.sprite = ButtonStates[0];
        bButton.image.sprite = ButtonStates[0];
        cButton.image.sprite = ButtonStates[0];
        dButton.image.sprite = ButtonStates[0];
        if (QuizComplete)
        {
            
            QuizScreen.SetActive(false);
            if (!StartedGame)
            {
                StartGameScreen.SetActive(true);
                StartedGame = true;
            }
            else
            {
                //Will switch ending screen depending on the game mode
              
                switch (SceneManager.GetActiveScene().buildIndex)
                {
                    case 0:
                        //Do nothing
                        break;

                    case 1:
                        //dorm do nothing
                        

                        break;
                    case 2:
                        //Cooks in the kitchen end screen
                        QuitGameScreen.GetComponent<EndScreen>().EndGame();
                        
                        break;

                        case 3:
                        QuitGameScreen.GetComponent<EndScreenCOnstruction>().EndGame();

                        //Habitats for humanity
                        break;
                    case 4:
                        //Toys for tots
                        QuitGameScreen.GetComponent<ToysEndGame>().EndGame();
                        break;
                }

                

                QuitGameScreen.SetActive(true);
                //QuitGameScreen.GetComponent<EndScreenCOnstruction>().EndGame();
            }
            
        }

        else
        {
            //Only move to the next question if picked 1 answer
            if (chosenAnswer != null)
            {
                GameObject question;
                //Check if answer is correct
                if (chosenAnswer == questionList[questionIndex].correctAnswer)
                {
                    //GameObject question;
					score += 50;
                    if(!isPostGameQuestionnaire)
                    {
                        question = GameObject.Find("Q" + (questionIndex + 1).ToString());
                    }
                    else
                    {
                        question = GameObject.Find("Q" + (questionIndex + 6).ToString());
                    }
                    Instantiate(checkMark, question.transform);
                }
                else
                {
                    if (!isPostGameQuestionnaire)
                    {
                        question = GameObject.Find("Q" + (questionIndex + 1).ToString());
                    }
                    else
                    {
                        question = GameObject.Find("Q" + (questionIndex + 6).ToString());
                    }
                    Instantiate(xMark, question.transform);
                }
                //Check if there're still question left
                //if (questionIndex != questionList.Count - 1)
				if (questionIndex <= 3)
                {
                    questionIndex += 1;
                    DisplayQuestion();
                    chosenAnswer = null;
                    //aButton.image.color = Color.white;
                    //bButton.image.color = Color.white;
                    //cButton.image.color = Color.white;
                    //dButton.image.color = Color.white;
                }
                else
                {
                    DisplayResult();
                }
				quizScoreText.text =""+score;
            }
        }
		
	}

    

	void Start()
    {
        ld = GameObject.FindObjectOfType<LoadFromDjango>();
        // JSONSave = FindObjectOfType<JSONPlayerSaver>();

        // ReadTextFile(path);
        ReadFromTextField(QuestionsforQuiz.text);
        ShuffleQuestionaire();
        foreach (Questions q in questionList)
        {
            ShuffleAnswers(q.answers);
        }

        foreach (Questions q in questionList)
        {
            //Debug.Log(q.question);
            foreach (string s in q.answers)
            {
                //Debug.Log(s);
            }
           // Debug.Log("Correct: " + q.correctAnswer);
        }

        aButton.onClick.AddListener(delegate { ChooseAnswer(0); });
		bButton.onClick.AddListener(delegate { ChooseAnswer(1); });
		cButton.onClick.AddListener(delegate { ChooseAnswer(2); });
		dButton.onClick.AddListener(delegate { ChooseAnswer(3); });
		nextButton.onClick.AddListener(NextQuestion);


		DisplayQuestion();
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update () {
		
	}


  public void InitializeQuestionaire()
    {
        //Turns on everything
        QuizComplete = false;
        questionIndex = 0;
        aButton.gameObject.SetActive(true);
        bButton.gameObject.SetActive(true);
        cButton.gameObject.SetActive(true);
        dButton.gameObject.SetActive(true);
        answerAText.gameObject.SetActive(true);
        answerBText.gameObject.SetActive(true);
        answerCText.gameObject.SetActive(true);
        answerDText.gameObject.SetActive(true);


        QuizScreen.SetActive(true);
        
        ReadTextFile(path);      

        ShuffleQuestionaire();
        foreach (Questions q in questionList)
        {
            ShuffleAnswers(q.answers);
        }

        foreach (Questions q in questionList)
        {
           // Debug.Log(q.question);
            foreach (string s in q.answers)
            {
               // Debug.Log(s);
            }
          //  Debug.Log("Correct: " + q.correctAnswer);
        }

        aButton.onClick.AddListener(delegate { ChooseAnswer(0); });
        bButton.onClick.AddListener(delegate { ChooseAnswer(1); });
        cButton.onClick.AddListener(delegate { ChooseAnswer(2); });
        dButton.onClick.AddListener(delegate { ChooseAnswer(3); });
        //nextButton.onClick.AddListener(NextQuestion);


        DisplayQuestion();
        resultText.text = ""+ score;
        Time.timeScale = 0;
    }


    //Hover Events
    public void HoverQuestionButton(int Button)
    {
        
        switch (Button)
        {
            case 0:
                aButton.GetComponent<Image>().sprite = ButtonStates[1]; 
                break;

            case 1:
                bButton.GetComponent<Image>().sprite = ButtonStates[1];
                break;

            case 2:
                cButton.GetComponent<Image>().sprite = ButtonStates[1];
                break;

            case 3:
                dButton.GetComponent<Image>().sprite = ButtonStates[1];
                break;
        }

           
    }

    public void UnhoverButton(int B)
    {
        switch (B)
        {
            case 0:

                if(chosenAnswer != answerAText.text)
                {
                    aButton.GetComponent<Image>().sprite = ButtonStates[0];
                }
                
                break;

            case 1:

                if(chosenAnswer != answerBText.text)
                {
                    bButton.GetComponent<Image>().sprite = ButtonStates[0];
                }
               
                break;

            case 2:
                if(chosenAnswer != answerCText.text)
                {
                    cButton.GetComponent<Image>().sprite = ButtonStates[0];
                }

               
                break;

            case 3:

                if (chosenAnswer != answerDText.text)
                dButton.GetComponent<Image>().sprite = ButtonStates[0];
                break;
        }

    }

    public void SetNExtButton(int state)
    {
        //nextButton.image.sprite=NextButtonState[state];
    }
   
    //Gets all the scores  of every quiz and add them up;
    public int GetOverallQuizScore()
    {
        //PlayerData data = JSONSave.LoadData(JSONSave.dataPath);
        int BadgeScore;
        BadgeScore = ld.CooksQuiz+ ld.HomesQuiz + ld.ToysQuiz;

        return(BadgeScore);
    }

}

