using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{

    [SerializeField] Rigidbody my_rigibody;
    [SerializeField] Transform ball;
    [SerializeField] Difficulty difficulty;
    [SerializeField] float speed;

    float yOfSatisfaction;
    float maxSpeed;
    float rate;

    public enum Difficulty
    {
        easy = 0,
        medium = 1,
        hard = 2
    };


    // Start is called before the first frame update
    void Start()
    {

        yOfSatisfaction = 1f;

        if (difficulty.Equals(Difficulty.easy))
        {
            maxSpeed = 5f;
            StartCoroutine(addPrecision());
            rate = 1f;
        }
        else if (difficulty.Equals(Difficulty.medium))
        {
            maxSpeed = 7f;
            StartCoroutine(addPrecision());
            rate = 2f;
        }
        else if (difficulty.Equals(Difficulty.hard))
        {
            maxSpeed = 9f;
            StartCoroutine(addPrecision());
            rate = 3f;
        }
        else
        {
            Debug.Log("An error has occured and a difficulty has not been selected somehow.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float yFromBall = Mathf.Abs(my_rigibody.position.y - ball.position.y);

        if (ball.position.y > my_rigibody.position.y && yFromBall > yOfSatisfaction)
        {
            moveUp(speed);
        }
        else if (ball.position.y < my_rigibody.position.y && yFromBall > yOfSatisfaction)
        {
            moveDown(speed);
        }
        else
        {
            stop();
        }
        if (difficulty.Equals(Difficulty.easy)) {
            maxSpeed = 5f;
            rate = 1f;
        }
        else if (difficulty.Equals(Difficulty.medium)) {
            maxSpeed = 7f;
            rate = 2f;
        }
        else if (difficulty.Equals(Difficulty.hard)) {
            maxSpeed = 9f;
            rate = 3f;
        }
        else {
            Debug.Log("An error has occured and a difficulty has not been selected somehow.");
        }


    }

    public void setDifficulty(int _difficulty){
        difficulty = (Difficulty)_difficulty;
    }

    void moveUp(float speed)
    {
        my_rigibody.velocity = new Vector3(0f, speed, 0f);
    }

    void moveDown(float speed)
    {
        my_rigibody.velocity = new Vector3(0f, -speed, 0f);
    }

    void stop()
    {
        my_rigibody.velocity = Vector3.zero;
    }

    IEnumerator addPrecision()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(5);
            float tempSpeed = speed + rate;
            if (tempSpeed < maxSpeed)
            {
                Debug.Log("Adding speed to the enemy paddle.");
                speed = tempSpeed;
            }
            else
            {
                Debug.Log("Stopping coroutine to add speed, maximum has been reached");
                yield break;
            }
        }
    }
}
