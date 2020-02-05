using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{   
    //set to public so it can be modified   
    public float speed;
    //player rigidbody at feet
    [SerializeField] private Rigidbody myRigidbody;
    //public Animator anim;
    //change to be made to character position
    private Vector3 change;
    [SerializeField] private GameObject player;
    //private string prevScene;
    bool created;

    [SerializeField] private bool enableHorizontalMove;

    void Awake() {
        if (!created) {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }

        else {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //myRigidbody = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //every frame, reset how much player has changed
        change = Vector3.zero;
        //we don't want any form of acceleration so we use getaxisraw

        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        if (change != Vector3.zero && enableHorizontalMove)
        {
            MoveCharacter(change);   
			
			//setting floats for animation x and y -Milo
            //anim.SetFloat("moveX", change.x);
            //anim.SetFloat("moveY", change.y);
            //setting boolean isMoving true for walking animations -Milo
            //anim.SetBool("isMoving", true);
        }
        else if(change != Vector3.zero && !enableHorizontalMove)
        {
            change.x = 0;
            MoveCharacter(change);
        }
		else{
            //setting boolean isMoving false to stop walking animation -Milo
            //anim.SetBool("isMoving", false);
            return;
		}
        
    }

    void MoveCharacter(Vector3 direction)
    {
        //my position + (change*speed*timechagne)
        myRigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
        //left (-1,0)
        //right (1,0)
        //up (0,1)
        //down (0,-1)

    }

}
