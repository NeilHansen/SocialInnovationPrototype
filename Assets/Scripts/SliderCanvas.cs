using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderCanvas : MonoBehaviour {

    
    public Button leftButton;
    public Button rightButton;
    public GameObject player;

    private Vector3 offset;
    private GameObject specialCustomer;
    public Quaternion RotationToKeep;

	// Use this for initialization
	void Start () {
        offset = transform.position - player.transform.position;

		leftButton.gameObject.SetActive(false);
		rightButton.gameObject.SetActive(false);
        RotationToKeep = player.transform.rotation;

        //positiveButton.onClick.AddListener(PositiveRespond);
        //negativeButton.onClick.AddListener(NegativeRespond);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = player.transform.position + offset;

        specialCustomer = GameObject.FindGameObjectWithTag("SpecialCustomer");
        player.transform.rotation = RotationToKeep;

        //if(Vector3.Distance(specialCustomer.transform.position, transform.position) < 5.0f)
        //{
        //    positiveButton.gameObject.SetActive(true);
        //    negativeButton.gameObject.SetActive(true);
        //}
    }

    void PositiveRespond()
    {
        specialCustomer.GetComponent<Customer>().PositiveRespond();
    }

    void NegativeRespond()
    {
        specialCustomer.GetComponent<Customer>().NegativeRespond();
    }
}
