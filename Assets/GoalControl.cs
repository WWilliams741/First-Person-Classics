using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalControl : MonoBehaviour
{

    [SerializeField] GameObject goalFrog;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter (Collider Collider)
    {
        //ToDo: Update score before disabling buterfly and enabling goalFrog:

        if (Collider.gameObject.layer == 9)
        {
            Debug.Log("Frog has touched me.");
        }


        //Enable goalFrog:
        goalFrog.SetActive(true);

        //Disable butterfly:
        this.gameObject.SetActive(false);

    }
}
