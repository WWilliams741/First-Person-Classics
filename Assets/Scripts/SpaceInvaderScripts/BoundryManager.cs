using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundryManager : MonoBehaviour
{

    [SerializeField] Game_Manager_Script_SpaceInvaders GameManager;
    [SerializeField] string WallSide;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision) {
        //if boundry collides with invader
        if (collision.gameObject.layer == 18 ) {
            print("Invader hit boundry");
            GameManager.hitBoundry(WallSide);
        }



    }



}
