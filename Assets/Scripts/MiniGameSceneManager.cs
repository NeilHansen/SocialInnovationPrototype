using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameSceneManager : MonoBehaviour {
    //name of mini game

    

    //load that index
    public void LoadScene(string levelToLoad)
    {
      SceneManager.LoadScene(levelToLoad);
    }
}
