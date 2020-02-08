using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public int playerScore = 0;
    public int enemyScore = 0;
    [SerializeField] private GameObject pongBall;
    [SerializeField] private PongBallMovement pongBallScript;
    private bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()    {
        if (!pongBall.activeSelf & !started) {
            StartCoroutine(SpawnBall());
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
        Debug.Log(playerScore);
    }

    public void updateEnemyScore() {
        enemyScore += 1;
        Debug.Log(enemyScore);
    }


}
