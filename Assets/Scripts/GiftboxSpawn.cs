﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftboxSpawn : MonoBehaviour {

    public GameObject giftboxPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.P))
        {
            Instantiate(giftboxPrefab, transform.position, transform.rotation);
        }
	}
}
