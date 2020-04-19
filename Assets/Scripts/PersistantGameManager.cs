using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PersistantGameManager : MonoBehaviour {
    [HideInInspector] public string dataPath;
    public string extenstion;
    [HideInInspector] public HighScoreData highScoreData;
    [SerializeField] private int maxScoreCount;

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

    

    public void Awake()
    {
        //print("ddol");
        if (_instance != null) Destroy(this);
        DontDestroyOnLoad(this.gameObject);
        CreateSaveObjects();
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

    // Update is called once per frame
    void Update() {
        Shader.SetGlobalFloat("_timeU", Time.unscaledTime);

    }

    //TODO add logic for top 5
    public void addPlayerMineSweeper(string _name, int _score) {
        highScoreData.highScoresMS.Add(new player(_name, _score));
        highScoreData.highScoresMS.Sort();
        checkForMoreThanMaxScorecount(highScoreData.highScoresMS);
        SaveData();
    }
    public void addPlayerFrogger(string _name, int _score) {
        highScoreData.highScoresFrogger.Add(new player(_name, _score));
        highScoreData.highScoresFrogger.Sort();
        checkForMoreThanMaxScorecount(highScoreData.highScoresFrogger);
        SaveData();
    }
    public void addPlayerSpaceInvaders(string _name, int _score) {
        highScoreData.highScoresSI.Add(new player(_name, _score));
        highScoreData.highScoresSI.Sort();
        checkForMoreThanMaxScorecount(highScoreData.highScoresSI);
        SaveData();
    }
    public void addPlayerPong(string _name, int _score) {
        highScoreData.highScoresPong.Add(new player(_name, _score));
        highScoreData.highScoresPong.Sort();
        checkForMoreThanMaxScorecount(highScoreData.highScoresPong);
        SaveData();
    }
    public void checkForMoreThanMaxScorecount(List<player> gameScores)
    {
        List<player> temp = new List<player>();
        if (gameScores.Count > maxScoreCount)
        {
            // do logic for adding beyond five here!
            // iterate over and then get top five and remove the rest.
            
            for (int i = gameScores.Count - maxScoreCount; i < gameScores.Count; i++)
            {
                temp.Add(gameScores[i]);
            }
            gameScores = temp;
        }
    }

    public void CreateSaveObjects()
    {
        highScoreData = new HighScoreData("/High Score Data");
        dataPath = Application.persistentDataPath + highScoreData.GetFileName() + extenstion;
        LoadData();
    }

    public void SaveData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(dataPath);
        bf.Serialize(file, highScoreData);
        file.Close();
    }

    public void LoadData()
    {
        if (File.Exists(dataPath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(dataPath, FileMode.Open);
            highScoreData = (HighScoreData)bf.Deserialize(file);
            file.Close();
        }
    }
}
