using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class API : MonoBehaviour {

    private const string BlackboardEndpoint = "https://bbgbctest.blackboard.com/";
    private const string API_KEY = "ce2cbd52-60e4-4e94-bb23-66099afe6d16";
    private const string secret = "ANuwg76VOV7aIA0KYqfv6KWIGOWEw0FX";

    [SerializeField]
    public Text responseText;

    public void Request()
    {
        WWWForm form = new WWWForm();

        Dictionary<string, string> headers = form.headers;
        headers["grant_type"] = "client_credentials";

        WWW request = new WWW(BlackboardEndpoint, null, headers);
       
        StartCoroutine(OnResponse(request));
    }

    private IEnumerator OnResponse(WWW req)
    {
        yield return req;

        responseText.text = req.text;
    }

    private void Start()
    {
        Request();
    }

}
