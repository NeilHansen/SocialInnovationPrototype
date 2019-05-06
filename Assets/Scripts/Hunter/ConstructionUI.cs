using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConstructionUI : MonoBehaviour {

    [SerializeField]
    Image House;
    [SerializeField]
    Sprite[] HouseImages;
    [SerializeField]
    TextMeshProUGUI ProgressText;
   




    // Use this for initialization
    void Start () {
        AddToHouse(0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    public void AddToHouse(int Score)
    {

        switch (Score)
        {

            case 0:
                //House.sprite = HouseImages[0];
                ProgressText.text = "House Progress: " + Score + "/3";
                break;


            case 1:
               
               // House.sprite = HouseImages[1];
                ProgressText.text = "House Progress: " + Score + "/3";

                break;

            case 2:
               
                //House.sprite = HouseImages[2];
                ProgressText.text = "House Progress: " + Score + "/3";

                break;

            case 3:
              
               // House.sprite = HouseImages[3];
                ProgressText.text = "House Progress: " + Score + "/3";
                break;

            case 4:
              
               // House.sprite = HouseImages[4];
                ProgressText.text = "House Progress: " + Score + "/3";
                break;

        }

    }
}
