﻿using System.Collections;
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
            if (Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow))
            {
                moveLeft();
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
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
            if (Input.GetKeyDown(KeyCode.Space)) {
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
            rocket.transform.position = new Vector3(barrel.position.x, barrel.position.y - 0.5f, barrel.position.z + 3f);
            rocket.SetActive(true);
        }
    }

    private void die()
    {
        // Insert losing stuff here - pause game and such:
        gameManager.updateLives();
        gameObject.SetActive(false);
        gameManager.paused = true;
        StartCoroutine(respawn());
    }

    public IEnumerator respawn() {
        yield return new WaitForSecondsRealtime(2);
        gameManager.paused = false;
        gameObject.SetActive(true);

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.Equals(19))
        {
            die();
        }
    }
}