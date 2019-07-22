using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;


public class AudioManager : MonoBehaviour {
    
    public Sound[] Sounds;


    private void Awake()
    {
        //Creates audio source for every sound in the level on play and sets them up accordingly
        foreach(Sound s in Sounds)
        {
            s.Source= gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;
            s.Source.volume = s.Volume;

            s.Source.pitch = s.Pitch;
            s.Source.loop = s.Looping;

        }
    }

    public void PlaySound(string name)
    {
      Sound S = Array.Find(Sounds, sound => sound.Name == name);
        if (S.Source != null && S.Source.isPlaying == false)
        {
            S.Source.Play();
        }
        
    }

    public void StopSound(string name)
    {
        Sound S = Array.Find(Sounds, sound => sound.Name == name);
        if (S.Source != null)
        {
            S.Source.Stop();
        }

    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
