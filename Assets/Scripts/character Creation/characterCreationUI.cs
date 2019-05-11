using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class characterCreationUI : MonoBehaviour
{
    public GameObject[] heads;
   // public Image head;
    public int currentHead;

   
	// Use this for initialization
	void Start ()
    {
		
	}
    public void setPart(int index)
    {
        heads[currentHead].SetActive(false);
        if(index >0 && index < heads.Length)
        {
            currentHead = index;
        }
        heads[currentHead].SetActive(true);

    }
    public void changeHeadLeft()
    {
        heads[currentHead].SetActive(false);

        currentHead--;
        if(currentHead < 0)
        {
            currentHead = heads.Length -1;
        }
      heads[currentHead].SetActive(true);
    }
    public void changeHeadRight()
    {
        heads[currentHead].SetActive(false);

        currentHead++;
        if (currentHead >= heads.Length)
        {
            currentHead = 0;
        }
       // Debug.Log(currentHead);
        heads[currentHead].SetActive(true);

    }
    // Update is called once per frame
    void Update ()
    {
		
	}
}
