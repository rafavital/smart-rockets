using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA  {

    public Vector2[] genes;

    private float maxForce = 100f;

    public float fitness;

    public DNA ()
    {
        genes = new Vector2[Population.lifeTime];
        for (int i = 0; i < genes.Length; i++)
        {
            genes[i] = Random.insideUnitCircle * Random.Range(0, maxForce);
        }
    }

    public DNA Crossover (DNA partner)
    {
        DNA child = new DNA();

        for (int i = 0; i < child.genes.Length; i++)
        {
            float parentToChoose = Random.value;
            child.genes[i] = parentToChoose > 0.5f ? this.genes[i] : partner.genes[i];
        }

        return child;
    }

    public void Mutate(float mutationRate)
    {
        for (int i = 0; i < genes.Length; i++)
        {
            if (Random.value < mutationRate)
                genes[i] = Random.insideUnitCircle * maxForce;
        }
    }


}
