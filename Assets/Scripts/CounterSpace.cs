using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterSpace : MonoBehaviour {

    public GameObject dirtyPlate;
    public GameObject cleanPlate;
    public GameObject filledPlate;
    public GameObject rawFood;

    // Use this for initialization
    void Start () {
        dirtyPlate.gameObject.SetActive(false);
        cleanPlate.gameObject.SetActive(true);
        filledPlate.gameObject.SetActive(false);
        rawFood.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void ObjectNone()
    {
        dirtyPlate.gameObject.SetActive(false);
        cleanPlate.gameObject.SetActive(false);
        filledPlate.gameObject.SetActive(false);
        rawFood.gameObject.SetActive(false);
    }

    public void ObjectDirtyPlate()
    {
        dirtyPlate.gameObject.SetActive(true);
        cleanPlate.gameObject.SetActive(false);
        filledPlate.gameObject.SetActive(false);
        rawFood.gameObject.SetActive(false);
    }

    public void ObjectCleanPlate()
    {
        dirtyPlate.gameObject.SetActive(false);
        cleanPlate.gameObject.SetActive(true);
        filledPlate.gameObject.SetActive(false);
        rawFood.gameObject.SetActive(false);
    }

    public void ObjectFilledPlate()
    {
        dirtyPlate.gameObject.SetActive(false);
        cleanPlate.gameObject.SetActive(false);
        filledPlate.gameObject.SetActive(true);
        rawFood.gameObject.SetActive(false);
    }

    public void ObjectRawFood()
    {
        dirtyPlate.gameObject.SetActive(false);
        cleanPlate.gameObject.SetActive(false);
        filledPlate.gameObject.SetActive(false);
        rawFood.gameObject.SetActive(true);
    }
}
