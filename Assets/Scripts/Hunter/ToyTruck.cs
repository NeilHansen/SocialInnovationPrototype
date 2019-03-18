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

    [SerializeField]

    public float DriveSpeed;

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
        if (packages >= 4)
        {
            StartCoroutine(MoveTruck(true));
            //Add to game score
            packages = 0;
           
        }

    }


    public void TallyUpPackages()
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

            transform.position= Vector3.Lerp (new Vector3 (MoveToPoints[0].position.x,transform.position.y, MoveToPoints[0].position.z), new Vector3( MoveToPoints[1].position.x, transform.position.y, MoveToPoints[1].position.z), perc);

        }


        else
        {
            CurrentLerpTime += Time.deltaTime;
            if (CurrentLerpTime > TotalLerpTime)
            {
                CurrentLerpTime = TotalLerpTime;
            }

            float perc = CurrentLerpTime / TotalLerpTime;


            transform.position = Vector3.Lerp(new Vector3(MoveToPoints[1].position.x, transform.position.y, MoveToPoints[1].position.z), new Vector3(MoveToPoints[0].position.x, transform.position.y, MoveToPoints[0].position.z), perc);
        }
    }






    IEnumerator MoveTruck(bool leaving)
    {
        if (leaving)
        {
            while (transform.position.x < MoveToPoints[1].position.x)
            {
                LerpTruck(true);
                yield return new WaitForFixedUpdate();
            }

            CurrentLerpTime = 0;
            yield return new WaitForSeconds(1.5f);

            while (transform.position.x > MoveToPoints[0].position.x)
            {
                //DriveTruck In
                LerpTruck(false);
                yield return new WaitForFixedUpdate();
            }

            //Reset Progress meter
        }


        else
        {
            while (transform.position.x > MoveToPoints[0].position.x)
            {
                //DriveTruck In
                LerpTruck(false);
                yield return new WaitForFixedUpdate();
            }
            CurrentLerpTime = 0;
        }










        yield break;
    }
}
