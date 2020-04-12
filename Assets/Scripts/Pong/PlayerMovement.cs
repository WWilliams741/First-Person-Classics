using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    //set to public so it can be modified   
    public float speed;
    //player rigidbody at feet
    [SerializeField] private Rigidbody myRigidbody;
    //change to be made to character position
    private Vector3 change;
    [SerializeField] private GameObject player;
    //private string prevScene;
    

    [SerializeField] private GameObject PauseMenuUI;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //every frame, reset how much player has changed
        change = Vector3.zero;

        if (!PauseMenuUI.activeSelf)
        {
            //we don't want any form of acceleration so we use getaxisraw
            change.y = Input.GetAxisRaw("Vertical");
        }
       
        MoveCharacter(change);

    }

    void MoveCharacter(Vector3 direction)
    {
        myRigidbody.velocity = (change * speed * Time.deltaTime);
    }

}
