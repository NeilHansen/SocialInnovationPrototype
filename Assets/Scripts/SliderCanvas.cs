using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderCanvas : MonoBehaviour {

    public GameObject player;
    public Button positiveButton;
    public Button negativeButton;
 

    private Vector3 offset;
    private GameObject specialCustomer;

	// Use this for initialization
	void Start () {
        offset = transform.position - player.transform.position;

        positiveButton.onClick.AddListener(PositiveRespond);
        negativeButton.onClick.AddListener(NegativeRespond);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = player.transform.position + offset;

        specialCustomer = GameObject.FindGameObjectWithTag("SpecialCustomer");

        if(Input.GetKeyDown(KeyCode.Space))
        {
            positiveButton.enabled = true;
            negativeButton.enabled = true;
        }
    }

    void PositiveRespond()
    {
        Debug.Log(specialCustomer.GetComponent<Customer>().sprite[0].name);
        specialCustomer.GetComponent<Customer>().PositiveRespond();
    }

    void NegativeRespond()
    {
        specialCustomer.GetComponent<Customer>().status.sprite = specialCustomer.GetComponent<Customer>().sprite[1];
    }

    
}
