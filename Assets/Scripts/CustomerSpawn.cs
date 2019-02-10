using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawn : MonoBehaviour {

    public GameObject customerPrefab;
    public GameObject specialCustomerPrefab;
    public GameObject spawnPoint;
    public GameManager gameManager;

    public float specialCustomerScoreSpawn;
    private bool specialCustomerSpawn;
    public float firstSpecialCustomerSpawn = 0.0f;

    private GameObject[] customers, specialCustomers;
    private float spawnTime = 2.0f;
    private float timeElapsed = 0.0f;
    private int maxCustomer = 4;
    private int customerCount;

	// Use this for initialization
	void Start () {
        Instantiate(customerPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        customerCount = 1;
        gameManager = FindObjectOfType<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (gameManager.playerScore >= specialCustomerScoreSpawn)
        {
            specialCustomerSpawn = true;
        }

        if(specialCustomerSpawn && firstSpecialCustomerSpawn == 0)
        {
            Instantiate(specialCustomerPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
            firstSpecialCustomerSpawn += 1.0f;
        }

        customers = GameObject.FindGameObjectsWithTag("Customer");
        specialCustomers = GameObject.FindGameObjectsWithTag("SpecialCustomer");

        if ((customers.Length + specialCustomers.Length) < maxCustomer)
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
        if (specialCustomerSpawn)
        {
            if (customerCount % 3 == 0)
                Instantiate(specialCustomerPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
            else
                Instantiate(customerPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }
        else
            Instantiate(customerPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        customerCount += 1;
    }
}
