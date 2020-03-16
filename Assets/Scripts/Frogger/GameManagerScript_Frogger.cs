using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManagerScript_Frogger : MonoBehaviour
{
    public int playerScore = 0;
    public TextMeshProUGUI PlayerScoreText;
    
    [SerializeField] private GameObject PauseMenuUI;
    [SerializeField] private GameObject LoseMenuUI;
    [SerializeField] private GameObject WinMenuUI;
    [SerializeField] private GameObject Life1;
    [SerializeField] private GameObject Life2;
    [SerializeField] private RectTransform Timer;
    [SerializeField] FroggerController froggerController;
    public int goalCount;
    public bool paused = false;
    public float timerTotal;
    public int ExtraLives;

    // Start is called before the first frame update
    void Start()    {
        timerTotal = Timer.rect.width;
        Debug.Log(timerTotal);
        StartCoroutine(startTimer());
        ExtraLives = 2;
        goalCount = 0;
    }

    // Update is called once per frame
    void Update()
    {

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
        if (paused) {
            Time.timeScale = 0;
        }
        else {
            Time.timeScale = 1;
        }

    }

    public void updatePlayerScore() {
        playerScore += 100;
        goalCount++;
        PlayerScoreText.text = playerScore.ToString();
        Debug.Log(playerScore);

        if (goalCount == 5)
        {   /*
            goalCount = 0;
            froggerController.resetPosition();
            ExtraLives = 2;
            restartTimer();*/
            if (SceneManager.GetActiveScene().name == "Frogger")
                SceneManager.LoadScene("Frogger level 2");
            else {
                paused = true;
                WinMenuUI.SetActive(true);
            }
        }
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
    public float currentTime = 60;
    public IEnumerator startTimer() {
        

        while (true) {
            yield return new WaitForSecondsRealtime(1);
            if (!paused && currentTime > 0) {
                currentTime -= 1;

                Timer.sizeDelta = new Vector2(timerTotal * currentTime / 60, Timer.rect.height);
            }
            else if (currentTime < 1) {
                froggerController.death();
                Debug.Log("deth");
                
            }
            
            
        }
        yield return null;
    }

    public void restartTimer() {
        currentTime = 61;
    }

    public void LoseLife() {
        //could be done with an array if we had more lives
        if (ExtraLives == 2) {
            Life2.SetActive(false);
            ExtraLives -= 1;
        }
        else if (ExtraLives == 1) {
            Life1.SetActive(false);
            ExtraLives -= 1;
        }
        else if (ExtraLives == 0) {
            paused = true;
            LoseMenuUI.SetActive(true);
        }

    }

    public void restart() {
        /*
        if (LoseMenuUI.activeSelf)
            LoseMenuUI.SetActive(false);
        if (PauseMenuUI.activeSelf)
            PauseMenuUI.SetActive(false);           

        paused = false;
        froggerController.restart();
        playerScore = 0;
        PlayerScoreText.text = playerScore.ToString();
        ExtraLives = 2;
        Life1.SetActive(true);
        Life2.SetActive(true);
         */
        SceneManager.LoadScene("Frogger");

    }



}
