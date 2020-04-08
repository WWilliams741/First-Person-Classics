using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spaceShip_Script : MonoBehaviour
{
    [SerializeField] Transform startPosition;
    [SerializeField] Rigidbody rb;
    private int side;

    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(1, 2);
        if (rand == 1)
        {
            side = 1;
            transform.position = new Vector3(.5f, startPosition.position.y, 40f);
        }
        else
        {
            side = -1;
            transform.position = new Vector3(49.5f, startPosition.position.y, 40f);
        }

        rb.velocity = Vector3.right * 5f * side;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.Equals(20))
        {
            if (side == 1 && other.gameObject.name.Equals("RightWall"))
            {
                Debug.Log("hit the right wall!");
                gameObject.SetActive(false);
            }
            else if (side == -1 && other.gameObject.name.Equals("LeftWall"))
            {
                Debug.Log("hit the left wall!");
                gameObject.SetActive(false);
            }
        }
        else if (other.gameObject.layer.Equals(19))
        {
            gameObject.SetActive(false);
        }
    }

    
}
