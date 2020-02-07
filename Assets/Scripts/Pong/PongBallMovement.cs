using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongBallMovement : MonoBehaviour
{

    [SerializeField] Rigidbody ball;
    [SerializeField] float Xspeed;
    [SerializeField] float Yspeed;

    [SerializeField] bool randomizeSpeed;
    [SerializeField] int lower_random_speed_limit;
    [SerializeField] int uppwer_random_speed_limit;

    // Start is called before the first frame update
    void Start()
    {
        if (randomizeSpeed)
        {
            Xspeed = Random.Range(lower_random_speed_limit, uppwer_random_speed_limit);
            Yspeed = Random.Range(lower_random_speed_limit, uppwer_random_speed_limit);

            ball.velocity = new Vector3(Xspeed, Yspeed, 0f);
        }
        else
        {
            ball.velocity = new Vector3(Xspeed, Yspeed, 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Upper Bound") {
            //send down - y
            Debug.Log("Hitting the uppper bound, sending it down now.");
            applyForceToBall(Vector3.down * 2 * Yspeed);
        }
        else if (collision.gameObject.name == "Lower Bound")
        {
            //send up - y
            applyForceToBall(Vector3.up * 2 * Yspeed);
        }
        else if (collision.gameObject.name == "Player")
        {
            //send right - x
            applyForceToBall(Vector3.right * 2 * Xspeed);
        }
        else if (collision.gameObject.name == "Enemy")
        {
            //send left = x
            applyForceToBall(Vector3.left * 2 * Xspeed);
        }
    }

    void applyForceToBall(Vector3 direction)
    {
        ball.velocity = ball.velocity + direction;
    }
}
