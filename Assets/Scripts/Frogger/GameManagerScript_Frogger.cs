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
    [SerializeField] private GameObject Life1;
    [SerializeField] private GameObject Life2;
    [SerializeField] private RectTransform Timer;
    [SerializeField] FroggerController froggerController;
    public bool paused = false;
    public float timerTotal;

    // Start is called before the first frame update
    void Start()    {
        timerTotal = Timer.rect.width;
        Debug.Log(timerTotal);
        StartCoroutine(startTimer());
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
            
        }
    }

    public void updatePlayerScore() {
        playerScore += 100;
        PlayerScoreText.text = playerScore.ToString();
        Debug.Log(playerScore);
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

    public IEnumerator startTimer() {
        float totalTime = 30;

        while (true) {
            yield return new WaitForSecondsRealtime(1);
            if (!paused && totalTime > 0) {
                totalTime -= 1;

                Timer.sizeDelta = new Vector2(timerTotal * totalTime / 30, Timer.rect.height);
            }
            else if (totalTime < 1) {
                froggerController.death();
                Debug.Log("deth");
                
            }
            
            
        }
        yield return null;
    }

    public void restartTimer() {
        StopCoroutine(startTimer());
        StartCoroutine(startTimer());
    }


}
