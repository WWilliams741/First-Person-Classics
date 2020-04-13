using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowManager_Script : MonoBehaviour
{

    public GameObject[] Invaders;
    [SerializeField] private Game_Manager_Script_SpaceInvaders gameManager;
    [SerializeField] private Transform position;
    [SerializeField] soundManager_spaceInvaders soundManager;

    private Vector3 startPosition;
    private int moveCounter;

    public bool paused;
    public bool left;

    // Start is called before the first frame update
    void Start()
    {
        moveCounter = 0;
        startPosition = position.position;
        startPosition.x = 0f;
        //alive = Invaders.Length;
        left = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (position.position.z <= 8) {
            gameManager.gameOver();
        }
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
        int alive = 0;
        for (int i = 0; i < Invaders.Length; i++)
        {
            if (Invaders[i].activeSelf)
            {
                alive++;
            }
        }
        return alive;
    }

    
    public void killInvader(int i)
    {
        Invaders[i].SetActive(false);
        
    }
    

    public void resetInvaders()
    {
        moveCounter = 0;
        for (int i = 0; i < Invaders.Length; i++)
        {
            Invaders[i].SetActive(true);
        }
        transform.position = startPosition;
        Debug.Log("moving back to start postion: " + startPosition);
    }

    public IEnumerator move()
    {
        while (!paused) {
            if (!left) {
                transform.position = new Vector3(transform.position.x + (0.82f * Time.timeScale), transform.position.y, transform.position.z);
                changeSprite();
            }
            else {
                transform.position = new Vector3(transform.position.x - (0.82f * Time.timeScale), transform.position.y, transform.position.z);
                changeSprite();
            }
            
            if (moveCounter == 0)
            {
                soundManager.playSound("InvadersMove1");
            }
            else if (moveCounter == 1)
            {
                soundManager.playSound("InvadersMove2");
            }
            else if (moveCounter == 2)
            {
                soundManager.playSound("InvadersMove3");
            }
            else if (moveCounter == 3)
            {
                soundManager.playSound("InvadersMove4");
            }
            moveCounter = (moveCounter + 1) % 4;

            yield return new WaitForSeconds(gameManager.waitTime);
        }
        
    }

    public void moveDown()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2f);
    }

    public void changeSprite() {
        for (int i = 0; i < Invaders.Length; i++) {
            Invaders[i].GetComponent<InvaderManager_Script>().changeSprite();
        }

    }
}
