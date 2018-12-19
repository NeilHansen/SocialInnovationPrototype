using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitHighlight : MonoBehaviour {

    public RtsMover rtsMover;

    public Material Green;
    public Material Red;
    public bool isClicked = false;
    public bool isInteracting = false;

	// Update is called once per frame
	void LateUpdate () {
        if(isClicked)
        {
            this.gameObject.GetComponent<Renderer>().material = Green;
        }
        else
        {
            this.gameObject.GetComponent<Renderer>().material = Red;
        }
           
	}

    private void OnMouseEnter()
    {
        this.gameObject.GetComponent<Renderer>().material = Green;
    }

    private void OnMouseOver()
    {
        this.gameObject.GetComponent<Renderer>().material = Green;
    }


    private void OnMouseExit()
    {
        this.gameObject.GetComponent<Renderer>().material = Red;
    }

    private void OnMouseDown()
    {
        isClicked = true;
        this.gameObject.GetComponent<Renderer>().material = Green;
        if (rtsMover.ActiveUnit != null)
        {
            rtsMover.ActiveUnit.GetComponent<UnitHighlight>().isClicked = false;
            rtsMover.ActiveUnit.GetComponent<Renderer>().material = Red;
        }
        rtsMover.ActiveUnit = this.gameObject;
        this.GetComponent<NavMeshAgent>().enabled = true;
        this.GetComponent<NavMeshAgent>().isStopped = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "InteractableAre")
        {
            if(other.gameObject.GetComponent<InteractableArea>().isInteracting == false)
            {
               // isInteracting = true;
                this.GetComponent<NavMeshAgent>().isStopped = true;
                this.GetComponent<NavMeshAgent>().enabled = false;
                rtsMover.ActiveUnit = null;
                isClicked = false;
                //this.GetComponent<NavMeshAgent>().isStopped = false;
            }

        }
    }
}
