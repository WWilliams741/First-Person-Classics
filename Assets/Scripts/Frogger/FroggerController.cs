using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FroggerController : MonoBehaviour {

    public GameObject frog;
    public GameObject frogsBody;
    [SerializeField] Camera eyes;
    [SerializeField] Transform trans;
    [SerializeField] Animator anim;
    private Vector3 StartLocation;
    private Quaternion startRotation;
    private GameObject turtleOrLog;
    [SerializeField] GameManagerScript_Frogger gameController;

    //public GameObject guts;
    //[SerializeField] GameObject gutsEx;
    bool smashed;
    bool moving;
    bool onTurtleOrLog;
    bool isUnderWaterTurtle;
    int turnCounter;
    Direction direction;

    enum Direction {
        north,
        east,
        south,
        west
    }

    void Start() {
        smashed = false;
        moving = false;
        onTurtleOrLog = false;
        direction = Direction.north;
        StartLocation = trans.position;
        startRotation = trans.rotation;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.W) && !moving) {
            Debug.Log("Frog is about to move forward");
            Jump();
        }
        else if (Input.GetKeyDown(KeyCode.D) && !moving) {
            Debug.Log("Frog is about to move right");
            TurnRight();
        }
        else if (Input.GetKeyDown(KeyCode.A) && !moving) {
            Debug.Log("Frog is about to move left");
            TurnLeft();
        }
        else {
            Idle();
        }

        if (onTurtleOrLog) {
            Debug.Log("Following turtle or log now.");
            followTurtleOrLog();
        }

    }

    void OnAnimatorMove() {

        if (anim && !anim.GetBool("Stop")) {
            if (direction == Direction.north) {
                Vector3 newPosition = transform.position;
                newPosition.z += anim.GetFloat("Jump Speed") * Time.deltaTime;
                transform.position = newPosition;
            }
            else if (direction == Direction.east) {
                Vector3 newPosition = transform.position;
                newPosition.x += anim.GetFloat("Jump Speed") * Time.deltaTime;
                transform.position = newPosition;
            }
            else if (direction == Direction.south) {
                Vector3 newPosition = transform.position;
                newPosition.z -= anim.GetFloat("Jump Speed") * Time.deltaTime;
                transform.position = newPosition;
            }
            else if (direction == Direction.west) {
                Vector3 newPosition = transform.position;
                newPosition.x -= anim.GetFloat("Jump Speed") * Time.deltaTime;
                transform.position = newPosition;
            }
            else {
                Debug.Log("A terrible error has occured and we are facing the void, somehow!");
            }

            Vector3 newRotation = transform.rotation.eulerAngles;
            newRotation.y += anim.GetFloat("Turn Speed") * Time.deltaTime;
            transform.rotation = Quaternion.Euler(newRotation);
        }
    }

    public void Idle() {
        RootMotion();
        //DestroyGuts();
        anim.SetTrigger("Idle");
    }

    public void Jump() {
        RootMotion();
        //DestroyGuts();
        if (!onTurtleOrLog) {
            transform.position = new Vector3(trans.position.x, trans.position.y + 0.6f, trans.position.z);
        }
        else {
            onTurtleOrLog = false;
        }
        anim.SetTrigger("Jump");
        moving = true;
        anim.SetBool("Stop", false);
    }

    public void finishJump(float value) {
        if (onTurtleOrLog) {
            transform.position = new Vector3(turtleOrLog.transform.position.x, trans.position.y, turtleOrLog.transform.position.z);
        }
        else {
            transform.position = new Vector3(trans.position.x, trans.position.y + value, trans.position.z);
        }
        moving = false;
    }

    public void Crawl() {
        RootMotion();
        //DestroyGuts();
        anim.SetTrigger("Crawl");
    }

    public void Tongue() {
        RootMotion();
        //DestroyGuts();
        anim.SetTrigger("Tongue");
    }

    public void Swim() {
        RootMotion();
        //DestroyGuts();
        anim.SetTrigger("Swim");
    }

    public void Smashed() {
        RootMotion();
        //DestroyGuts();
        anim.SetTrigger("Smashed");
        //Guts();
    }

    public void TurnLeft() {
        //anim.applyRootMotion = true;
        //DestroyGuts();
        turnCounter = (turnCounter + 3) % 4;
        direction = (Direction)turnCounter;
        Debug.Log("frog is now facing " + direction);
        anim.SetTrigger("TurnLeft");
        moving = true;
        anim.SetBool("Stop", false);
    }

    public void TurnRight() {
        //anim.applyRootMotion = true;
        //DestroyGuts();
        turnCounter = (turnCounter + 1) % 4;
        direction = (Direction)turnCounter;
        Debug.Log("frog is now facing " + direction);
        anim.SetTrigger("TurnRight");
        moving = true;
        anim.SetBool("Stop", false);
    }

    void RootMotion() {
        if (!anim.applyRootMotion) {
            anim.applyRootMotion = true;
        }
    }

    private void followTurtleOrLog() {
        if (isUnderWaterTurtle) {
            if (turtleOrLog.transform.position.y < -2.0f) {
                onTurtleOrLog = false;
                transform.position = new Vector3(transform.position.x, trans.position.y - 0.6f, transform.position.z);
            }
            else {
                transform.position = new Vector3(turtleOrLog.transform.position.x, trans.position.y, turtleOrLog.transform.position.z);
            }
        }
        else {
            transform.position = new Vector3(turtleOrLog.transform.position.x, trans.position.y, turtleOrLog.transform.position.z);
        }
    }

    public void canMove() {
        moving = false;
    }

    private void OnTriggerEnter(Collider Collider) {
        //Debug.Log(Collider.gameObject.name);
        if (Collider.gameObject.layer == 10) {
            death();
            Debug.Log("Hit by car");
        }
        else if (Collider.gameObject.layer == 11)
        {
            death();
            Debug.Log("Hit a wall/border and went out of bounds!");
        }
        else if (Collider.gameObject.layer == 13) {
            death();
            Debug.Log("Fell into the water");
        }
        else if (Collider.gameObject.layer == 14) {
            onTurtleOrLog = true;
            turtleOrLog = Collider.gameObject;
            isUnderWaterTurtle = turtleOrLog.GetComponent<HazardAI>().IsUnderWaterTurtle;
            Debug.Log("Going on top of a turtle/log");
        }
        else if (Collider.gameObject.layer == 15) {
            resetPosition();
            gameController.restartTimer();
            Debug.Log("Touched a butterfly");
        }
    }

    public void death() {
        resetPosition();
        gameController.restartTimer();
        gameController.LoseLife();
    }

    public void resetPosition()
    {
        anim.SetBool("Stop", true);
        trans.position = StartLocation;
        trans.rotation = startRotation;
        canMove();
        direction = Direction.north;
        turnCounter = 0;
    }
}
