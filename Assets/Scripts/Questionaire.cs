using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Questionaire : MonoBehaviour {

    public class Questions
    {
        public string question;
        public List<string> answers;
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

            if (index == 0)
            {
                questionTemp = inp_ln;
            }
            else 
            {
                answersTemp.Add(inp_ln);
            }
            index++;
            if (index == 5)
            {
                Questions q = new Questions(questionTemp, answersTemp);
                questionList.Add(q);
                index = 0;
                answersTemp = new List<string>();
            }
        }

        inp_stm.Close();
    }

    void Start()
    {
        //Debug.Log(byteText);
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
        }
    }

    void ShuffleQuestionaire()
    {
        for (int i = 0; i < questionList.Count; i++)
        {
            Swap(questionList, i, Random.Range(0, questionList.Count));
        }
    }

    private List<string> ShuffleAnswers(List<string> a)
    {
        List<string> shuffled = a;

        for (int i = 0; i < a.Count; i++)
        {
            Swap(a, i, Random.Range(0, a.Count));
        }
        return shuffled;
    }

    private void Swap<T>(List<T> list, int i, int j)
    {
        var temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
