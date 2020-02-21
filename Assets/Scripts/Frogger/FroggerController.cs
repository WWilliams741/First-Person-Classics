﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FroggerController : MonoBehaviour
{

    public GameObject frog;
    public GameObject frogsBody;
    [SerializeField] Camera eyes;
    [SerializeField] Transform trans;
    [SerializeField] Animator anim;
    private Vector3 StartLocation;

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
        StartLocation = new Vector3(trans.position.x, trans.position.y, trans.position.z );
    }

    void Update()
    {
        float forward = Input.GetAxisRaw("Vertical");
        float side = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Frog is about to move forward");
            Jump();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("Frog is about to move right");
            TurnRight();
            //Idle();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Frog is about to move left");
            TurnLeft();
            //Idle();
        }
        else
        {
            Idle();
        }
        
    }

    void OnAnimatorMove()
    {

        if (anim)
        {
            Vector3 newPosition = transform.position;
            newPosition.z += anim.GetFloat("Jump Speed") * Time.deltaTime;
            transform.position = newPosition;
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
        if (!anim.applyRootMotion)
        {
            anim.applyRootMotion = true;
        }
    }

    private void OnTriggerEnter(Collider Collider) {
        //Debug.Log(Collider.gameObject.name);
        if (Collider.gameObject.layer == 10) {
            trans.position = StartLocation;
            Debug.Log("Hit by car");
        }

    }

}