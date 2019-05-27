﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TutorialVideoPlayer : MonoBehaviour {

    public RawImage rawImage;
    public VideoPlayer videoPlayer;


	// Use this for initialization
	void Start () {
       // StartCoroutine(PlayVideo());
	}
    private void OnEnable()
    {
      //  StartCoroutine(PlayVideo());
    }

    public IEnumerator PlayVideo()
     {
        //videoPlayer.Stop();
        Debug.Log("HERE");
        videoPlayer.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(3.5f);
        while(!videoPlayer.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }
        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();      
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
