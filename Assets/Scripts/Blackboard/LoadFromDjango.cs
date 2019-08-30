using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.IO;


public class LoadFromDjango : MonoBehaviour {

    public List<string> PlayerData;

    public string Username;
    public int Head;
    public int Body;
    public int Tutorial;
    public int CooksScore;
    public int CooksQuiz;
    public int HomesScore;
    public int HomesQuiz;
    public int ToysScore;
    public int ToysQuiz;
    



    [SerializeField]
    private Text responseText;

    private void Start()
    {
         LoadData();
        //StartCoroutine(GetText());
    }

    public void LoadData()
    {
        StartCoroutine(GetPlayerDataFromSever());
    }
    
    public void SetValues()
    {
        Username =  PlayerData[0];
        Head = System.Convert.ToInt32(PlayerData[1]);
        Body = System.Convert.ToInt32(PlayerData[2]);
        Tutorial = System.Convert.ToInt32(PlayerData[3]);
        CooksScore = System.Convert.ToInt32(PlayerData[4]);
        CooksQuiz = System.Convert.ToInt32(PlayerData[5]);
        HomesScore = System.Convert.ToInt32(PlayerData[6]);
        HomesQuiz = System.Convert.ToInt32(PlayerData[7]);
        ToysScore = System.Convert.ToInt32(PlayerData[8]);
        ToysQuiz = System.Convert.ToInt32(PlayerData[9]);
    }

    public int GiveTotalScore()
    {
        int OverallScore = CooksScore + CooksQuiz + HomesQuiz + HomesScore + ToysQuiz + ToysScore;

        return (OverallScore);
    }


    IEnumerator GetPlayerDataFromSever()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://127.0.0.1:8000/loadplayerdata/");
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

           // LeaderBoard myObject = JsonUtility.FromJson<LeaderBoard>("{\"model\":" + temp + "}");

            string[] stringarray = temp.Split('{', '}');

            foreach(string token in stringarray)
            {
                if (token.Contains("UserName"))
                {
                   string[] tmpstringarray = token.Split(',',' ');

                    foreach (string tmptoken in tmpstringarray)
                    {
                        if (!tmptoken.EndsWith( ":"))
                            {
                            if(tmptoken!="")
                            {
                                PlayerData.Add(tmptoken.TrimEnd('"').TrimStart('"'));
                            }

                        }
                    }
                }
            }
            //responseText.text = temp;
            SetValues();
        }
    }


    IEnumerator SetPlayerDataToServer()
    {
        string score = "1000000";
        UnityWebRequest www = UnityWebRequest.Get("http://127.0.0.1:8000/addscore/100000");
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












  
   
   

}
