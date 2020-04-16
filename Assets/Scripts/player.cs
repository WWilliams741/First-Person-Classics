using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class player
{
    public string name;
    public int score;

    public player(string _name, int _score)
    {
        name = _name;
        score = _score;
    }
    public int CompareTo(player other)
    {
        if (other == null)
        {
            return 1;
        }

        //Return the difference in score.
        return other.score - score;
    }
}
