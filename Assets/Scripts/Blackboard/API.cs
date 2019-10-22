using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.IO;

public class OAuth2Token
{
    public string access_token;
    public string token_type;
    public string expires_in;
    public string refresh_token;
    public string scope;
    public string user_id;
}
[System.Serializable]
public class Fields
{
    public string UserName;
    public string Score;
    public string FirstName;
    public string LastName; 
}
[System.Serializable]
public class LeaderBoard
{
    public string model;
    public int pk;
    public Fields fields;
}



public class API : MonoBehaviour {

    public List<string> LeaderBoardInfo;

    public string test;
    private const string BlackboardEndpoint = "https://bbgbctest.blackboard.com/learn/api/public/v1/oauth2/token";
    private const string API_KEY = "ce2cbd52-60e4-4e94-bb23-66099afe6d16";
    private const string secret = "ANuwg76VOV7aIA0KYqfv6KWIGOWEw0FX";

    [SerializeField]
    private Text responseText;

    private void Start()
    {
        // Request();
        //StartCoroutine(GetText());
    }

    public void LoadLeaderboard()
    {
        StartCoroutine(GetLeaderBoardText());
    }

    public void ClearLeaderboard()
    {
        LeaderBoardInfo.Clear();
    }

    public void IncrementScore()
    {
        StartCoroutine(AddScore());
    }


    IEnumerator GetLeaderBoardText()
    {
        UnityWebRequest www = UnityWebRequest.Get(Endpoints.url+"leaderboard/");
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

            LeaderBoard myObject = JsonUtility.FromJson<LeaderBoard>("{\"model\":" + temp + "}");

            string[] stringarray = temp.Split('{', '}');

            foreach(string token in stringarray)
            {
                if (token.Contains("UserName"))
                {
                   // Debug.Log(token);
                    LeaderBoardInfo.Add(token);
                }
            }

         //   responseText.text = temp;

        }
    }


    IEnumerator AddScore()
    {
        string score = "1000000";
        UnityWebRequest www = UnityWebRequest.Get(Endpoints.url+"addscore /100000");
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












    public void Request()
    {
        WWWForm form = new WWWForm();
        form.AddField("grant_type", "client_credentials");

        UnityWebRequest request = UnityWebRequest.Post(BlackboardEndpoint, form);
        request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");

        string OAuthURI = "ce2cbd52-60e4-4e94-bb23-66099afe6d16:ANuwg76VOV7aIA0KYqfv6KWIGOWEw0FX";

        request.SetRequestHeader("Authorization", "Basic " + EncodeTo64(OAuthURI));

        StartCoroutine(OnResponse(request));
    }
    private IEnumerator OnResponse(UnityWebRequest req)
    {
        yield return req.SendWebRequest();
        if (req.isNetworkError)
        {
            Debug.Log("Error While Sending: " + req.error);
        }
        else
        {
            Debug.Log("Received: " + req.downloadHandler.text);
        }

        responseText.text = "Token: " +  req.downloadHandler.text  + "Error Code: " + req.error;
        
        Debug.Log("Token: " + req.downloadHandler.text + "Error Code: " + req.error);
        string temp = req.downloadHandler.text;
        OAuth2Token json = JsonUtility.FromJson<OAuth2Token>(temp);
        
        Debug.LogError(json.access_token);
    }

   
    static public string EncodeTo64(string toEncode)
    {
        byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);

        string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

        return returnValue;
    }

    static public string DecodeFrom64(string encodedData)
    {
        byte[] encodedDataAsBytes = System.Convert.FromBase64String(encodedData);

        string returnValue = System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);

        return returnValue;
    }

   
   

}
