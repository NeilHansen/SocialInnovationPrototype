using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLinks : MonoBehaviour {

    // Use this for initialization

    public string link;
    public void OpenChannel()
    {
        Application.OpenURL(link);
    }
}
