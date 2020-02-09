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

    enum Difficulty
    {
        easy,
        medium,
        hard
    };


    // Start is called before the first frame update
    void Start()
    {

        yOfSatisfaction = 1f;

        if (difficulty.Equals(Difficulty.easy))
        {
            StartCoroutine(addPrecision(1f));
        }
        else if (difficulty.Equals(Difficulty.medium))
        {
            StartCoroutine(addPrecision(2f));
        }
        else if (difficulty.Equals(Difficulty.hard))
        {
            StartCoroutine(addPrecision(3f));
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

    IEnumerator addPrecision(float rate)
    {
        yield return new WaitForSecondsRealtime(5);
        speed += rate;
    }
}
