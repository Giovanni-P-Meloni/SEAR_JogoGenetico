using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Individual{
    public float[] angles;
    public int score;
    public Individual(int angLenght){
        angles = new float[angLenght];
        for (int i = 0; i < angles.Length; i++)
        {   
            angles[i] = 0;
        }
        score = 0;
    }
    public Individual(float[] ang){
        angles = new float[ang.Length];
        for (int i = 0; i < angles.Length; i++)
        {
            angles[i] = ang[i];
        }
        score = 0;
    }
    public Individual(float[] ang, int sco){
        angles = new float[ang.Length];
        for (int i = 0; i < angles.Length; i++)
        {
            angles[i] = ang[i];
        }
        score = sco;
    }
};
[System.Serializable]
public class Generation{
    public Individual[] population;
    public int generation;

    public Generation(Individual[] pop, int gen){
        population = pop;
        generation = gen;
    }
}