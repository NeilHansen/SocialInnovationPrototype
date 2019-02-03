using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RtsMover : MonoBehaviour {
    public GameObject Unit1;
    public GameObject Unit2;
    public GameObject ActiveUnit;

    Vector3 originalPosition;


    public LayerMask mask;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(0))
        {
            MouseClick();
        }
        //Posses unit on number key presses
        
        //if (Input.GetKeyDown("1"))
        //{
        //    if (ActiveUnit == Unit2 || ActiveUnit == null)
        //    {
        //        Unit1.gameObject.GetComponent<UnitHighlight>().PossesUnit();
        //    }
        //    else
        //    {

        //    }
        //}

        //if (Input.GetKeyDown("2"))
        //{
        //    if (ActiveUnit == Unit1 || ActiveUnit == null)
        //    {
        //        Unit2.gameObject.GetComponent<UnitHighlight>().PossesUnit();
        //    }
        //    else
        //    {

        //    }
        //}


    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(new Vector3(originalPosition.x, 0, originalPosition.z), 1.0f);
    }

    private void MouseClick()
    {
        if (ActiveUnit != null)
        {
           
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, mask))
            {
               // if (hit.collider.gameObject.tag == "Floor")
             //   {
                    originalPosition = hit.point;
              //  }
            }
            
            //originalPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MovePlayer(new Vector3(originalPosition.x, 0,originalPosition.z));
        }
    }

    void MovePlayer(Vector3 newPos)
    {
        ActiveUnit.GetComponent<NavMeshAgent>().SetDestination(newPos);
    }

    

    
}
