using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSaver : MonoBehaviour {

    public GameObject headPanel;
    public GameObject bodyPanel;

    private JSONPlayerSaver JSONSave;

    public int head;
    public int body;

    // Use this for initialization
    void Start () {
        JSONSave = FindObjectOfType<JSONPlayerSaver>();

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
        PlayerPrefs.SetInt("head", head);
        PlayerPrefs.SetInt("body", body);
    }
}
