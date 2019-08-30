using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class CharacterSaver : MonoBehaviour {

    public GameObject headPanel;
    public GameObject bodyPanel;

    //private JSONPlayerSaver JSONSave;

    public int head;
    public int body;
    private LoadFromDjango ld;

    // Use this for initialization
    void Start () {
        // JSONSave = FindObjectOfType<JSONPlayerSaver>();
        ld = GameObject.FindObjectOfType<LoadFromDjango>();
    }
	
	// Update is called once per frame
	void Update () {
         head = headPanel.GetComponent<characterCreationUI>().currentHead;
         body = bodyPanel.GetComponent<characterCreationUI>().currentHead;
    }

    public void SaveCharacter()
    {

        // PlayerData data = JSONSave.LoadData(JSONSave.dataPath);
        // data.playerHead = head;
        // data.playerBody = body;
        //  JSONSave.SaveData(data, JSONSave.dataPath);
        //PlayerPrefs.SetInt("head", head);
        //PlayerPrefs.SetInt("body", body);
        //UnityWebRequest www = UnityWebRequest.Get("http://127.0.0.1:8000/savechar/"+head+"/"+body+"/");
        StartCoroutine(SaveCharacterToServer());
        ld.Head = head;
        ld.Body = body;


    }


    IEnumerator SaveCharacterToServer()
    {
        //string score = "1000000";
        UnityWebRequest www = UnityWebRequest.Get("http://127.0.0.1:8000/savechar/" + head + "/" + body + "/");
       
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
