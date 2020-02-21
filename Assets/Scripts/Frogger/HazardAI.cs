using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardAI : MonoBehaviour {
    
    [SerializeField]private bool IsTurtle;
    [SerializeField] private int Speed;
    //positive or negative 1 to denote direction 
    [SerializeField] private int Direction;
    [SerializeField] private Transform resetLocation;
    [SerializeField] private Rigidbody RB;
    [SerializeField] private Transform trans;




    // Start is called before the first frame update
    void Start()    {
        RB.velocity = Vector3.right * Speed * Direction;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider Collider) {
        Debug.Log(Collider.gameObject.name);
        if (Collider.gameObject.name == "leftCollision" && Direction == -1) {
            trans.position = new Vector3(resetLocation.position.x , resetLocation.position.y, trans.position.z);
        }
        else if (Collider.gameObject.name == "rightCollision" && Direction == 1) {
            trans.position = new Vector3(resetLocation.position.x, resetLocation.position.y, trans.position.z);
        }


    }


}
