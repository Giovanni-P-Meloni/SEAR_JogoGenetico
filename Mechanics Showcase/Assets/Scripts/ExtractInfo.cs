using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtractInfo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 303; i++)
        {
            Generation gen =  SaveSys.LoadGeneration(i);
            float aux = 0;
            for (int j=0; j<gen.population.Length; j++){
                aux += gen.population[j].score;
            }
            aux = aux/gen.population.Length;
            SaveSys.SaveToDataFrame(gen.generation, aux);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
