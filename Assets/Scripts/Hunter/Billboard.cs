using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

    Camera RefCam;
    public enum Axis { up,down,left,right,forward,back};
    public bool reverseFace = false;
    public Axis axis = Axis.up;

    public Vector3 Getaxis (Axis refaxis)
    {
        switch (refaxis)
        {
            case Axis.up:
                return Vector3.up;
                
            case Axis.down:
                return Vector3.down;
                
            case Axis.left:
                return Vector3.left;
                
            case Axis.right:
                return Vector3.right;
                
            case Axis.forward:
                return Vector3.forward;
                
            case Axis.back:
                return Vector3.back;
                
        }

        return Vector3.up;
    }


    private void Awake()
    {
        if (!RefCam)
        {
            RefCam = Camera.main;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {

        Vector3 TargetPos = transform.position + RefCam.transform.rotation * (reverseFace ? Vector3.forward : Vector3.back);
        Vector3 TargetOrientation = RefCam.transform.rotation * Getaxis(axis);
        transform.LookAt(TargetPos, TargetOrientation);
         
		
	}
}
