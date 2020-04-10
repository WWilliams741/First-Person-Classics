using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket_script : MonoBehaviour
{

    [SerializeField] Rigidbody rb;
    [SerializeField] Game_Manager_Script_SpaceInvaders gameManager;
    [SerializeField] private bool playerRocket = false;

    // Start is called before the first frame update
    void Start()
    {
        if (playerRocket)
        {
            rb.velocity = Vector3.forward * 20f;
        }
        else
        {
            print("shooting enemy rocket");
            rb.velocity = Vector3.forward * -10f;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.Equals(18)) {
            Debug.Log("hit an invader, telling gameManager to update speed.");
            gameManager.updateWaitTime();
            gameObject.SetActive(false);
            gameManager.score += System.Convert.ToInt32(other.gameObject.tag);
            gameManager.updatePlayerScore();
            gameManager.GetBottomMost();
        }
        else if (other.gameObject.layer.Equals(20)|| other.gameObject.layer.Equals(9) || other.gameObject.layer.Equals(19)) {
            gameObject.SetActive(false);
        }
    }
}
