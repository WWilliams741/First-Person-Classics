using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Game_Manager_Script_SpaceInvaders : MonoBehaviour
{

    [SerializeField] RowManager_Script[] rowManagers;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] private GameObject PauseMenuUI;
    [SerializeField] private GameObject LoseMenuUI;
    //[SerializeField] private GameObject WinMenuUI;
    [SerializeField] private GameObject Life1;
    [SerializeField] private GameObject Life2;
    [SerializeField] private GameObject player;
    public Transform playerTrans;
    public TextMeshProUGUI PlayerScoreText;
    [SerializeField] private GameObject EnemyRocket;
    [SerializeField] private GameObject spaceShip;
    [SerializeField] private Camera primaryCam;
    [SerializeField] private Camera secondaryCam;
    [SerializeField] private BarrierController_script[] barriers;
    [SerializeField] private soundManager_spaceInvaders soundManager;

    [SerializeField] GameObject scoreInputMenu;
    [SerializeField] TextMeshProUGUI inputPanelScore;
    [SerializeField] TMP_InputField nameInput;
    private bool gameEnd = false;

    private Vector3 playerStart;
    private Coroutine[] rows;
    private Transform shootTrans;
    private List<int> closest;
    public float waitTime;
    public bool hitRight;
    private int leftMost;
    private int rightMost;
    public int lives;
    public int score;
    public bool paused = false;
    public int victoryCounter;

    private LinkedList<int> InvaderIndices;
    private Hashtable shootingInvaders;

    // Start is called before the first frame update
    void Start()
    {
        playerStart = playerTrans.position;
        victoryCounter = 0;
        updateWaitTime();
        rows = new Coroutine[rowManagers.Length];
        hitRight = false;
        moveRows();
        lives = 3;
        InvaderIndices = new LinkedList<int>();
        shootingInvaders = new Hashtable();

        GetBottomMost();

        StartCoroutine(spaceShipChance());
        secondaryCam.depth = primaryCam.depth - 1;
    }


    // Update is called once per frame
    void Update()
    {

       

        secondaryCam.transform.position = new Vector3(primaryCam.transform.position.x, secondaryCam.transform.position.y, secondaryCam.transform.position.z);
        if (paused) {
            Time.timeScale = 0f;
        }
        else {
            Time.timeScale = 1f;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !PauseMenuUI.activeSelf && !scoreInputMenu.activeSelf) {
            Debug.Log("Pausing the game");
            PauseMenuUI.SetActive(true);
            paused = true;
            

        }
        else if (Input.GetKeyDown(KeyCode.Escape) && PauseMenuUI.activeSelf && !gameEnd) {
            Debug.Log("Un-pausing the game");
            PauseMenuUI.SetActive(false);
            paused = false;
        }
        if (!EnemyRocket.activeSelf) {
            InvadersShoot();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            killAllInvaders();
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

    public void killAllInvaders()
    {
        for (int k = 0; k < rowManagers.Length; k++)
        {
            for (int l = 0; l < rowManagers[0].Invaders.Length; l++)
            {
                rowManagers[k].killInvader(l);
            }
        }
        winRound();
    }

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

    public void resetInvaders()
    {
        for (int i = 0; i < rowManagers.Length; i++)
        {
            rowManagers[i].resetInvaders();
        }
    }

    public void winRound()
    {

        for (int i = 0; i < rows.Length; i++)
        {
            StopCoroutine(rows[i]);
        }

        EnemyRocket.SetActive(false);
        playerTrans.position = playerStart;
        for (int b = 0; b < barriers.Length; b++)
        {
            barriers[b].resetCubes();
        }

        victoryCounter++;
        if (victoryCounter < 6)
        {
            resetInvaders();
            for (int i = 0; i < victoryCounter; i++)
            {
                Debug.Log("moving rows down " + victoryCounter + " times.");
                moveRowsDown();
            }
        }
        else
        {
            resetInvaders();
            for (int i = 0; i < 5; i++)
            {
                Debug.Log("moving row down, but they can't go any further!");
                moveRowsDown();
            }
        }
        updateWaitTime();

        for (int i = 0; i < rows.Length; i++)
        {
            rows[i] = StartCoroutine(rowManagers[i].move());
        }
    }

    public void moveRowsDown() {


        for (int j = 0; j < rowManagers.Length; j++)
        {
            rowManagers[j].moveDown();
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

    public void InvadersShoot() {
        closest = new List<int>();
        
        if (getTotalAlive() > 0)
        {
            foreach (var key in shootingInvaders.Keys)
            {
                closest.Add((int)key);
            }
            closest.Sort(SortByPosition);
            shootTrans = (Transform)shootingInvaders[closest[0]];
            closest.Reverse();
            foreach (var key in closest)
            {
                if (UnityEngine.Random.Range(0, 9) < 1)
                {
                    shootTrans = (Transform)shootingInvaders[closest[key]];
                    break;
                }
            }

            // column x rocket goes to trans by some offset here - set it active:
            EnemyRocket.transform.position = new Vector3(shootTrans.position.x, shootTrans.position.y, shootTrans.position.z - 2f);
            EnemyRocket.SetActive(true);
        }
    }

    //comparison function for sort
    public int SortByPosition(int invader1, int invader2) {
        double dist1 = Mathf.Abs(((Transform)shootingInvaders[invader1]).position.x - playerTrans.position.x);
        double dist2 = Mathf.Abs(((Transform)shootingInvaders[invader2]).position.x - playerTrans.position.x);
        return dist1.CompareTo(dist2);
    }

    public void spawnSpaceShip()
    {
        spaceShip.SetActive(true);
    }

    public IEnumerator spaceShipChance()
    {
        while (true) {
            if (!paused) {
                int rand = Random.Range(1, 6);
                if ( rand == 1) {
                    Debug.Log("spawning the ship!");
                    spawnSpaceShip();
                }
                else {
                    Debug.Log("NOT spawning the ship! " + rand);
                }
                yield return new WaitForSeconds(10f);
            }
        }
    }


    public void updateWaitTime()
    {
        int totalAlive = getTotalAlive();
        Debug.Log("totalalive: " + totalAlive);

        waitTime =  Mathf.Log(totalAlive, 10f) + .25f;

        if (totalAlive <= 0)
        {
            Debug.Log("you have won the round!");
            winRound();
        }
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
            Life2.SetActive(false);
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
            gameOver();            
        }

    }

    public void gameOver() {
        gameEnd = true;
        paused = true;
        scoreInputMenu.SetActive(true);
    }

    public void respawn1() {
        if(lives>1)
        StartCoroutine(respawn());

    }
    public IEnumerator respawn() {
        print("starting respawn");
        player.SetActive(true);
        print(player.activeSelf);
        yield return new WaitForSecondsRealtime(2f);
        paused = false;
        print("respawning");
        player.SetActive(true);

    }


    public void menuOpen() {
        PersistantGameManager.Instance.addPlayerSpaceInvaders(nameInput.text, score);
        scoreInputMenu.SetActive(false);
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
