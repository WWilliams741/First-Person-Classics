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
    public int finalScore = 0;
    public bool tieBreaker = false;
    public TextMeshProUGUI EnemyScoreText;
    [SerializeField] private GameObject pongBall;
    [SerializeField] private PongBallMovement pongBallScript;
    private bool started = false;
    [SerializeField] private GameObject PauseMenuUI;
    [SerializeField] private GameObject LoseMenuUI;
    [SerializeField] private GameObject WinMenuUI;


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
        if (!tieBreaker && playerScore == 10)
        {
            win();
        }
        else if (tieBreaker && playerScore - enemyScore == 2)
        {
            win();
        }
    }

    public void updateEnemyScore() {
        enemyScore += 1;
        EnemyScoreText.text = enemyScore.ToString();
        Debug.Log(enemyScore);

        checkForTie();
        if (!tieBreaker && enemyScore == 10)
        {
            lose();
        }
        else if (tieBreaker && enemyScore - playerScore == 2)
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
        gameOver = true;
        paused = true;
        finalScore = playerScore - enemyScore;

        scoreInputMenu.SetActive(true);
        playerScore = playerScore - enemyScore >= 0 ? playerScore - enemyScore : 0;
        inputPanelScore.text = "Your score is " + playerScore + " points!";
    }

    public void lose()
    {
        lost = true;
        paused = true;
        gameOver = true;
        finalScore = 0;

        //bring up lose screen here!
        scoreInputMenu.SetActive(true);
        playerScore = playerScore - enemyScore >= 0 ? playerScore - enemyScore : 0;
        inputPanelScore.text = "Your score is " + playerScore + " points!";
    }

    public void menuOpen() {
        PersistantGameManager.Instance.addPlayerFrogger(nameInput.text, finalScore);
        scoreInputMenu.SetActive(false);
        if (lost) {

            LoseMenuUI.SetActive(true);

        }
        else {
            WinMenuUI.SetActive(true);

        }
    }
    public void restart() {
        SceneManager.LoadScene("Pong");

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
