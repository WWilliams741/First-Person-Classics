using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_SpaceInvaders : MonoBehaviour
{

    [SerializeField] Rigidbody rb;
    [SerializeField] Transform barrel;
    [SerializeField] GameObject rocket;
    [SerializeField] Game_Manager_Script_SpaceInvaders gameManager;

    // Start is called before the first frame update
    void Start()
    {
        stop();
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameManager.paused)
        {
            if (Input.GetKey(KeyCode.A))
            {
                moveLeft();
            }
            else if (Input.GetKey(KeyCode.D))
            {
                moveRight();
            }
            else
            {
                stop();
            }

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("calling shoot!");
                shoot();
            }
        }
    }

    private void moveRight()
    {
        rb.velocity = Vector3.right * 10f;
    }

    private void moveLeft()
    {
        rb.velocity = Vector3.left * 10f;
    }

    private void stop()
    {
        rb.velocity = Vector3.zero;
    }

    private void shoot()
    {
        if(!rocket.activeSelf)
        {
            rocket.transform.position = new Vector3(barrel.position.x, barrel.position.y, barrel.position.z + 3f);
            rocket.SetActive(true);
        }
    }

}
