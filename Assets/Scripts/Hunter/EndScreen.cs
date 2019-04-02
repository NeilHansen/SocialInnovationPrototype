using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour {
    [SerializeField]
    Image[] Stars;
    [SerializeField]
    Text ScoreDisplay;
    [SerializeField]
    GameManager GM;



    // Use this for initialization
    void Start () {
        EndGame();
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    public void EndGame()
    {
        int StarAmount=3;
        ScoreDisplay.text = Mathf.RoundToInt(GM.playerScore).ToString();
        //Debug.Log("EndGameScreen" + GM.playerScore);
        if (GM.playerScore >= 299)
        {
            StarAmount = 3;
        }

        else if (GM.playerScore >= 200)
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
        SceneManager.LoadScene(0);
    }

   

}
