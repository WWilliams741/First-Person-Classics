using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManagerScript_Pong : MonoBehaviour {
    public int playerScore = 0;
    public TextMeshProUGUI PlayerScoreText;
    public int enemyScore = 0;
    public int tieBreakerScore = 12;
    public bool tieBreaker = false;
    public TextMeshProUGUI EnemyScoreText;
    [SerializeField] private GameObject pongBall;
    [SerializeField] private PongBallMovement pongBallScript;
    private bool started = false;
    [SerializeField] private GameObject PauseMenuUI;

    [SerializeField] GameObject scoreInputMenu;
    [SerializeField] TextMeshProUGUI inputPanelScore;
    [SerializeField] TMP_InputField nameInput;
    [SerializeField] GameObject diffMenu;
    private bool lost = false;
    private bool gameOver = false;
    public bool paused;

    // Start is called before the first frame update
    void Start() {
        paused = true;

    }

    // Update is called once per frame
    void Update()    {
        if (!pongBall.activeSelf & !started) {
            StartCoroutine(SpawnBall());
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !PauseMenuUI.activeSelf && !diffMenu.activeSelf) {
            Debug.Log("Pausing the game");
            PauseMenuUI.SetActive(true);
            paused = true;
            //pongBallScript.Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && PauseMenuUI.activeSelf) {
            Debug.Log("Un-pausing the game");
            PauseMenuUI.SetActive(false);
            paused = false;
            //pongBallScript.Unpause();
        }

        
        if (paused) {
            Time.timeScale = 0f;
        }
        else {
            Time.timeScale = 1f;
        }

    }

    IEnumerator SpawnBall() {
        started = true;
        yield return new WaitForSecondsRealtime(5);
        pongBall.SetActive(true);
        started = false;
        Debug.Log("turning on ball");
        yield return new WaitForSecondsRealtime(5);
        pongBallScript.BeginMovement();
        
    }
    public void updatePlayerScore() {
        playerScore += 1;
        PlayerScoreText.text = playerScore.ToString();
        Debug.Log(playerScore);

        checkForTie();
        if (!tieBreaker && playerScore == 11)
        {
            win();
        }
        else if (playerScore == 12)
        {
            win();
        }
    }

    public void updateEnemyScore() {
        enemyScore += 1;
        EnemyScoreText.text = enemyScore.ToString();
        Debug.Log(enemyScore);

        checkForTie();
        if (!tieBreaker && enemyScore == 11)
        {
            lose();
        }
        else if (enemyScore == 12)
        {
            lose();
        }
    }

    public void checkForTie()
    {
        if (enemyScore == 10 && playerScore == 10)
        {
            tieBreaker = true;
        }
    }

    public void win()
    {
        //bring up winning screen here!
    }

    public void lose()
    {
        lost = true;
        paused = true;
        //bring up lose screen here!
    }

    public void MainMenu(string a) {
        Debug.Log(a);
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame(string a) {
        Debug.Log("Quit Game Button Clicked");
     #if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
    #else
         Application.Quit();
    #endif
    }

    public void un_pause() {
        paused = false;
    }



}
