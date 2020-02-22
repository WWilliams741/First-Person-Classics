using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

    [SerializeField]
    private float stuff;
    private float morestuff;
    private float evenmorestuff;
    [SerializeField]

    // Start is called before the first frame update
    void Start()    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pong(string data) {
        Debug.Log("Pong Button Clicked");
        SceneManager.LoadScene("Pong");
    }

    public void MineSweeper(string data) {
        Debug.Log("Minesweeper Button Clicked");
        SceneManager.LoadScene("MineSweeper");
    }

    public void PacMan(string data) {
        Debug.Log("PacMan Button Clicked");
        SceneManager.LoadScene("Pac-Man");
    }

    public void Frogger(string data) {
        Debug.Log("Frogger Button Clicked");
        SceneManager.LoadScene("Frogger");
    }

    public void QuitGame(string data) {
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
