using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RtsMover : MonoBehaviour {
    public GameObject Unit1;
    public GameObject Unit2;
    public GameObject Unit3;
    public GameObject Unit4;
    public GameObject ActiveUnit;
    //Moveing Wood
    public GameObject BigWood;
    public GameObject BigPipe;

    [SerializeField]
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
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(new Vector3(originalPosition.x, 0, originalPosition.z), 1.0f);
    }

    private void MouseClick()
    {
        if (ActiveUnit != null)
        {
           

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000f  ,mask))
            {
                
                originalPosition = hit.point;
                Debug.DrawRay(ray.origin, ray.direction * 1000.0f, Color.green);
                Debug.Log("HIIIIT" + hit.collider.name);
            }
            else
            {
                Debug.Log("not");
                Debug.DrawRay(ray.origin, ray.direction * 1000.0f, Color.red);
            }



           MovePlayer(originalPosition);
            
          
        }
    }

  

    public void MovePlayer(Transform newPos)
    {
       // ActiveUnit.GetComponent<NavMeshAgent>().SetDestination(newPos.position);
        MovePlayer(newPos.position);
//Debug.Log("Fuck");
    }

    public void MovePlayer(Vector3 newPos)
    {
        if (newPos != null)
        {
            ActiveUnit.GetComponent<NavMeshAgent>().SetDestination(newPos);
        }
      

        //to get rid of the navmaesh adgent rotation

       // ActiveUnit.GetComponent<NavMeshAgent>().updateRotation = false;
       // ActiveUnit = null;
       //  Debug.Log("Fuck");
    }



    void FollowLeader()
    {
        //Variable for follower
        UnitTaskController Follower = ActiveUnit.GetComponent<UnitTaskController>().companion.GetComponent<UnitTaskController>();

         
    }

    public void MoveToWoodPile()
    {
        
    }
  
    


}
