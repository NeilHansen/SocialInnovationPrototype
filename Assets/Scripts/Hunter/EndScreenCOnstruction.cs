using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreenCOnstruction : MonoBehaviour {
    [SerializeField]
    Image[] Stars;
    [SerializeField]
    GameManagerConstruction GM;



    // Use this for initialization
    void Start () {
        EndGame();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EndGame()
    {
        int StarAmount = 3;
        Debug.Log("The time value is" + GM.timeValue / GM.MaxTime);

        if (GM.timeValue/GM.MaxTime>=.70f )
        {
            StarAmount = 3;
        }

        else if (GM.timeValue / GM.MaxTime >= .245f)
        {
            StarAmount = 2;
        }

        else
        {
            StarAmount = 1;
        }

        for (int i = 0; i < StarAmount; i++)
        {
            Stars[i].enabled = true;
        }
    }


    public void RestartGame()
    {
        SceneManager.LoadScene(2);
    }
    public void LoadDorm()
    {
        SceneManager.LoadScene(0);
    }

}
