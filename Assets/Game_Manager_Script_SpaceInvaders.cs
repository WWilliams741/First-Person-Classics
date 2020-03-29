using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager_Script_SpaceInvaders : MonoBehaviour
{

    [SerializeField] RowManager_Script[] rowManagers;

    private Coroutine[] rows;

    private float waitTime;

    private int leftMost;
    private int rightMost;

    // Start is called before the first frame update
    void Start()
    {
        waitTime = updateWaitTime();
        rows = new Coroutine[rowManagers.Length];

        moveRowsRight();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getLeftMost()
    {
        int left = rowManagers[0].Invaders.Length;
        for (int i = 0; i < rowManagers.Length; i++)
        {
            int leftMost = rowManagers[i].getLeftMost();
            if (left > leftMost)
            {
                left = leftMost;
            }
        }

        return left;
    }

    public int getRightMost()
    {
        int right = 0;
        for (int i = 0; i < rowManagers.Length; i++)
        {
            int rightMost = rowManagers[i].getRightMost();
            if (right < rightMost)
            {
                right = rightMost;
            }
        }

        return right;
    }

    public void moveRowsLeft()
    {
        for (int i = 0; i < rows.Length; i++)
        {
            rows[i] = StartCoroutine(rowManagers[i].moveLeft(waitTime));
        }
    }

    public void moveRowsRight()
    {
        for (int i = 0; i < rows.Length; i++)
        {
            rows[i] = StartCoroutine(rowManagers[i].moveRight(waitTime));
        }
    }

    public void moveRowsDown()
    {
        for (int i = 0; i < rowManagers.Length; i++)
        {
            rowManagers[i].moveDown();
        }
    }

    public int getTotalAlive()
    {
        int alive = 0;
        for (int i = 0; i < rowManagers.Length; i++)
        {
            alive += rowManagers[i].getAlive();
        }

        return alive;
    }

    public float updateWaitTime()
    {
        int totalAlive = getTotalAlive();

        return Mathf.Log(totalAlive, 2f) + 0.5f;
    }
}
