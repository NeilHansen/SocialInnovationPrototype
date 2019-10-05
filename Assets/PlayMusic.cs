using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour {
    AudioManager AM;



	// Use this for initialization
	void Start () {
        AM=GetComponent<AudioManager>();
        AM.PlaySound("music");

    }
	
	// Update is called once per frame
	void Update () {
		
	}

   
}
