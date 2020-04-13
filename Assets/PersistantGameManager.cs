using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistantGameManager : MonoBehaviour {
    List<player> highScoresMS = new List<player>(); //minesweeper
    List<player> highScoresFrogger = new List<player>(); //frogger
    List<player> highScoresSI = new List<player>(); //space invaders
    List<player> highScoresPong = new List<player>(); //pong



    private static PersistantGameManager _instance;

    public static PersistantGameManager Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<PersistantGameManager>();
                if (_instance == null){
                    _instance = new GameObject().AddComponent<PersistantGameManager>();
                }
            }
            return _instance;
        }
    }


    // Start is called before the first frame update
    void Start() {
        SceneManager.LoadScene(1);
        /*
        highScores.Add(new player("Mahesh Chand", 35));
        highScores.Add(new player("Mike Gold", 25));
        highScores.Add(new player("Praveen Kumar", 29));
        highScores.Add(new player("Raj Beniwal", 21));
        highScores.Add(new player("Dinesh Beniwal", 84));
        highScores.Sort();

        foreach (player x in highScores) {
            print(x.name + " " + x.score);

        }*/

    }
    public void Awake() {
        //print("ddol");
        if (_instance != null) Destroy(this);
        DontDestroyOnLoad(this.gameObject);

    }

    // Update is called once per frame
    void Update() {
        Shader.SetGlobalFloat("_timeU", Time.unscaledTime);

    }

    //TODO add logic for top 5
    public void addPlayerMineSweeper(string _name, int _score) {
        highScoresMS.Add(new player(_name, _score));
        highScoresMS.Sort();
    }
    public void addPlayerFrogger(string _name, int _score) {
        highScoresFrogger.Add(new player(_name, _score));
        highScoresFrogger.Sort();
    }
    public void addPlayerSpaceInvaders(string _name, int _score) {
        highScoresSI.Add(new player(_name, _score));
        highScoresSI.Sort();
    }
    public void addPlayerPong(string _name, int _score) {
        highScoresPong.Add(new player(_name, _score));
        highScoresPong.Sort();
    }


}

public class player : System.IComparable<player> {
    public string name;
    public int score;

    public player(string _name, int _score) {
        name = _name;
        score = _score;
    }
    public int CompareTo(player other) {
        if (other == null) {
            return 1;
        }

        //Return the difference in score.
        return other.score - score;
    }
}


