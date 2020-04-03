using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket_script : MonoBehaviour
{

    [SerializeField] Rigidbody rb;
    [SerializeField] Game_Manager_Script_SpaceInvaders gameManager;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = Vector3.forward * 20f;
    }

    // Update is called once per frame
    void Update()
    {
        //rb.velocity = Vector3.forward * 20f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.Equals(18))
        {
            Debug.Log("hit an invader, telling gameManager to update speed.");
            gameManager.updateWaitTime();
            gameObject.SetActive(false);
        }
        else if (other.gameObject.layer.Equals(20))
        {
            gameObject.SetActive(false);
        }
    }
}
