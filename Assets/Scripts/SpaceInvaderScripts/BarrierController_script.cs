using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierController_script : MonoBehaviour
{

    [SerializeField] GameObject[] cubes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetCubes()
    {
        for (int i = 0; i < cubes.Length; i++)
        {
            cubes[i].SetActive(true);
        }
    }
}
