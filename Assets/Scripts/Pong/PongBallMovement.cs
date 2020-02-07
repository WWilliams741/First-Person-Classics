using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongBallMovement : MonoBehaviour
{

    [SerializeField] Rigidbody ball;
    [SerializeField] float speed;

    // Start is called before the first frame update
    void Start()
    {
        ball.velocity = Vector3.right * speed;
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
            applyForceToBall(Vector3.down * 2 * speed);
        }
        else if (collision.gameObject.name == "Lower Bound")
        {
            //send up - y
            applyForceToBall(Vector3.up * 2 * speed);
        }
        else if (collision.gameObject.name == "Player")
        {
            //send right - x
            applyForceToBall(Vector3.right * 2 * speed);
        }
        else if (collision.gameObject.name == "Enemy")
        {
            //send left = x
            applyForceToBall(Vector3.left * 2 * speed);
        }
    }

    void applyForceToBall(Vector3 direction)
    {
        ball.velocity = ball.velocity + direction;
    }
}
