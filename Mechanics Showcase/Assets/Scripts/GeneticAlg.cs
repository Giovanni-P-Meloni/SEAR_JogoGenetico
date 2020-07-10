using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GeneticAlg : MonoBehaviour
{
    //Publics
    [HideInInspector]
    public string atLevel = "Level 1"; //FIX ME: Checar LvlChoice.cs
    [Header("GA Variables")]
    public int popsize = 10;
    public float initital_mutationChance = .5f;
    public float mutationIncreaseRate = .5f;
    public int shotQuantity = 3;
    public int predationRate = 5;
    public Individual[] population;
    [HideInInspector]
    public bool undergoingDraw;
    [HideInInspector]
    public Individual currentIndividual;
    [HideInInspector]
    public bool individualReady;

    //Privates
    private float mutationChance;
    private int predationCounter;
    private int currentIndividualIndex; //counter
    private int currentGeneration;
    private int bestGeneration;
    private Individual bestOfBest; //Best individual of the best generation
    [SerializeField]
    private bool resetExperiment=false;

    // Start is called before the first frame update
    void Start()
    {
        Setup();
        //Debug.Log(population[Random.Range(0,299)].angle);
    }

    //Initializing the population
    public void Setup(){
        

        int lastBestGen = SaveSys.CheckLastBestGen();//Check to see if there already is a generation

        if (!resetExperiment && lastBestGen >= 0){
            Debug.Log("Loading previous generation...");
            //FIXME: Sempre carregar a ultima geracao, porem carregar o numero da ultima melhor geracao
            Generation gen = SaveSys.LoadGeneration(lastBestGen);
            Individual[] aux = gen.population;
            popsize = aux.Length;
            population = new Individual[popsize];

            for (int i = 0; i < population.Length; i++)
            {
                population[i] = new Individual(aux[i].angles, aux[i].score);
            }
            currentGeneration = lastBestGen;
            bestGeneration = lastBestGen;
            bestOfBest = GetBestIndividual(aux);
        }
        else{
            Debug.Log("So it begins anew...");
            population = new Individual[popsize];

            for (int i = 0; i < population.Length; i++)
            {
                float[] auxAngles = new float[shotQuantity];    
                for (int j = 0; j < auxAngles.Length; j++)
                {
                    auxAngles[j] = UnityEngine.Random.Range(-89f, 89f);
                }
                population[i] = new Individual(auxAngles);
            }

            currentGeneration = 0;
            bestGeneration = 0;
            bestOfBest = new Individual(shotQuantity);
        }

        predationCounter = 0;
        mutationChance = initital_mutationChance;
        currentIndividualIndex = 0;
        currentIndividual = population[currentIndividualIndex];
        individualReady = true;
        undergoingDraw = false;
    }

    //Evaluate fitness and choose next generation
    public void Draw(){
        this.undergoingDraw = true;

        Individual bestOfGen = new Individual(shotQuantity);
       
        //Sorting the array....
        Array.Sort(population, delegate(Individual x, Individual y){
            return x.score.CompareTo(y.score); 
        });
        

        bestOfGen = GetBestIndividual(population);//REDUNDANT
        

        bool isCurrentGenBetter = false;
        if (bestOfBest.score < bestOfGen.score){
            predationCounter = 0;
            SaveSys.SaveToDataFrame(currentGeneration, bestOfGen.score, mutationChance);
            isCurrentGenBetter = true;
            mutationChance = initital_mutationChance;
            bestOfBest = bestOfGen;
            
        }
        else{
            predationCounter++;
            SaveSys.SaveToDataFrame(currentGeneration, bestOfBest.score, mutationChance);
            Debug.Log("Last Generation was better");
            mutationChance += mutationIncreaseRate; //keep multiplying by 2, until it finds a better gen
            
        }

        SaveStatus(isCurrentGenBetter, currentGeneration);//Saving the result of this iteration
        NextGeneration(bestOfBest);
        this.undergoingDraw = false;
        
    }

    /// Update is called every frame, if the MonoBehaviour is enabled.
    void Update()
    {
       GetControl();//Ai is always trying to get control
    }

    //Getting the control after the destruction of the projectile
    void GetControl(){
        // Debug.Log(this.individualReady + " " + ProjectBHV.projSpawned + " " + this.undergoingDraw);
        if(!this.individualReady && !ProjectBHV.projSpawned && !this.undergoingDraw){//AI receives Control
            //Debug.Log("AI got Control");
            population[currentIndividualIndex].score = Player.Score; //Register the score
            //Debug.Log(currentIndividualIndex + " " + population.Length);
            currentIndividualIndex++;
            if (currentIndividualIndex >= popsize) {
                Debug.Log("Reached the end of population " +  currentGeneration);
                
                Draw();//if all individuals have been evaluated, then it is time to compute

                //Preparing basic variables for next iteration
                currentIndividualIndex = 0;
                currentGeneration++;
            }
            
            currentIndividual = population[currentIndividualIndex];
            GameController.RestartLevel(atLevel);
            this.individualReady = true;
        }

    }

    //Saving the current generation
    void SaveStatus(bool betterGenFound, int genNumber){
        SaveSys.SaveGeneration(population, genNumber);
        if (betterGenFound) SaveSys.SaveLastBestGen(genNumber);
    }   

    //Getting the best Inidividual in the current genertion based solely on score
    public virtual Individual GetBestIndividual(Individual[] pop){
        
        Individual bestIndividual = new Individual(shotQuantity);
        /*foreach (Individual indiv in pop)
        {
            if (indiv.score >= bestIndividual.score){
                bestIndividual = indiv;
            }
        }*/
        bestIndividual = pop[pop.Length-1];
        Debug.Log("Best individual score was: " + bestIndividual.score);
        return bestIndividual;
    }

    //Generating the next population
    public virtual void NextGeneration(Individual chosenIndividual){
        Individual[] auxPopulation = new Individual[population.Length];
        int j=0;
        float[] auxAngles = new float[shotQuantity];

        Debug.Log("Chosen individual is " + chosenIndividual.score);
        foreach (Individual indiv in population)
        {       
            for (int i = 0; i < shotQuantity; i++){
                //Debug.Log("(indiv angle + best angle) / 2 = nextindiv angle : " + indiv.angles[i] + " + " + chosenIndividual.angles[i] + " = " + (indiv.angles[i] +  chosenIndividual.angles[i])  /2);
                auxAngles[i] = (indiv.angles[i] + chosenIndividual.angles[i]) / 2 ;//Crossover
                if (UnityEngine.Random.Range(0f, 100f) <= mutationChance){ 
                    //Debug.Log("Mutation occurred");
                    auxAngles[i] = (chosenIndividual.angles[i] + 3*UnityEngine.Random.Range(-89f,89f))/4;
                }
            }

            auxPopulation[j] = new Individual(auxAngles, 0);
            j++;
        }
        population = auxPopulation;
        if (predationCounter >= predationRate) Predation();
    }

    private void Predation(){
        Debug.Log("Predation Occurred");
        for (int i=0; i<2; i++){
            for (int j=0; j<shotQuantity; j++){
                 population[i].angles[j] = UnityEngine.Random.Range(-89f, 89f);
            }
        }
        predationCounter = 0;
    }
}