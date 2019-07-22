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
    public GameObject head8;
    public GameObject head9;
    public GameObject head10;
    public GameObject head11;


    //player bods
    public GameObject body1;
    public GameObject body2;

    // Use this for initialization
    void Start () {
        JSONSave = FindObjectOfType<JSONPlayerSaver>();
        head = PlayerPrefs.GetInt("head");
        body = PlayerPrefs.GetInt("body");
    }
	
	// Update is called once per frame
	void Update () {
        // head = JSONSave.LoadData(JSONSave.dataPath).playerHead;
        // body = JSONSave.LoadData(JSONSave.dataPath).playerBody;.
        head = PlayerPrefs.GetInt("head");
        body = PlayerPrefs.GetInt("body");

        switch (head)
        {
            case 0:
                head1.SetActive(true);
                head2.SetActive(false);
                head3.SetActive(false);
                head4.SetActive(false);
                head5.SetActive(false);
                head6.SetActive(false);
                head7.SetActive(false);
                head8.SetActive(false);
                head9.SetActive(false);
                head10.SetActive(false);
                head11.SetActive(false);
                break;
            case 1:
                head1.SetActive(false);
                head2.SetActive(true);
                head3.SetActive(false);
                head4.SetActive(false);
                head5.SetActive(false);
                head6.SetActive(false);
                head7.SetActive(false);
                head8.SetActive(false);
                head9.SetActive(false);
                head10.SetActive(false);
                head11.SetActive(false);
                break;
            case 2:
                head1.SetActive(false);
                head2.SetActive(false);
                head3.SetActive(true);
                head4.SetActive(false);
                head5.SetActive(false);
                head6.SetActive(false);
                head7.SetActive(false);
                head8.SetActive(false);
                head9.SetActive(false);
                head10.SetActive(false);
                head11.SetActive(false);
                break;
            case 3:
                head1.SetActive(false);
                head2.SetActive(false);
                head3.SetActive(false);
                head4.SetActive(true);
                head5.SetActive(false);
                head6.SetActive(false);
                head7.SetActive(false);
                head8.SetActive(false);
                head9.SetActive(false);
                head10.SetActive(false);
                head11.SetActive(false);
                break;
            case 4:
                head1.SetActive(false);
                head2.SetActive(false);
                head3.SetActive(false);
                head4.SetActive(false);
                head5.SetActive(true);
                head6.SetActive(false);
                head7.SetActive(false);
                head8.SetActive(false);
                head9.SetActive(false);
                head10.SetActive(false);
                head11.SetActive(false);
                break;
            case 5:
                head1.SetActive(false);
                head2.SetActive(false);
                head3.SetActive(false);
                head4.SetActive(false);
                head5.SetActive(false);
                head6.SetActive(true);
                head7.SetActive(false);
                head8.SetActive(false);
                head9.SetActive(false);
                head10.SetActive(false);
                head11.SetActive(false);
                break;
            case 6:
                head1.SetActive(false);
                head2.SetActive(false);
                head3.SetActive(false);
                head4.SetActive(false);
                head5.SetActive(false);
                head6.SetActive(false);
                head7.SetActive(true);
                head8.SetActive(false);
                head9.SetActive(false);
                head10.SetActive(false);
                head11.SetActive(false);
                break;
            case 7:
                head1.SetActive(false);
                head2.SetActive(false);
                head3.SetActive(false);
                head4.SetActive(false);
                head5.SetActive(false);
                head6.SetActive(false);
                head7.SetActive(false);
                head8.SetActive(true);
                head9.SetActive(false);
                head10.SetActive(false);
                head11.SetActive(false);
                break;
            case 8:
                head1.SetActive(false);
                head2.SetActive(false);
                head3.SetActive(false);
                head4.SetActive(false);
                head5.SetActive(false);
                head6.SetActive(false);
                head7.SetActive(false);
                head8.SetActive(false);
                head9.SetActive(true);
                head10.SetActive(false);
                head11.SetActive(false);
                break;
            case 9:
                head1.SetActive(false);
                head2.SetActive(false);
                head3.SetActive(false);
                head4.SetActive(false);
                head5.SetActive(false);
                head6.SetActive(false);
                head7.SetActive(false);
                head8.SetActive(false);
                head9.SetActive(false);
                head10.SetActive(true);
                head11.SetActive(false);
                break;
            case 10:
                head1.SetActive(false);
                head2.SetActive(false);
                head3.SetActive(false);
                head4.SetActive(false);
                head5.SetActive(false);
                head6.SetActive(false);
                head7.SetActive(false);
                head8.SetActive(false);
                head9.SetActive(false);
                head10.SetActive(false);
                head11.SetActive(true);
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
