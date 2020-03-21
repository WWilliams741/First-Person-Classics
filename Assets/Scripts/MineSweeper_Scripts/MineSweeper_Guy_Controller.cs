using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSweeper_Guy_Controller : MonoBehaviour
{

    [SerializeField] Animator anim;
    [SerializeField] MineSweeperGameController gameController;
    //[SerializeField] Transform hips;


    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool("Walking", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            anim.SetBool("Walking", true);
        }

        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("I am turning left");
            transform.Rotate(Vector3.up, -1f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("I am turning right");
            transform.Rotate(Vector3.up, 1f);
        }

        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Left clicking!");
            getBlock();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("right clicking!");
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.speed = 2f;
        }
        else
        {
            anim.speed = 1f;
        }
    }

    void getBlock()
    {

        LayerMask blockLayerMask = 1 << 17;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, blockLayerMask)) {
            Debug.Log("I hit a block!");
            MineSweeperTileController block_script = hit.transform.gameObject.GetComponent<MineSweeperTileController>();
            if(!block_script.pressed)
            {
                block_script.revealTile(1);
            }
            //Vector3 start = transform.position;
            //start.y += 1.5f;
            //Debug.DrawRay(start, ray.direction * 3f, Color.red, 100f);
        }
    }

    public void resetWalk()
    {
        //transform.position = new Vector3(hips.position.x, transform.position.y, hips.position.z);
        anim.SetBool("Walking", false);
    }
}
