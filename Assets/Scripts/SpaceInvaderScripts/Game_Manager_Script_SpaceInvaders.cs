using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Game_Manager_Script_SpaceInvaders : MonoBehaviour
{

    [SerializeField] RowManager_Script[] rowManagers;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] private GameObject PauseMenuUI;
    [SerializeField] private GameObject LoseMenuUI;
    //[SerializeField] private GameObject WinMenuUI;
    [SerializeField] private GameObject Life1;
    [SerializeField] private GameObject Life2;
    public TextMeshProUGUI PlayerScoreText;


    private Coroutine[] rows;

    public float waitTime;
    public bool hitRight;
    private int leftMost;
    private int rightMost;
    public int lives;
    public int score;
    public bool paused = false;

    private LinkedList<int> InvaderIndices;
    private Hashtable shootingInvaders;

    // Start is called before the first frame update
    void Start()
    {
        updateWaitTime();
        rows = new Coroutine[rowManagers.Length];
        hitRight = false;
        moveRows();
        lives = 3;
        InvaderIndices = new LinkedList<int>();
        shootingInvaders = new Hashtable();

        GetBottomMost();
        for (int i = 0; i < shootingInvaders.Count; i++)
        {
            Debug.Log(shootingInvaders[i]);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (paused) {
            Time.timeScale = 0;
        }
        else {
            Time.timeScale = 1;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !PauseMenuUI.activeSelf) {
            Debug.Log("Pausing the game");
            PauseMenuUI.SetActive(true);
            paused = true;
            //TO DO add functionality to pause cars and interupt movement

        }
        else if (Input.GetKeyDown(KeyCode.Escape) && PauseMenuUI.activeSelf) {
            Debug.Log("Un-pausing the game");
            PauseMenuUI.SetActive(false);
            paused = false;
        }
        
    }
    /*
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
    }*/

    public void hitBoundry(string Side) {
        if (Side == "right" && !hitRight) {
            hitRight = true;
            for (int i = 0; i < rows.Length; i++) {
                rowManagers[i].left = !rowManagers[i].left;
            }
            moveRowsDown();
            
        }
        else if (Side == "left" && hitRight) {
            hitRight = false;
            for (int i = 0; i < rows.Length; i++) {
                rowManagers[i].left = !rowManagers[i].left;
            }
            moveRowsDown();
            
        }
    }

    
    public void moveRows()
    {
        for (int i = 0; i < rows.Length; i++)
        {
            rows[i] = StartCoroutine(rowManagers[i].move());
        }
    }
    /*
    public void moveRowsRight()
    {
        for (int i = 0; i < rows.Length; i++)
        {
            rows[i] = StartCoroutine(rowManagers[i].moveRight(waitTime));
        }
    }*/

    public void moveRowsDown() {


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

    public void GetBottomMost()
    {
        InvaderIndices = new LinkedList<int>();
        for (int i = 0; i < rowManagers[0].Invaders.Length; i++)
        {
            InvaderIndices.AddLast(i);
        }
        shootingInvaders.Clear();

        for (int i = 0; i < rowManagers.Length; i++)
        {
            int index = InvaderIndices.First.Value;
            while (index <= InvaderIndices.Last.Value)
            {
                try
                {
                    if (rowManagers[i].Invaders[index].activeSelf)
                    {
                        shootingInvaders.Add(index, rowManagers[i].Invaders[index].transform);
                        int temp = index;
                        index = InvaderIndices.Find(index).Next.Value;
                        InvaderIndices.Remove(temp);
                    }
                    else
                    {
                        index = InvaderIndices.Find(index).Next.Value;
                    }
                } catch (Exception e) {
                    if (e is NullReferenceException || e is ArgumentException)
                    {
                        break;
                    }
                }
            }
        }
    }

    public void InvadersShoot()
    {
        Transform trans;
        foreach (var key in shootingInvaders.Keys)
        {
            break;
            // implement random shooting here?
        }

        // column x rocket goes to trans by some offset here - set it active:
    }

    public void updateWaitTime()
    {
        int totalAlive = getTotalAlive();

        waitTime =  Mathf.Log(totalAlive, 10f) + .25f;
    }
    public void updatePlayerScore() {
        PlayerScoreText.text = "Score: " + score.ToString();
        
    }


    public void updateLives()
    {
        if (lives == 3)
        {
            lives = 2;
            livesText.text = "Lives: " + lives.ToString();
            Life1.SetActive(false);
        }
        else if (lives == 2)
        {
            lives = 1;
            livesText.text = "Lives: " + lives.ToString();
            Life1.SetActive(false);
        }
        else if (lives == 1)
        {
            lives = 0;
            livesText.text = "Lives: " + lives.ToString();
            GameOver();
        }

    }

    public void GameOver() {
        paused = true;
        //game over menu
        LoseMenuUI.SetActive(true);
    }

    public void goToMainMenu() {
        SceneManager.LoadScene("Main Menu");
    }
    public void restart() {
        SceneManager.LoadScene("Space Invaders");

    }


    public void QuitGame(string a) {
        Debug.Log("Quit Game Button Clicked");

    
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    
    }


}
