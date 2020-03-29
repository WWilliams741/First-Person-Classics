using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowManager_Script : MonoBehaviour
{

    public GameObject[] Invaders;

    private int alive;
    public bool paused;

    // Start is called before the first frame update
    void Start()
    {
        alive = Invaders.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getLeftMost()
    {
        for (int i = 0; i < Invaders.Length; i++)
        {
            if(Invaders[i].activeSelf)
            {
                return i;
            }
        }
        return -1;
    }

    public int getRightMost()
    {
        for (int i = Invaders.Length - 1; i >= 0; i--)
        {
            if (Invaders[i].activeSelf)
            {
                return i;
            }
        }
        return -1;
    }

    public int getAlive()
    {
        return alive;
    }

    public void killInvader(int i)
    {
        Invaders[i].SetActive(false);
        alive--;
    }

    public void resetInvaders()
    {
        for (int i = 0; i < Invaders.Length; i++)
        {
            Invaders[i].SetActive(true);
        }
        alive = Invaders.Length;
    }

    public IEnumerator moveLeft(float waitTime)
    {
        while(!paused)
        {
            yield return new WaitForSecondsRealtime(waitTime);
            transform.position = new Vector3(transform.position.x - 2f, transform.position.y, transform.position.z);
        }
    }

    public IEnumerator moveRight(float waitTime)
    {
        while (!paused)
        {
            yield return new WaitForSecondsRealtime(waitTime);
            transform.position = new Vector3(transform.position.x + 2f, transform.position.y, transform.position.z);
        }
    }

    public void moveDown()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2f);
    }
}
