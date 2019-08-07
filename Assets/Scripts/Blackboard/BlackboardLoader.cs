using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using System;


[System.Serializable]
public class TokenClassName
{
    public string access_token;
}

public class BlackboardLoader : MonoBehaviour {

    public static string url = "https://bbgbctest.blackboard.com/learn/api/public/v1/oauth2/token" ;

    // Use this for initialization
    void Start () {
        DoThing();
    }
	
	// Update is called once per frame
	void Update () {

    }
   
    public void DoThing()
    {
        StartCoroutine(GetAccessToken(url));
    }

    private static IEnumerator GetAccessToken(string token)
    {
        WWWForm form = new WWWForm();

        form.AddField("redirect_uri", "uri");
        form.AddField("response_type", "login-secret");
        form.AddField("client_id", "id");
        //Fill key and value


        ///UnityWebRequest www = UnityWebRequest.Get(BlackboardLoader.url, form);
        WWW www = new WWW(url, form);

        //Send request
        yield return www.isDone;

        if (String.IsNullOrEmpty(www.error))
        {
            string resultContent = www.text;
            Debug.Log(resultContent);
            TokenClassName json = JsonUtility.FromJson<TokenClassName>(resultContent);

            //Return result
            Debug.Log("Token: " + json.access_token);
        }
        else
        {
            //Return null
            UnityEngine.Debug.Log("WWW Error: " + www.error);
        }
    }


}
