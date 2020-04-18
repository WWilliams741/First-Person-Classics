using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuScript : MonoBehaviour {

    [SerializeField] TextMeshProUGUI[] MSNameArray;
    [SerializeField] TextMeshProUGUI[] MSScoreArray;

    [SerializeField] TextMeshProUGUI[] FroggerNameArray;
    [SerializeField] TextMeshProUGUI[] FroggerScoreArray;

    [SerializeField] TextMeshProUGUI[] SINameArray;
    [SerializeField] TextMeshProUGUI[] SIScoreArray;

    [SerializeField] TextMeshProUGUI[] PongNameArray;
    [SerializeField] TextMeshProUGUI[] PongScoreArray;




    // Start is called before the first frame update
    void Start()    {
        //update minesweeper highscore board on load
        for (int i = 0; i < 5 && i < PersistantGameManager.Instance.highScoreData.highScoresMS.Count; i++) {
            MSNameArray[i].text = PersistantGameManager.Instance.highScoreData.highScoresMS[i].name;
            MSScoreArray[i].text = PersistantGameManager.Instance.highScoreData.highScoresMS[i].score.ToString();
        }
        //update Frogger highscore board on load
        for (int i = 0; i < 5 && i < PersistantGameManager.Instance.highScoreData.highScoresFrogger.Count; i++) {
            FroggerNameArray[i].text = PersistantGameManager.Instance.highScoreData.highScoresFrogger[i].name;
            FroggerScoreArray[i].text = PersistantGameManager.Instance.highScoreData.highScoresFrogger[i].score.ToString();
        }
        //update Space Invaders highscore board on load
        for (int i = 0; i < 5 && i < PersistantGameManager.Instance.highScoreData.highScoresSI.Count; i++) {
            SINameArray[i].text = PersistantGameManager.Instance.highScoreData.highScoresSI[i].name;
            SIScoreArray[i].text = PersistantGameManager.Instance.highScoreData.highScoresSI[i].score.ToString();
        }
        //update pong highscore board on load
        for (int i = 0; i < 5 && i < PersistantGameManager.Instance.highScoreData.highScoresPong.Count; i++) {
            PongNameArray[i].text = PersistantGameManager.Instance.highScoreData.highScoresPong[i].name;
            PongScoreArray[i].text = PersistantGameManager.Instance.highScoreData.highScoresPong[i].score.ToString();
        }



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

    public void SpaceInvaders(string data) {
        Debug.Log("SpaceInvaders Button Clicked");
        SceneManager.LoadScene("Space Invaders");
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
