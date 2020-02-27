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
    private Transform turtleOrLogPosition;

    //public GameObject guts;
    //[SerializeField] GameObject gutsEx;
    bool smashed;
    bool onTurtleOrLog;
    int turnCounter;
    Direction direction;

    enum Direction
    {
        north,
        east,
        south,
        west
    }

    void Start()
    {
        smashed = false;
        onTurtleOrLog = false;
        direction = Direction.north;
        StartLocation = new Vector3(trans.position.x, trans.position.y, trans.position.z );
    }

    void Update()
    {
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

        if(onTurtleOrLog)
        {
            Debug.Log("Following turtle or log now.");
            followTurtleOrLog();
        }

    }

    void OnAnimatorMove()
    {

        if (anim)
        {
            if (direction == Direction.north)
            {
                Vector3 newPosition = transform.position;
                newPosition.z += anim.GetFloat("Jump Speed") * Time.deltaTime;
                transform.position = newPosition;
            }
            else if (direction == Direction.east)
            {
                Vector3 newPosition = transform.position;
                newPosition.x += anim.GetFloat("Jump Speed") * Time.deltaTime;
                transform.position = newPosition;
            }
            else if (direction == Direction.south)
            {
                Vector3 newPosition = transform.position;
                newPosition.z -= anim.GetFloat("Jump Speed") * Time.deltaTime;
                transform.position = newPosition;
            }
            else if (direction == Direction.west)
            {
                Vector3 newPosition = transform.position;
                newPosition.x -= anim.GetFloat("Jump Speed") * Time.deltaTime;
                transform.position = newPosition;
            }
            else
            {
                Debug.Log("A terrible error has occured and we are facing the void, somehow!");
            }

            Vector3 newRotation = transform.rotation.eulerAngles;
            newRotation.y += anim.GetFloat("Turn Speed") * Time.deltaTime;
            transform.rotation = Quaternion.Euler(newRotation);
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
        if(!onTurtleOrLog)
        {
            transform.position = new Vector3(trans.position.x, trans.position.y + 0.6f, trans.position.z);
        }
        else
        {
            onTurtleOrLog = false;
        }
        anim.SetTrigger("Jump");
    }

    public void finishJump(float value)
    {
        if(onTurtleOrLog)
        {
            transform.position = new Vector3(turtleOrLogPosition.position.x, trans.position.y, turtleOrLogPosition.position.z);
        }
        else
        {
            transform.position = new Vector3(trans.position.x, trans.position.y + value, trans.position.z);
        }
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
        turnCounter = (turnCounter + 3) % 4;
        direction = (Direction) turnCounter;
        Debug.Log("frog is now facing " + direction);
        anim.SetTrigger("TurnLeft");
    }

    public void TurnRight()
    {
        //anim.applyRootMotion = true;
        //DestroyGuts();
        turnCounter = (turnCounter + 1) % 4;
        direction = (Direction)turnCounter;
        Debug.Log("frog is now facing " + direction);
        anim.SetTrigger("TurnRight");
    }

    void RootMotion()
    {
        if (!anim.applyRootMotion)
        {
            anim.applyRootMotion = true;
        }
    }

    private void followTurtleOrLog()
    {
        transform.position = new Vector3(turtleOrLogPosition.position.x, trans.position.y, turtleOrLogPosition.position.z);
    }

    private void OnTriggerEnter(Collider Collider) {
        //Debug.Log(Collider.gameObject.name);
        if (Collider.gameObject.layer == 10)
        {
            trans.position = StartLocation;
            Debug.Log("Hit by car");
        }
        else if (Collider.gameObject.layer == 13)
        {
            trans.position = StartLocation;
            Debug.Log("Fell into the water");
        }
        else if (Collider.gameObject.layer == 14)
        {
            onTurtleOrLog = true;
            turtleOrLogPosition = Collider.gameObject.transform;
            Debug.Log("Going on top of a turtle/log");
        }
        else if (Collider.gameObject.layer == 15)
        {
            //add score - possibly tell gameManager:

            Collider.gameObject.SetActive(false);
            //change gameobject to a goalFrog:
            if (gameObject.name.EndsWith("1"))
            {
                
            }
            else if (gameObject.name.EndsWith("2"))
            {

            }
            else if (gameObject.name.EndsWith("3"))
            {
                
            }
            else if (gameObject.name.EndsWith("4"))
            {
                
            }
            else if (gameObject.name.EndsWith("5"))
            {
                
            }
            else
            {
                Debug.Log("You have entered the void, and are enabling/disabling some strange things my dude.");
            }
            Debug.Log("We reached a butterfly, GOAL!!!!!");
        }
    }

}
