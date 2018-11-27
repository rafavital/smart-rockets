using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population : MonoBehaviour {

    [SerializeField] private GameObject rocketPrefab;

    private Rocket[] population = new Rocket[100];
    private float mutationRate = 0.01f;
    private List<Rocket> matingPool = new List<Rocket> ();
    public int generations;

    private float lifeCounter;
    public static int lifeTime = 500;

    private void Start()
    {
        for (int i = 0; i < population.Length; i++)
        {
            population[i] = Instantiate(rocketPrefab).GetComponent<Rocket>();
        }
    }

    private void Update()
    {
        if (lifeCounter < lifeTime)
        {
            Live();
            lifeCounter++;
        } else
        {
            lifeCounter = 0;
            Fitness();
            Mate();
        }
    }

    private void Live ()
    {
        for (int i = 0; i < population.Length; i++)
        {
            population[i].Run();
        }
    }

    public void Fitness ()
    {
        for (int i = 0; i < population.Length; i++)
        {
            
            population[i].CalculateFitness();

            int n = (int) population[i].mDNA.fitness * 100;

            for (int j = 0; j < n; j++)
            {
                matingPool.Add(population[i]);
            }
        }
    }
    
    public void Mate ()
    {
        //Cria uma nova população
        Rocket[] newPopulation = new Rocket[population.Length];

        for (int i = 0; i < population.Length; i++)
        {
            //Reproduz os individuos da atual população e os coloca na nova população
            int a = Random.Range(0, matingPool.Count);
            int b = Random.Range(0, matingPool.Count);

            Rocket parentA = matingPool[a];
            Rocket parentB = matingPool[b];

            Rocket childRocket = new Rocket();
            childRocket.mDNA = parentA.mDNA.Crossover(parentB.mDNA);
            childRocket.mDNA.Mutate(mutationRate);
            newPopulation[i] = childRocket;
        }

        for (int i = 0; i < population.Length; i++)
        {
            //Destroi os individuos da atual população
            Destroy(population[i].gameObject);
            print("Destrui a população");
            //Cria novos individuos com os atributos da nova população
            population[i] = Instantiate(rocketPrefab).GetComponent<Rocket>();
            population[i] = newPopulation[i];
        }
        generations++;


    }


}
