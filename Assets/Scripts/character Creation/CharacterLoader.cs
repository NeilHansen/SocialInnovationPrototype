using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLoader : MonoBehaviour {



    private JSONPlayerSaver JSONSave;

    public int head;
    public int body;

    //player heads
    public GameObject head1;
    public GameObject head2;
    public GameObject head3;
    public GameObject head4;
    public GameObject head5;
    public GameObject head6;
    public GameObject head7;

    //player bods
    public GameObject body1;
    public GameObject body2;

    // Use this for initialization
    void Start () {
        JSONSave = FindObjectOfType<JSONPlayerSaver>();
    }
	
	// Update is called once per frame
	void Update () {
        head = JSONSave.LoadData(JSONSave.dataPath).playerHead;
        body = JSONSave.LoadData(JSONSave.dataPath).playerBody;

        switch(head)
        {
            case 0:
                head1.SetActive(true);
                head2.SetActive(false);
                head3.SetActive(false);
                head4.SetActive(false);
                head5.SetActive(false);
                head6.SetActive(false);
                head7.SetActive(false);
                break;
            case 1:
                head1.SetActive(false);
                head2.SetActive(true);
                head3.SetActive(false);
                head4.SetActive(false);
                head5.SetActive(false);
                head6.SetActive(false);
                head7.SetActive(false);
                break;
            case 2:
                head1.SetActive(false);
                head2.SetActive(false);
                head3.SetActive(true);
                head4.SetActive(false);
                head5.SetActive(false);
                head6.SetActive(false);
                head7.SetActive(false);
                break;
            case 3:
                head1.SetActive(false);
                head2.SetActive(false);
                head3.SetActive(false);
                head4.SetActive(true);
                head5.SetActive(false);
                head6.SetActive(false);
                head7.SetActive(false);
                break;
            case 4:
                head1.SetActive(false);
                head2.SetActive(false);
                head3.SetActive(false);
                head4.SetActive(false);
                head5.SetActive(true);
                head6.SetActive(false);
                head7.SetActive(false);
                break;
            case 5:
                head1.SetActive(false);
                head2.SetActive(false);
                head3.SetActive(false);
                head4.SetActive(false);
                head5.SetActive(false);
                head6.SetActive(true);
                head7.SetActive(false);
                break;
            case 6:
                head1.SetActive(false);
                head2.SetActive(false);
                head3.SetActive(false);
                head4.SetActive(false);
                head5.SetActive(false);
                head6.SetActive(false);
                head7.SetActive(true);
                break;
        }

        switch (body)
        {
            case 0:
                body1.SetActive(true);
                body2.SetActive(false);
                this.GetComponent<UnitHighlight>().playerMesh = body1;
                break;
            case 1:
                body1.SetActive(false);
                body2.SetActive(true);
                this.GetComponent<UnitHighlight>().playerMesh = body2;
                break;
        }

    }
}
