using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetGoalFrogs : MonoBehaviour
{
    [SerializeField] GameObject butterFly;
    [SerializeField] GameManagerScript_Frogger gameManager;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.goalCount == 0)
        {
            butterFly.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
