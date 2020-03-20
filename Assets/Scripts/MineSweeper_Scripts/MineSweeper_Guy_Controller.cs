using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSweeper_Guy_Controller : MonoBehaviour
{

    [SerializeField] Animator anim;
    [SerializeField] MineSweeperGameController gameController;


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

        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Left clicking!");
            getBlock();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("right clicking!");
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
}
