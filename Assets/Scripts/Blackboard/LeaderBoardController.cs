using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderBoardController : MonoBehaviour {

    public TextMeshProUGUI[] LeaderNames;
    public TextMeshProUGUI[] LeaderScore;
    public List<string> LeaderInfo;// not parsed

    public List<string> Names;
    public List<string> Scores;

    string tempName;
    string tempFirstName;
    string tempLastName;
    string tempScore;

    public API api;
    // Use this for initialization
    void Start () {
       
	}
    void OnEnable()
    {
        StartCoroutine(ParseInfo());
    }

    IEnumerator ParseInfo()
    {
        yield return new WaitForSeconds(0.1f);
        
        for (int i =0; i<LeaderInfo.Count; i++)
        {
            string[] temp = LeaderInfo[i].Split(',');
            foreach(string token in temp)
            {
                if (token.Contains("FirstName"))
                {
                  //  Debug.Log(token.Remove(0,15).TrimEnd('"'));
                    tempFirstName = token.Remove(0, 15).TrimEnd('"');
                }
                if (token.Contains("LastName"))
                {
                   // Debug.Log(token.Remove(0, 14).TrimEnd('"'));
                    tempLastName = token.Remove(0, 14).TrimEnd('"');
                }
                if (token.Contains("Score"))
                {
                    Debug.Log(token.Remove(0, 11).TrimEnd('"'));
                    tempScore = token.Remove(0, 11).TrimEnd('"');
                }
            }
            tempName = tempFirstName + " " + tempLastName;
            Names.Add(tempName);
            Scores.Add(tempScore);
        }
        DisplayLeaderboardInfo();
    }

    public void DisplayLeaderboardInfo()
    {
        for (int i = 0; i < Names.Count; i++)
        {
            LeaderNames[i].text = Names[i];
        }

        for (int i = 0; i < Names.Count; i++)
        {
            LeaderScore[i].text = Scores[i];
        }
    }

    public void ClearLists()
    {
        Names.Clear();
        Scores.Clear();

    }
	
	// Update is called once per frame
	void Update () {
        LeaderInfo =  api.LeaderBoardInfo;
        
    }
}
