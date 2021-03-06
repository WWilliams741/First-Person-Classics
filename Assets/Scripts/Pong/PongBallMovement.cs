﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongBallMovement : MonoBehaviour
{

    [SerializeField] Rigidbody ball;
    [SerializeField] GameObject pongBall;
    [SerializeField] float Xspeed;
    [SerializeField] float Yspeed;
    [SerializeField] float speed = 10;

    [SerializeField] GameManagerScript_Pong gameManager;
    [SerializeField] SoundManagerScriptPong soundManager;
    [SerializeField] GameObject explosion;


    [SerializeField] bool randomizeSpeed;
    [SerializeField] int lower_random_speed_limit;
    [SerializeField] int uppwer_random_speed_limit;

    // Start is called before the first frame update
    void Start(){
        BeginMovement();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.name == "Upper Bound" || collision.gameObject.name == "Lower Bound") {
            //send down - y
            //Debug.Log("Hitting the uppper bound, sending it down now.");
            //applyForceToBall(Vector3.down * 2 * Yspeed);

            //invert y velocity
            Debug.Log("hit upper or lower boundry");
            ball.velocity = new Vector3(Xspeed, -Yspeed, 0f);
            Yspeed = -Yspeed;
        }
        
        else if (collision.gameObject.name == "Player" || collision.gameObject.name == "Enemy")
        {
            //send right - x
            //applyForceToBall(Vector3.right * 2 * Xspeed);
            Debug.Log("hit a paddle");
            ball.velocity = new Vector3(-Xspeed, Yspeed, 0f);
            Xspeed = -Xspeed;
        }

        //score
        else if (collision.gameObject.name == "Player Score Zone") {
            pongBall.SetActive(false);
            pongBall.transform.SetPositionAndRotation(Vector3.zero, pongBall.transform.rotation);            
            gameManager.updateEnemyScore();

            //insert explosion here

        }
        else if (collision.gameObject.name == "Enemy Score Zone") {
            pongBall.SetActive(false);
            pongBall.transform.SetPositionAndRotation(Vector3.zero, pongBall.transform.rotation);            
            gameManager.updatePlayerScore();

            //insert explosion here

        }*/


    void OnTriggerEnter(Collider collision) {

        if (collision.gameObject.name == "Upper Bound" || collision.gameObject.name == "Lower Bound") {
            //send down - y
            //Debug.Log("Hitting the uppper bound, sending it down now.");
            //applyForceToBall(Vector3.down * 2 * Yspeed);

            //invert y velocity
            Debug.Log("hit upper or lower boundry");
            ball.velocity = new Vector3(Xspeed, -Yspeed, 0f);
            Yspeed = -Yspeed;
        }

        else if (collision.gameObject.name == "Player") {
            //send right - x
            //applyForceToBall(Vector3.right * 2 * Xspeed);
            Debug.Log("hit a paddle");
            ball.velocity = new Vector3(-Xspeed, Yspeed, 0f);
            Xspeed = -Xspeed;
            soundManager.playSound("bounce");

            explosion.SetActive(true);
            explosion.transform.position = new Vector3(transform.position.x + 4f, transform.position.y, transform.position.z);
        }
        else if (collision.gameObject.name == "Enemy")
        {
            //send right - x
            //applyForceToBall(Vector3.right * 2 * Xspeed);
            Debug.Log("hit a paddle");
            ball.velocity = new Vector3(-Xspeed, Yspeed, 0f);
            Xspeed = -Xspeed;
            soundManager.playSound("bounce");

            explosion.SetActive(true);
            explosion.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }

        //score
        else if (collision.gameObject.name == "Player Score Zone") {
            pongBall.SetActive(false);
            pongBall.transform.SetPositionAndRotation(Vector3.zero, pongBall.transform.rotation);
            gameManager.updateEnemyScore();

            //insert explosion here

        }
        else if (collision.gameObject.name == "Enemy Score Zone") {
            pongBall.SetActive(false);
            pongBall.transform.SetPositionAndRotation(Vector3.zero, pongBall.transform.rotation);
            gameManager.updatePlayerScore();

            //insert explosion here

        }
        


    }
    //apply a range angle and start ball movement
    public void BeginMovement() {
        /*
        if (randomizeSpeed) {
            Xspeed = Random.Range(lower_random_speed_limit, uppwer_random_speed_limit);
            Yspeed = Random.Range(lower_random_speed_limit, uppwer_random_speed_limit);

            ball.velocity = new Vector3(Xspeed, Yspeed, 0f);
        }
        else {
            ball.velocity = new Vector3(Xspeed, Yspeed, 0f);
        }*/
        int randomAngle = (int)Random.Range(-50,50) + (int)Random.Range(0, 1) * 180;
        Yspeed = speed * Mathf.Sin(randomAngle);
        Xspeed = speed * Mathf.Cos(randomAngle);
        ball.velocity = new Vector3(Xspeed, Yspeed, 0f);



    }
    //for pausing while in menu
    //deprecated
    public void Pause() {
        ball.velocity = Vector3.zero;

    }
    //unpause when exit menu
    //deprecated
    public void Unpause() {
        ball.velocity = new Vector3(Xspeed, Yspeed, 0f);

    }


    void applyForceToBall(Vector3 direction)
    {
        ball.velocity = ball.velocity + direction;
    }
}
