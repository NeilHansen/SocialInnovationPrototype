using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    private int questionIndex = 0;
    private string chosenAnswer = null;
    public int score = 0;
    private bool QuizComplete = false;
    private bool StartedGame = false;
    [SerializeField]
    GameObject QuizScreen;
    [SerializeField]
    GameObject StartGameScreen;
    [SerializeField]
    GameObject QuitGameScreen;
    private string path;

    [SerializeField]
    Sprite[] ButtonStates;

    [SerializeField]
    Sprite[] NextButtonState;


    JSONPlayerSaver JSONSave;


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
        path = Path.Combine(Application.streamingAssetsPath, "Questionaire Real.txt");
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
		questionText.text = "Q" + (questionIndex + 1).ToString() + ". " + questionList[questionIndex].question;
		answerAText.text = questionList[questionIndex].answers[0];
		answerBText.text = questionList[questionIndex].answers[1];
		answerCText.text = questionList[questionIndex].answers[2];
		answerDText.text = questionList[questionIndex].answers[3];
	}

    void DisplayResult()
	{
        PlayerData data = JSONSave.LoadData(JSONSave.dataPath);
        string sceneName = SceneManager.GetActiveScene().name;

        Debug.Log("DisplayResults");
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
            questionText.text = "Your current score is " + score * 100 / 10 + "%";
            
            if(sceneName.Contains("Cook"))
            {
                if(score > data.preQuizScoreCooks)
                    data.preQuizScoreCooks = score;
            }
            else if(sceneName.Contains("Habitat"))
            {
                if (score > data.preQuizScoreHabitats)
                    data.preQuizScoreHabitats = score;
            }
            else if(sceneName.Contains("Toy"))
            {
                if (score > data.preQuizScoreToys)
                    data.preQuizScoreToys = score;
            }
            else
            {
                Debug.Log("Error cant find scene name to save pre quiz score");
            }
        }
        else
        {
            questionText.text = "Your final score is " + score * 100 / 10 + "%";

            if (sceneName.Contains("Cook"))
            {
                if(score > data.postQuizScoreCooks)
                    data.postQuizScoreCooks = score;
            }
            else if (sceneName.Contains("Habitat"))
            {
                if (score > data.postQuizScoreHabitats)
                    data.postQuizScoreHabitats = score;
            }
            else if (sceneName.Contains("Toy"))
            {
                if (score > data.postQuizScoreToys)
                    data.postQuizScoreToys = score;
            }
            else
            {
                Debug.Log("Error cant find scene name to save post quiz score");
            }
        }
		//questionText.text = "You got " + score * 100 / 10 + "%";
        QuizComplete = true;

        if (sceneName.Contains("Cook"))
        {
            data.totalQuizScoreCooks = data.preQuizScoreCooks + data.postQuizScoreCooks;
        }
        else if (sceneName.Contains("Habitat"))
        {
            data.totalQuizScoreHabitats = data.preQuizScoreHabitats + data.postQuizScoreHabitats;
        }
        else if (sceneName.Contains("Toy"))
        {
            data.totalQuizScoreToys = data.preQuizScoreToys + data.postQuizScoreToys;
        }
        else
        {
            //Debug.Log("Error cant find scene name to save total quiz score");
        }

        JSONSave.SaveData(data, JSONSave.dataPath);
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
                //Check if answer is correct
                if (chosenAnswer == questionList[questionIndex].correctAnswer)
                {
					score += 1;
                }
                //Check if there're still question left
                //if (questionIndex != questionList.Count - 1)
				if (questionIndex <= 3)
                {
                    questionIndex += 1;
                    DisplayQuestion();
                    chosenAnswer = null;
                    aButton.image.color = Color.white;
                    bButton.image.color = Color.white;
                    cButton.image.color = Color.white;
                    dButton.image.color = Color.white;
                }
                else
                {
                    DisplayResult();
                }
				resultText.text = score + "/10";
            }
        }
		
	}

    

	void Start()
    {
        JSONSave = FindObjectOfType<JSONPlayerSaver>();

        ReadTextFile(path);

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
            Debug.Log("Correct: " + q.correctAnswer);
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
            Debug.Log(q.question);
            foreach (string s in q.answers)
            {
               // Debug.Log(s);
            }
            Debug.Log("Correct: " + q.correctAnswer);
        }

        aButton.onClick.AddListener(delegate { ChooseAnswer(0); });
        bButton.onClick.AddListener(delegate { ChooseAnswer(1); });
        cButton.onClick.AddListener(delegate { ChooseAnswer(2); });
        dButton.onClick.AddListener(delegate { ChooseAnswer(3); });
        //nextButton.onClick.AddListener(NextQuestion);


        DisplayQuestion();
        resultText.text = score + "/10";
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
        nextButton.image.sprite=NextButtonState[state];
    }

}

