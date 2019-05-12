using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitHighlight : MonoBehaviour {

    public RtsMover rtsMover;

    public GameObject playerMesh;
    public Material Green;
    public Material Red;
    public bool isClicked = false;
    public bool isHovered = false;

    public bool isInteracting = false;

    private int numberOfUnits = 0;

    private GameObject specialCustomer = null;

    //For keeping The players on Big Wood
    

    void Start()
    {
        rtsMover = Camera.main.GetComponent<RtsMover>();
        numberOfUnits = GameObject.FindGameObjectsWithTag("Player").Length;
    }

    // Update is called once per frame
    void LateUpdate () {
        if(isClicked || isHovered)
        {
            //this.gameObject.GetComponent<Renderer>().material = Green;
           // playerMesh.GetComponent<Renderer>().material = Green;
            this.GetComponent<Outline>().enabled = true;
        }
        else
        {
            //this.gameObject.GetComponent<Renderer>().material = Red;
            //playerMesh.GetComponent<Renderer>().material = Red;
            this.GetComponent<Outline>().enabled = false;
        }

        if (Input.GetKeyDown("1"))
        {
          if (rtsMover.ActiveUnit != rtsMover.Unit1 || rtsMover.ActiveUnit == null)
            {
               rtsMover.Unit1.gameObject.GetComponent<UnitHighlight>().PossesUnit();
            }
        }

        
        if (Input.GetKeyDown("2"))
        {
            if (rtsMover.ActiveUnit != rtsMover.Unit2 || rtsMover.ActiveUnit == null)
            {
                rtsMover.Unit2.gameObject.GetComponent<UnitHighlight>().PossesUnit();
            }
        }

        if (numberOfUnits >= 3)
        {
            if (Input.GetKeyDown("3"))
            {
                if (rtsMover.ActiveUnit != rtsMover.Unit3 || rtsMover.ActiveUnit == null)
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

    private void Update()
    {
        if(specialCustomer != null)
        {
            if(Vector3.Distance(specialCustomer.transform.position, transform.position) > 5.0f)
            {
                specialCustomer.GetComponent<SphereCollider>().enabled = true;
                specialCustomer = null;
            }
        }
    }

    private void OnMouseEnter()
    {
      //  this.gameObject.GetComponent<Renderer>().material = Green;
     //   playerMesh.GetComponent<Renderer>().material = Green;
        this.GetComponent<Outline>().enabled = true;
        isHovered = true;
    }

    private void OnMouseOver()
    {
        //  this.gameObject.GetComponent<Renderer>().material = Green;
        // playerMesh.GetComponent<Renderer>().material = Green;
        this.GetComponent<Outline>().enabled = true;
        this.rtsMover.enabled = false;
        isHovered = true;


    }


    private void OnMouseExit()
    {
        this.rtsMover.enabled = true;
        // this.gameObject.GetComponent<Renderer>().material = Red;
        //  playerMesh.GetComponent<Renderer>().material = Red;
        
        this.GetComponent<Outline>().enabled = false;
        isHovered = false;
    }

    private void OnMouseDown()
    {
        if(rtsMover.ActiveUnit != this.gameObject)
        PossesUnit();
    }


    private void OnTriggerEnter(Collider other )
    {
        if (other.gameObject.tag == "InteractableAre" && other.gameObject.GetComponent<InteractableArea>().UnitToMoveTo == rtsMover.ActiveUnit)
        {
            if(other.gameObject.GetComponent<InteractableArea>().isInteracting == false)
            {
                isInteracting = true;

                this.GetComponent<NavMeshAgent>().isStopped = true;
                this.GetComponent<NavMeshAgent>().enabled = false;
                this.GetComponent<NavMeshAgent>().enabled = true;
                // rtsMover.ActiveUnit = null;
                // isClicked = false;
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
                this.GetComponent<NavMeshAgent>().enabled = true;
                // rtsMover.ActiveUnit = null;
                // isClicked = false;
                //this.GetComponent<NavMeshAgent>().isStopped = false;
            }

        }

        if (other.gameObject.tag == "SpecialCustomer")
        {
            rtsMover.ActiveUnit = null;
            isClicked = false;
            other.gameObject.GetComponent<SphereCollider>().enabled = false;
            Debug.Log(this.gameObject.name + " is talking to special customer");
            specialCustomer = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "SpecialCustomer")
        {
            //other.gameObject.GetComponent<SphereCollider>().enabled = true;
        }
    }

    public void PossesUnit()
    {
        
            isClicked = true;
        // this.gameObject.GetComponent<Renderer>().material = Green;
        //  playerMesh.GetComponent<Renderer>().material = Green;
        this.GetComponent<Outline>().enabled = true;
        if (rtsMover.ActiveUnit != null)
            {
                rtsMover.ActiveUnit.GetComponent<UnitHighlight>().isClicked = false;
            // rtsMover.ActiveUnit.GetComponent<Renderer>().material = Red;
            // playerMesh.GetComponent<Renderer>().material = Red;
            this.GetComponent<Outline>().enabled = false;
        }
            rtsMover.ActiveUnit = this.gameObject;
            this.GetComponent<NavMeshAgent>().enabled = true;
            this.GetComponent<NavMeshAgent>().isStopped = false;
        
        
    }


    public void TurnRed()
    {
       // this.gameObject.GetComponent<Renderer>().material = Red;
      //  playerMesh.GetComponent<Renderer>().material = Red;
    }
}
