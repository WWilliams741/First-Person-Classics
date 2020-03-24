using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSweeper_Guy_Controller : MonoBehaviour
{

    [SerializeField] Animator anim;
    [SerializeField] GameObject gameController;

    private float turnSpeed;


    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool("Walking", false);
        turnSpeed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            anim.SetBool("Walking", true);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            anim.SetBool("Walking Back", true);
        }

        if (Input.GetKey(KeyCode.A))
        {
           // Debug.Log("I am turning left");
            transform.Rotate(Vector3.up, -1 * turnSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
           // Debug.Log("I am turning right");
            transform.Rotate(Vector3.up, turnSpeed);
        }

        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Left clicking!");
            getBlock();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("right clicking!");
            markBlock();
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.speed = 2f;
            turnSpeed = 2f;
        }
        else
        {
            anim.speed = 1f;
            turnSpeed = 1f;
        }
    }

    void getBlock()
    {

        LayerMask blockLayerMask = 1 << 17;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, blockLayerMask))
        {
            if (!hit.transform.tag.Equals("walls"))
            {
                Debug.Log("I marked a block! at " + hit.transform.position);
                gameController.GetComponent<MineSweeperGameController>().checkTile((int)hit.transform.position.x, (int)hit.transform.position.z);
            }
            else
            {
                Debug.Log("I hit a wall!");
            }
        }
    }

    void markBlock() {
        LayerMask blockLayerMask = 1 << 17;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, blockLayerMask)) {
            if(!hit.transform.tag.Equals("walls"))
            {
                Debug.Log("I marked a block! at " + hit.transform.position);
                gameController.GetComponent<MineSweeperGameController>().markBlock((int)hit.transform.position.x, (int)hit.transform.position.z);
            }
            else
            {
                Debug.Log("I hit a wall!");
            }
        }
    }

    public void resetWalk()
    {
        anim.SetBool("Walking", false);
    }

    public void resetWalkBack()
    {
        anim.SetBool("Walking Back", false);
    }
}
