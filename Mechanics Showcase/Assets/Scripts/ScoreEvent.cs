using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreEvent : MonoBehaviour
{

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "SCORE: " + 0;
    }
    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable(){
        Player.ScoreChanged += HasScored;
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        Player.ScoreChanged -= HasScored;
    }

    void HasScored(int S){
    GetComponent<TextMeshProUGUI>().text = "SCORE: " + S;
    }
}
