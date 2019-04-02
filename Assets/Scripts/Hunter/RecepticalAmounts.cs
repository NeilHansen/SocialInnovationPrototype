using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class RecepticalAmounts : MonoBehaviour {
    [SerializeField]
    TextMeshProUGUI Amount;
    [SerializeField]
    InteractableAreaConstructionSite RecepticalBin;
    [SerializeField]
    bool PipeRecepticalBin=false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (PipeRecepticalBin)
        {
            Amount.text = RecepticalBin.numberOfPipes.ToString()+"/4";
        }

        else
        {
            Amount.text = RecepticalBin.numberOfBoards.ToString()+"/4";
        }
	}
}
