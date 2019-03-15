using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePath : MonoBehaviour {

    //public bool isDebug = true;
    public float Radius = 1.0f;
    //public Vector3[] pointA;
    public GameObject[] locations;

    public float Length
    {
        get
        {
            return locations.Length;
        }
    }

    public Vector3 GetPoint(int index)
    {
        return locations[index].transform.position;
    }

    //void Update()
    //{
    //    for (int i = 0; i < locations.Length; i++)
    //    {
    //        locations[i] = new GameObject("path" + i);
    //        locations[i].AddComponent<Collider>();
    //    }
    //}

    //void OnDrawGizmos()
    //{
    //    if (!isDebug)
    //        return;

    //    for (int i = 0; i < pointA.Length; i++)
    //    {
    //        if (i + 1 < pointA.Length)
    //        {
    //            Debug.DrawLine(pointA[i], pointA[i + 1], Color.cyan);
    //        }
    //    }
    //}

    //void OnDrawGizmos()
    //{ 
    //    if (!isDebug)
    //        return;
    //    for (int i = 0; i < locations.Length; i++)
    //    {
    //        //locations[i] = new GameObject("path");
    //        //Gizmos.DrawSphere(locations[i].transform.position, 1);
    //        Debug.Log(locations.Length);
    //    }
    //}
}
