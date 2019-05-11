using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IBodyPart
{
    void ReceiveInput(Vector2 input);
}
public class BodyPart : MonoBehaviour, IBodyPart
{
    public enum facing { Front,Side,Back}
    public facing currentFacing = facing.Front;
    public Transform Front;
    public Transform Side;
    public Transform Back;
    public Animator FrontAnim;
    public Animator SideAnim;
    public Animator BackAnim;
    public bool walkingForward;
    public bool walkingSide;
    public bool walkingBack;

    float h = 0;
    float v = 0;
    [ContextMenu("setup References")]
    public void setupReferences()
    {
        Front = transform.Find("Front");
        Side = transform.Find("Side");
        Back = transform.Find("Back");
    }

    [ContextMenu("Apply Facing")]
    public void ApplyFacing()
    {
        switch (currentFacing)
        {
            case facing.Front:
                Front.gameObject.SetActive(true);
                Side.gameObject.SetActive(false);
                Back.gameObject.SetActive(false);
               
                if (FrontAnim != null)
                {
                    FrontAnim.SetBool("WalkingForward", walkingForward);

                }
                break;
            case facing.Side:
                Front.gameObject.SetActive(false);
                Side.gameObject.SetActive(true);
                Back.gameObject.SetActive(false);
                if (SideAnim != null)
                {
                    SideAnim.SetBool("WalkingSide", walkingSide);
                }
                break;
            case facing.Back:
                Front.gameObject.SetActive(false);
                Side.gameObject.SetActive(false);
                Back.gameObject.SetActive(true);
                if(BackAnim != null)
                {
                    BackAnim.SetBool("WalkingBack", walkingBack);

                }
                break;
        }

    }
   
    public void ReceiveInput (Vector2 input)
    {
         h = input.x;
         v = input.y;
    }
    public void Update()
    {
        
        if (v < 0)
        {
            currentFacing = facing.Front;
            walkingForward = true;
            walkingBack = false;
            walkingSide = false;
            ApplyFacing();
        }
        else if (v > 0)
        {
            currentFacing = facing.Back;
            walkingForward = false;
            walkingBack = true;
            walkingSide = false;
            ApplyFacing();
        }
        else if(h > 0)
        {
            currentFacing = facing.Side;
            Side.localScale = Vector3.one;
            walkingForward = false;
            walkingBack = false;
            walkingSide = true;
            ApplyFacing();
        }
        else if (h < 0)
        {

            currentFacing = facing.Side;
            Side.localScale = new Vector3(-1, 1, 1);
            walkingForward = false;
            walkingBack = false;
            walkingSide = true;
            ApplyFacing();
        }
        else
        {
            currentFacing = facing.Front;

            walkingForward = false;
            walkingBack = false;
            walkingSide = false;
            ApplyFacing();

            if (FrontAnim != null && FrontAnim.isActiveAndEnabled)
            {
                FrontAnim.SetBool("WalkingForward", walkingForward);
            }
            else if (SideAnim != null && SideAnim.isActiveAndEnabled)
            {
                SideAnim.SetBool("WalkingSide", walkingSide);
            }
            else if (BackAnim != null && BackAnim.isActiveAndEnabled)
            {
                BackAnim.SetBool("WalkingBack", walkingBack);
            }
        }
    }
}
