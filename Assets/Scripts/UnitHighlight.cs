using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHighlight : MonoBehaviour {

    public RtsMover rtsMover;

    public Material Green;
    public Material Red;
    public bool isClicked = false;
    public bool isHovered = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if(isClicked || isHovered)
        {
            this.gameObject.GetComponent<Renderer>().material = Green;
        }
        else if(!isClicked || !isHovered)
        {
            this.gameObject.GetComponent<Renderer>().material = Red;
        }
		
	}

    private void OnMouseEnter()
    {
        isHovered = true;
    }


    private void OnMouseExit()
    {
        isHovered = false;
    }

    private void OnMouseDown()
    {
        isClicked = true;
        if (rtsMover.ActiveUnit != null)
        {
            rtsMover.ActiveUnit.GetComponent<UnitHighlight>().isClicked = false;
        }
        rtsMover.ActiveUnit = this.gameObject;
    }
}
