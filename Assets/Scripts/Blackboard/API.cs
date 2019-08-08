using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class API : MonoBehaviour {

    private const string BlackboardEndpoint = "https://bbgbctest.blackboard.com/learn/api/public/v1/oauth2/token";
    private const string API_KEY = "ce2cbd52-60e4-4e94-bb23-66099afe6d16";
    private const string secret = "ANuwg76VOV7aIA0KYqfv6KWIGOWEw0FX";

    [SerializeField]
    private Text responseText;

    public void Request()
    {
        WWWForm form = new WWWForm();

        Dictionary<string, string> headers = form.headers;
        //headers["grant_type"] = "client_credentials";
        headers["Authorization"] = API_KEY + secret;
        form.AddField("grant_type", "ce2cbd52-60e4-4e94-bb23-66099afe6d16:ANuwg76VOV7aIA0KYqfv6KWIGOWEw0FX");
        //form.AddField("Authorization", "ANuwg76VOV7aIA0KYqfv6KWIGOWEw0FX");
       
        WWW request = new WWW(BlackboardEndpoint,form.data, headers);
       
        //UnityWebRequest request = UnityWebRequest.Post(BlackboardEndpoint, form);
        
        StartCoroutine(OnResponse(request));
    }

    private IEnumerator OnResponse(WWW req)
    {
        yield return req;

        responseText.text = "Token: " +  req.text  + "Error Code: " + req.error;
        Debug.Log("Token: " + req.text + "Error Code: " + req.error);
    }

    private void Start()
    {
        Request();
    }

}
