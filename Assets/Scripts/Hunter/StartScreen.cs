using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour {
   
  
    

	// Use this for initialization
	void Start () {
        Time.timeScale = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BeginGame()
    {
       gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
