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
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EndGame()
    {
        int StarAmount = 3;
       

        if (GM.MaxTime/GM.timeValue>=.70f )
        {
            StarAmount = 3;
        }

        else if (GM.MaxTime / GM.timeValue >= .245f)
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
        SceneManager.LoadScene(1);
    }

}
