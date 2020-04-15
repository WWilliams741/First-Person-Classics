using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class HighScoreData
{
    private string fileName;
    public List<player> highScoresMS = new List<player>(); //minesweeper
    public List<player> highScoresFrogger = new List<player>(); //frogger
    public List<player> highScoresSI = new List<player>(); //space invaders
    public List<player> highScoresPong = new List<player>(); //pong

    public HighScoreData(string path)
    {
        fileName = path;
    }

    public string GetFileName()
    {
        return fileName;
    }
}
