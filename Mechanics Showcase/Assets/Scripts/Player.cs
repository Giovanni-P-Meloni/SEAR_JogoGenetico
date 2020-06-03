using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public delegate void ScoreAction(int S);
    public static event ScoreAction ScoreChanged;
    private static int score = 0;

    public static int Score{
        
        get{
            return score;
        }
    
        set{
            score = value;
            ScoreChanged(score);
        }
    }

}
