using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public int playerScore = 0;
    public TextMeshProUGUI PlayerScoreText;
    public int enemyScore = 0;
    public TextMeshProUGUI EnemyScoreText;
    [SerializeField] private GameObject pongBall;
    [SerializeField] private PongBallMovement pongBallScript;
    private bool started = false;
    [SerializeField] private GameObject PauseMenuUI;

    // Start is called before the first frame update
    void Start() 
    {
        
    }

    // Update is called once per frame
    void Update()    {
        if (!pongBall.activeSelf & !started) {
            StartCoroutine(SpawnBall());
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !PauseMenuUI.activeSelf) {
            Debug.Log("Pausing the game");
            PauseMenuUI.SetActive(true);
            pongBallScript.Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && PauseMenuUI.activeSelf) {
            Debug.Log("Un-pausing the game");
            PauseMenuUI.SetActive(false);
            pongBallScript.Unpause();
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
    }

    public void updateEnemyScore() {
        enemyScore += 1;
        EnemyScoreText.text = enemyScore.ToString();
        Debug.Log(enemyScore);
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



}
