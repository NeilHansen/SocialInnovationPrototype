﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class Questionaire : MonoBehaviour {

	public Text questionText;
	public Text answerAText;
	public Text answerBText;
	public Text answerCText;
	public Text answerDText;
    
	public Button aButton;
	public Button bButton;
	public Button cButton;
	public Button dButton;
	public Button nextButton;

	private int questionIndex = 0;
	private string chosenAnswer = null;
	private int score = 0;

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

    void readTextFile(string file_path)
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
		aButton.image.color = Color.white;
		bButton.image.color = Color.white;
		cButton.image.color = Color.white;
		dButton.image.color = Color.white;

		switch (a)
		{ 
			case 0:
				aButton.image.color = Color.green;
				chosenAnswer = answerAText.text;
				break;
			case 1:
				bButton.image.color = Color.green;
				chosenAnswer = answerBText.text;
				break;
			case 2:
				cButton.image.color = Color.green;
				chosenAnswer = answerCText.text;
				break;
			case 3:
				dButton.image.color = Color.green;
				chosenAnswer = answerDText.text;
				break;
			default:
				Debug.Log("Error clicking button");
				break;
		}
	}

	void DisplayQuestion()
	{
		questionText.text = questionList[questionIndex].question;
		answerAText.text = questionList[questionIndex].answers[0];
		answerBText.text = questionList[questionIndex].answers[1];
		answerCText.text = questionList[questionIndex].answers[2];
		answerDText.text = questionList[questionIndex].answers[3];
	}

    void DisplayResult()
	{
		aButton.gameObject.SetActive(false);
		bButton.gameObject.SetActive(false);
		cButton.gameObject.SetActive(false);
		dButton.gameObject.SetActive(false);
		answerAText.gameObject.SetActive(false);
		answerBText.gameObject.SetActive(false);
		answerCText.gameObject.SetActive(false);
		answerDText.gameObject.SetActive(false);
		questionText.text = "You got " + score * 100 / questionList.Count + "%";
	}

    void NextQuestion()
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
            if (questionIndex != questionList.Count - 1)
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
		}
	}

	void Start()
    {
        readTextFile("Assets/Resources/Questionaire.txt");

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
                Debug.Log(s);
            }
            Debug.Log("Correct: " + q.correctAnswer);
        }

		aButton.onClick.AddListener(delegate { ChooseAnswer(0); });
		bButton.onClick.AddListener(delegate { ChooseAnswer(1); });
		cButton.onClick.AddListener(delegate { ChooseAnswer(2); });
		dButton.onClick.AddListener(delegate { ChooseAnswer(3); });
		nextButton.onClick.AddListener(NextQuestion);


		DisplayQuestion();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
