using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardAI : MonoBehaviour {
    
    [SerializeField]private bool IsUnderWaterTurtle;
    [SerializeField] private int Speed;
    //positive or negative 1 to denote direction 
    [SerializeField] private int Direction;
    [SerializeField] private Transform resetLocation;
    [SerializeField] private Rigidbody RB;
    [SerializeField] private Transform trans;


    bool underwater;
    float startY;

    // Start is called before the first frame update
    void Start()    {
        RB.velocity = Vector3.right * Speed * Direction;
        underwater = false;
        startY = RB.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsUnderWaterTurtle && !underwater)
        {
            underwater = true;
            StartCoroutine(alternateUnderWater());
        }
    }

    private void OnTriggerEnter(Collider Collider) {
        //Debug.Log(Collider.gameObject.name);
        if (Collider.gameObject.name == "leftCollision" && Direction == -1) {
            Debug.Log("Hitting the left wall");
            trans.position = new Vector3(resetLocation.position.x , trans.position.y, trans.position.z);
        }
        else if (Collider.gameObject.name == "rightCollision" && Direction == 1) {
            Debug.Log("Hitting the right wall");
            trans.position = new Vector3(resetLocation.position.x, trans.position.y, trans.position.z);
        }


    }

    IEnumerator alternateUnderWater()
    {

        while(true)
        {
            Debug.Log("moving the turtle underwater");
            RB.velocity += Vector3.down;
            yield return new WaitForSecondsRealtime(3.15f);

            Debug.Log("moving the turtle above water");
            RB.velocity += Vector3.up * 2;

            yield return new WaitForSecondsRealtime(3f);

            StartCoroutine(waitForCycle());

            yield break;
        }
    }

    IEnumerator waitForCycle()
    {
        RB.velocity += Vector3.down;
        yield return new WaitForSeconds(5);
        RB.position = new Vector3(RB.position.x, startY, RB.position.z);
        underwater = false;
    }
}
