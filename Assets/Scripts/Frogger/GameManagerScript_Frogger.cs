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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && !PauseMenuUI.activeSelf) {
            Debug.Log("Pausing the game");
            PauseMenuUI.SetActive(true);
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

}
