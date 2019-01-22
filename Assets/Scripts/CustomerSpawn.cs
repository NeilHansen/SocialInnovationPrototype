using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawn : MonoBehaviour {

    public GameObject customerPrefab;
    public GameObject specialCustomerPrefab;
    public GameObject spawnPoint;
    private GameObject[] customers;
    private float spawnTime = 2.0f;
    private float timeElapsed = 0.0f;
    private int maxCustomer = 4;
    private int customerCount;

	// Use this for initialization
	void Start () {
        Instantiate(customerPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        customerCount = 1;
    }
	
	// Update is called once per frame
	void Update () {
        customers = GameObject.FindGameObjectsWithTag("Customer");
        
        if(customers.Length < maxCustomer)
        {
            timeElapsed += Time.deltaTime;
        }

        if (timeElapsed >= spawnTime)
        {
            Spawn();
            timeElapsed = 0.0f;
        }
	}

    void Spawn()
    {
        if(customerCount%4 == 0)
            Instantiate(specialCustomerPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        else
            Instantiate(customerPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        customerCount += 1;
    }
}
