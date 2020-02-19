using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FroggerController : MonoBehaviour
{

    public GameObject frog;
    public GameObject frogsBody;
    [SerializeField] SkinnedMeshRenderer skinnedMeshRenderer;

    [SerializeField] Animator anim;

    //public GameObject guts;
    //[SerializeField] GameObject gutsEx;
    bool smashed = false;
    Direction direction;

    enum Direction
    {
        left,
        right,
        up,
        down
    }

    void Start()
    {
        direction = Direction.up;
    }

    void Update()
    {
        float forward = Input.GetAxisRaw("Vertical");
        float side = Input.GetAxisRaw("Horizontal");
        if (forward > 0)
        {
            Debug.Log("Frog is about to move forward");
            Jump();
        }
        else if (side > 0)
        {
            Debug.Log("Frog is about to move right");
            TurnRight();
        }
        else if (side < 0)
        {
            Debug.Log("Frog is about to move left");
            TurnLeft();
        }
        
    }

    public void Idle()
    {
        RootMotion();
        //DestroyGuts();
        anim.SetTrigger("Idle");
    }

    public void Jump()
    {
        RootMotion();
        //DestroyGuts();
        anim.SetTrigger("Jump");
    }

    public void Crawl()
    {
        RootMotion();
        //DestroyGuts();
        anim.SetTrigger("Crawl");
    }

    public void Tongue()
    {
        RootMotion();
        //DestroyGuts();
        anim.SetTrigger("Tongue");
    }

    public void Swim()
    {
        RootMotion();
        //DestroyGuts();
        anim.SetTrigger("Swim");
    }

    public void Smashed()
    {
        RootMotion();
        //DestroyGuts();
        anim.SetTrigger("Smashed");
        //Guts();
    }

    public void TurnLeft()
    {
        //anim.applyRootMotion = true;
        //DestroyGuts();
        anim.SetTrigger("TurnLeft");
    }

    public void TurnRight()
    {
        //anim.applyRootMotion = true;
        //DestroyGuts();
        anim.SetTrigger("TurnRight");
    }

    void RootMotion()
    {
        if (anim.applyRootMotion)
        {
            anim.applyRootMotion = true;
        }
    }

}