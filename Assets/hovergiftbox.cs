using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hovergiftbox : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.gameObject.GetComponent<Outline>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseOver()
    {
       
        this.gameObject.GetComponent<Outline>().enabled = true;
    }

    private void OnMouseExit()
    {
       
        this.gameObject.GetComponent<Outline>().enabled = false;
    }
}
