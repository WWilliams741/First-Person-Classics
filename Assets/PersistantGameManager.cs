using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistantGameManager : MonoBehaviour {
    List<player> highScores = new List<player>();

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

        highScores.Add(new player("Mahesh Chand", 35));
        highScores.Add(new player("Mike Gold", 25));
        highScores.Add(new player("Praveen Kumar", 29));
        highScores.Add(new player("Raj Beniwal", 21));
        highScores.Add(new player("Dinesh Beniwal", 84));
        highScores.Sort();

        foreach (player x in highScores) {
            print(x.name + " " + x.score);

        }

    }
    public void Awake() {
        //print("ddol");
        if (_instance != null) Destroy(this);
        DontDestroyOnLoad(this.gameObject);

    }

    // Update is called once per frame
    void Update() {

    }

    public void addPlayer(string _name, int _score) {

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


