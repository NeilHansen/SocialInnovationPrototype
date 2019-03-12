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

    private int numberOfUnits = 0;

    //For keeping The players on Big Wood
    

    void Start()
    {
        numberOfUnits = GameObject.FindGameObjectsWithTag("Player").Length;
    }

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

        if (Input.GetKeyDown("1"))
        {
            if (rtsMover.Unit1.GetComponent<UnitTaskController>().currentTaskType == UnitTaskController.TaskType.LargeWood)
            {
                rtsMover.BigWood.gameObject.GetComponent<UnitHighlight>().PossesUnit();
                Debug.Log("Posses the wood");
            }


            else if(rtsMover.Unit1.GetComponent<UnitTaskController>().currentTaskType == UnitTaskController.TaskType.LargePipe)
            {
                rtsMover.BigPipe.gameObject.GetComponent<UnitHighlight>().PossesUnit();
            }

           else if (rtsMover.ActiveUnit != rtsMover.Unit1 || rtsMover.ActiveUnit == null)
            {
               rtsMover.Unit1.gameObject.GetComponent<UnitHighlight>().PossesUnit();
              
            }
            

        }

        
        if (Input.GetKeyDown("2"))
        {
            if (rtsMover.Unit2.GetComponent<UnitTaskController>().currentTaskType == UnitTaskController.TaskType.LargeWood)
            {
                rtsMover.BigWood.gameObject.GetComponent<UnitHighlight>().PossesUnit();
                Debug.Log("Posses the wood");
            }

            else if (rtsMover.Unit2.GetComponent<UnitTaskController>().currentTaskType == UnitTaskController.TaskType.LargePipe)
            {
                rtsMover.BigPipe.gameObject.GetComponent<UnitHighlight>().PossesUnit();
            }

            else if (rtsMover.ActiveUnit != rtsMover.Unit2 || rtsMover.ActiveUnit == null)
            {
                rtsMover.Unit2.gameObject.GetComponent<UnitHighlight>().PossesUnit();
            }

         

        }

        if (numberOfUnits >= 3)
        {
            if (Input.GetKeyDown("3"))
            {
                if (rtsMover.Unit1.GetComponent<UnitTaskController>().currentTaskType == UnitTaskController.TaskType.LargeWood)
                {
                    rtsMover.BigWood.gameObject.GetComponent<UnitHighlight>().PossesUnit();
                    Debug.Log("Posses the wood");
                }

                else if (rtsMover.Unit3.GetComponent<UnitTaskController>().currentTaskType == UnitTaskController.TaskType.LargePipe)
                {
                    rtsMover.BigPipe.gameObject.GetComponent<UnitHighlight>().PossesUnit();
                }

                else if (rtsMover.ActiveUnit != rtsMover.Unit3 || rtsMover.ActiveUnit == null)
                {
                    rtsMover.Unit3.gameObject.GetComponent<UnitHighlight>().PossesUnit();
                }
               
            }
        }

        if (numberOfUnits >= 4)
        {
            if (Input.GetKeyDown("4"))
            {
                if (rtsMover.ActiveUnit != rtsMover.Unit4 || rtsMover.ActiveUnit == null)
                {
                    rtsMover.Unit4.gameObject.GetComponent<UnitHighlight>().PossesUnit();
                }
                else
                {

                }
            }
        }



    }

    private void OnMouseEnter()
    {
        this.gameObject.GetComponent<Renderer>().material = Green;
    }

    private void OnMouseOver()
    {
        this.gameObject.GetComponent<Renderer>().material = Green;
        this.rtsMover.enabled = false;
    }


    private void OnMouseExit()
    {
        this.rtsMover.enabled = true;
        this.gameObject.GetComponent<Renderer>().material = Red;
    }

    private void OnMouseDown()
    {
        if(rtsMover.ActiveUnit != this.gameObject)
        PossesUnit();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "InteractableAre" )
        {
            if(other.gameObject.GetComponent<InteractableArea>().isInteracting == false)
            {
                isInteracting = true;
                this.GetComponent<NavMeshAgent>().isStopped = true;
                this.GetComponent<NavMeshAgent>().enabled = false;
                rtsMover.ActiveUnit = null;
                isClicked = false;
                //this.GetComponent<NavMeshAgent>().isStopped = false;
            }

        }
        if (other.gameObject.tag == "constructionarea" && other.gameObject.GetComponent<InteractableAreaConstructionSite>().UnitToMoveTo == rtsMover.ActiveUnit)
        {
            if (other.gameObject.GetComponent<InteractableAreaConstructionSite>().isInteracting == false)
            {
                isInteracting = true;
                
                this.GetComponent<NavMeshAgent>().isStopped = true;
                this.GetComponent<NavMeshAgent>().enabled = false;
                rtsMover.ActiveUnit = null;
                isClicked = false;
                //this.GetComponent<NavMeshAgent>().isStopped = false;
            }

        }

        if (other.gameObject.tag == "SpecialCustomer")
        {
            rtsMover.ActiveUnit = null;
            isClicked = false;
            other.gameObject.GetComponent<SphereCollider>().enabled = false;
            Debug.Log(this.gameObject.name + " is talking to special customer");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "SpecialCustomer")
        {
            other.gameObject.GetComponent<SphereCollider>().enabled = true;
        }
    }

    public void PossesUnit()
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
}
