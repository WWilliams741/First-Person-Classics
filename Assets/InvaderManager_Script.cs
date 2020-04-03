using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderManager_Script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.Equals(19))
        {
            gameObject.SetActive(false);
        }
    }
}
