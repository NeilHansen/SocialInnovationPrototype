using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyTruck : MonoBehaviour {
    [HideInInspector]
    public int packages= 0;

    //index 0 drives to and index 1 drives away
    public Transform[] MoveToPoints;
    [SerializeField]
    float CurrentLerpTime = 0;
    [SerializeField]
    float TotalLerpTime = 2;
    public float TruckWaitTime=60;
    [SerializeField]
    ToyDriveGameManager GM;
    [SerializeField]
    float PackageCapacity=4;


	// Use this for initialization
	void Start () {
        StartCoroutine(MoveTruck(false));
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddPackage();
        }
	}



    public void AddPackage()
    {
        packages++;
        Debug.Log("The package number is" + packages);
        if (packages >0)
        {
            StartCoroutine(MoveTruck(true));
            //Add to game score
            GM.AddScore();
           
        }

    }


    public void Drive()
    {
        //When progress bar runs out tally up score and drive away
        StartCoroutine(MoveTruck(true));
    }



    public void LerpTruck(bool leaving)
    {
        if (leaving)
        {
            CurrentLerpTime += Time.deltaTime;
            if (CurrentLerpTime > TotalLerpTime)
            {
                CurrentLerpTime = TotalLerpTime;
            }

            float perc = CurrentLerpTime / TotalLerpTime;

            Debug.Log(MoveToPoints[0].transform);
            
           gameObject.transform.position= Vector3.Lerp (new Vector3 (MoveToPoints[0].position.x,transform.position.y, MoveToPoints[0].position.z), new Vector3( MoveToPoints[1].position.x, transform.position.y, MoveToPoints[1].position.z), perc);

        }


        else
        {
            CurrentLerpTime += Time.deltaTime;
            if (CurrentLerpTime > TotalLerpTime)
            {
                CurrentLerpTime = TotalLerpTime;
            }
          
            float perc = CurrentLerpTime / TotalLerpTime;

            Debug.Log(MoveToPoints[0].transform.position);
            gameObject. transform.position = Vector3.Lerp(new Vector3(MoveToPoints[1].position.x, transform.position.y, MoveToPoints[1].position.z), new Vector3(MoveToPoints[0].position.x, transform.position.y, MoveToPoints[0].position.z), perc);
        }
    }






    IEnumerator MoveTruck(bool leaving)
    {

        if (leaving)
        {
            
            GM.hasTruck = false;

            while (transform.position.x < MoveToPoints[1].position.x)
            {
                LerpTruck(true);
                yield return new WaitForFixedUpdate();
            }

            CurrentLerpTime = 0;
            yield return new WaitForSeconds(1.5f);
            packages = 0;
            while (transform.position.x > MoveToPoints[0].position.x)
            {

                //DriveTruck In
                LerpTruck(false);
                yield return new WaitForFixedUpdate();
            }

            GM.StartNewTruck();
            //Reset Progress meter
        }


        else
        {
            while (transform.position.x > MoveToPoints[0].position.x)
            {
                //DriveTruck In
                Debug.Log("Lerp Truck");
                LerpTruck(false);
                yield return new WaitForFixedUpdate();
            }
            CurrentLerpTime = 0;
            GM.StartNewTruck();

        }










        yield break;
    }
}
