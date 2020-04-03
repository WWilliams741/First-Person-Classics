using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier_script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void destroySelf()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.Equals(18) || other.gameObject.layer.Equals(19))
        {
            destroySelf();
        }
    }
}
