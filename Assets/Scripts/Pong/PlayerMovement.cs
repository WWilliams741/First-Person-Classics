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
    bool created;

    [SerializeField] private float maxY;
    [SerializeField] private float minY;

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //every frame, reset how much player has changed
        change = Vector3.zero;

        //we don't want any form of acceleration so we use getaxisraw
        change.y = Input.GetAxisRaw("Vertical");

        float currentY = myRigidbody.position.y;


        if (change != Vector3.zero && currentY < maxY && change.y > 0)
        {
            MoveCharacter(change);
        }
        else if (change != Vector3.zero && currentY > minY && change.y < 0)
        {
            MoveCharacter(change);
        }
        else
        {
            return;
        }

    }

    void MoveCharacter(Vector3 direction)
    {
        //my position + (change*speed*timechagne)
        myRigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
    }

}
