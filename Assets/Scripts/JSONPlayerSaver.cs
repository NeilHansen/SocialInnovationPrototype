using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSONPlayerSaver : MonoBehaviour {

    public PlayerData playerData;
    public string dataPath;

	// Use this for initialization
	void Awake () {
        // dataPath = Path.Combine(Application.streamingAssetsPath, "PlayerData.txt");
        dataPath =(Application.streamingAssetsPath +  "/PlayerData.txt");
    }
	
    public void SaveData(PlayerData data, string path)
    {
        playerData = data;

        string jsonString = JsonUtility.ToJson(data);
        
        using (StreamWriter streamWriter = File.CreateText(path))
        {
            streamWriter.Write(jsonString);
        }
    }

    public PlayerData LoadData(string path)
    {
        //playerData.progress = data.progress;

        using (StreamReader streamReader = File.OpenText(path))
        {
            string jsonString = streamReader.ReadToEnd();
            return JsonUtility.FromJson<PlayerData>(jsonString);
        }
    }

    public void ResetData()
    {
        PlayerData data = new PlayerData();
        dataPath = (Application.streamingAssetsPath + "/PlayerData.txt");
        SaveData(data, dataPath);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
