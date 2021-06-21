using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAttractor : MonoBehaviour
{

    public static List<GravityAttractor> Attractors;

    public bool gravityEnabled;

    Rigidbody2D rb;


    private void FixedUpdate()
    {
        foreach (GravityAttractor attractor in Attractors)
        {
            if (attractor != this)
                Attract(attractor);
        }
    }

    private void OnEnable()
    {
        if (Attractors == null)
            Attractors = new List<GravityAttractor>();

        Attractors.Add(this);

        rb = GetComponent<Rigidbody2D>();
    }

    private void OnDisable()
    {
        Attractors.Remove(this);
    }

    private void OnDestroy()
    {
        Attractors.Remove(this);
    }

    void Attract (GravityAttractor objectAttractedTo)
    {
        Vector2 attractorPosition = objectAttractedTo.transform.position;
        float attractorMass = objectAttractedTo.rb.mass;

        if (!gravityEnabled)
            return;

        Vector2 direction = rb.position - attractorPosition;
        float distance = direction.magnitude;

        if (distance == 0f)
            return;

        float forceMagnitude = GameManager.Instance.gravitationalConstant * (rb.mass * attractorMass) / Mathf.Pow(distance, 2);
        Vector2 force = -direction.normalized * forceMagnitude;

        rb.AddForce(force);
    }



}
