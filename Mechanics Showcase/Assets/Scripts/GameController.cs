using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameController
{
    public static void RestartLevel(string levelnumber){
        GameObject level = GameObject.Find(levelnumber);

        foreach (Transform child in level.transform)
        {
            child.gameObject.SetActive(true);
        }
        Player.Score = 0;
    }
}
