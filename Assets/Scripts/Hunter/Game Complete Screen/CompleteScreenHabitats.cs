using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CompleteScreenHabitats : MonoBehaviour {

    [SerializeField]
    GameManagerConstruction GM;

    [SerializeField]
    Image[] Stars;
   
    [SerializeField]
    TextMeshProUGUI ScoreText;

    public Sprite FilledStar;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void RoundComplete()
    {
        int StarAmount = 3;

        //Debug.Log("EndGameScreen" + GM.playerScore);
        if (GM.playerScore >= 299)
        {
            StarAmount = 3;
        }

        else if (GM.playerScore >= 200)
        {
            StarAmount = 2;
        }

        else if (GM.playerScore > 0)
        {
            StarAmount = 1;
        }

        else
        {
            StarAmount = 0;
        }

        for (int i = 0; i < StarAmount; i++)
        {
            Stars[i].sprite = FilledStar;
        }
        ScoreText.text = "Score:" + GM.playerScore;




    }
    //YUK
    public void GoToQuiz()
    {
        FindObjectOfType<GameManagerConstruction>().QuizScreen();
        gameObject.SetActive(false);
    }


    


}
