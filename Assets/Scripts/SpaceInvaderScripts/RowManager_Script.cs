using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowManager_Script : MonoBehaviour
{

    public GameObject[] Invaders;
    [SerializeField] private Game_Manager_Script_SpaceInvaders gameManager;

    private int alive;
    public bool paused;
    public bool left;

    // Start is called before the first frame update
    void Start()
    {
        alive = Invaders.Length;
        left = false;
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

    public IEnumerator move()
    {
        while (!paused) {
            if (!left) {
                transform.position = new Vector3(transform.position.x + 0.82f, transform.position.y, transform.position.z);
            }
            else {
                transform.position = new Vector3(transform.position.x - 0.82f, transform.position.y, transform.position.z);
            }
            yield return new WaitForSecondsRealtime(gameManager.waitTime);
        }
        
    }

    public void moveDown()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2f);
    }
}
