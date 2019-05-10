﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftboxSpawn : MonoBehaviour {

    public GameObject giftboxPrefab;
    

    public bool hasBox;

	// Use this for initialization
	void Start () {
       // GameObject box =  Instantiate(giftboxPrefab, transform.position, Quaternion.Euler(-90.0f, 0.0f, 0.0f));
       // box.transform.parent = this.gameObject.transform;
        hasBox = true;
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.P))
        {
           // GameObject box =  Instantiate(giftboxPrefab, transform.position, transform.rotation);
          //  box.transform.parent = this.gameObject.transform;
       
        }
        if (hasBox == false)
        {
            Respawn();
            hasBox = true;
        }

    }

    public void Respawn()
    {
        if (!hasBox)
        {
            giftboxPrefab.SetActive(true);
            // GameObject box = Instantiate(giftboxPrefab, transform.position,  Quaternion.Euler(-90.0f,0.0f,0.0f));
            // box.transform.parent = this.gameObject.transform;
            giftboxPrefab.GetComponent<ToyOrders>().GenerateItems();
        }
    }
}
