using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
public class Rocket : MonoBehaviour {

    public DNA mDNA;

    private Rigidbody2D rb;
    public static Vector2 target;

    private int genesCounter;

    private void Start()
    {
        mDNA = new DNA();
        target = GameObject.FindGameObjectWithTag("Target").transform.position;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        transform.position = new Vector2(0, -4f);
    }

    public void Run()
    {
        if (genesCounter >= mDNA.genes.Length)
            return;

        rb.AddForce(mDNA.genes[genesCounter]);
        genesCounter++;
    }

    public void CalculateFitness ()
    {
        float distance = Vector2.Distance(transform.position, target);
        mDNA.fitness = 1 / (distance * distance);
    }
}
