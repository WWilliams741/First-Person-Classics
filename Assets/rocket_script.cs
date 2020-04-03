using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket_script : MonoBehaviour
{

    [SerializeField] Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = Vector3.forward * 20f;
    }

    // Update is called once per frame
    void Update()
    {
        //rb.velocity = Vector3.forward * 20f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.Equals(18) || other.gameObject.layer.Equals(20))
        {
            Debug.Log("rocket being set to inactive!");
            gameObject.SetActive(false);
        }
    }
}
