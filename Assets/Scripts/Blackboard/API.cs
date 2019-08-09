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

public class API : MonoBehaviour {

    public string test;
    private const string BlackboardEndpoint = "https://bbgbctest.blackboard.com/learn/api/public/v1/oauth2/token";
    private const string API_KEY = "ce2cbd52-60e4-4e94-bb23-66099afe6d16";
    private const string secret = "ANuwg76VOV7aIA0KYqfv6KWIGOWEw0FX";

    [SerializeField]
    private Text responseText;

    public void Request()
    {
        WWWForm form = new WWWForm();
        form.AddField("grant_type", "client_credentials");


        //headers["grant_type"] = "client_credentials";

        //form.AddField("grant_type", "ce2cbd52-60e4-4e94-bb23-66099afe6d16:ANuwg76VOV7aIA0KYqfv6KWIGOWEw0FX");
        //form.AddField("Authorization", "ANuwg76VOV7aIA0KYqfv6KWIGOWEw0FX");
       
      //  WWW request = new WWW(BlackboardEndpoint,form.data, headers);
       
        UnityWebRequest request = UnityWebRequest.Post(BlackboardEndpoint, form);
        request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        
        string blah = "ce2cbd52-60e4-4e94-bb23-66099afe6d16:ANuwg76VOV7aIA0KYqfv6KWIGOWEw0FX";
        // blah =  System.Text.Encoding.UTF8.GetBytes(blah);
        request.SetRequestHeader("Authorization","Basic "  + EncodeTo64(blah));
        //request.SetRequestHeader("grant_type", "client_credentials");
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
        // { "access_token":"tE1RgRvCI12WpQEa6mZbOiAO3954Sz22","token_type":"bearer","expires_in":2582}
        Debug.LogError(json.access_token);
        //StartCoroutine(rest(json.access_token));

    }

    public IEnumerator rest(string token)
    {
        WWWForm form = new WWWForm();
        form.AddField("input", test);

        UnityWebRequest request = UnityWebRequest.Post("https://bbgbctest.blackboard.com/learn/api/public/v1/announcements", form);
        request.SetRequestHeader("Content-Type", "application/json");

        // blah =  System.Text.Encoding.UTF8.GetBytes(blah);
        request.SetRequestHeader("Authorization", "Bearer " + token);
        //  request.SetRequestHeader("grant_type", "client_credentials");

        yield return request.SendWebRequest();
        if (request.isNetworkError)
        {
            Debug.Log("Error While Sending: " + request.error);
        }
        else
        {
            Debug.Log("Received: " + request.downloadHandler.text);
        }



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

    private void Start()
    {
        Request();
    }

}
