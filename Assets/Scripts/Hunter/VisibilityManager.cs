using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class VisibilityManager : MonoBehaviour {

    public GameObject visibleMesh;
    
    public Vector3 OrginalPosition;

    public List<GameObject> Heavycarriers;

    // Use this for initialization
    void Start () {
        OrginalPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void TurnonObject()
    {
        GetComponent<BoxCollider>().enabled = true;
        Debug.Log(visibleMesh.name);
        visibleMesh.SetActive(true);
    }

    public void TurnoffObject()
    {
        GetComponent<BoxCollider>().enabled = false;
        visibleMesh.SetActive(false);
        GetComponent<NavMeshAgent>().enabled = false;
    }
}
