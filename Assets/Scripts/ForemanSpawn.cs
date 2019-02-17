using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForemanSpawn : MonoBehaviour {

    public GameObject foremanPrefab;
    public GameObject spawnPoint;
    public GameManager gameManager;

    private GameObject[] foremen;
    private float spawnTime = 2.0f;
    private float timeElapsed = 0.0f;
    //private int maxForeman = 1;
    //private int customerCount;

    // Use this for initialization
    void Start()
    {
        Instantiate(foremanPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        //customerCount = 1;
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        foremen = GameObject.FindGameObjectsWithTag("Foreman");
        
        if (foremen.Length < 1)
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
        Instantiate(foremanPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
    }
}
