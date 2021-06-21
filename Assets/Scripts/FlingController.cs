using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlingController : MonoBehaviour
{

    [Header("Fling Settings")]
    public float clickAttractRadius;
    public float flingForceMultiplier;
    public float maxFlingForce;

    [Header("Attractor Settings")]
    private Vector2 attractPoint;
    Collider2D[] thingsInRange;

    public float toVel = 2.5f;
    public float maxVel = 15.0f;
    public float maxForce = 40.0f;
    public float gain = 5f;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            attractPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            thingsInRange = Physics2D.OverlapCircleAll(attractPoint, clickAttractRadius, 1 << 8);

        }
        else if (Input.GetMouseButtonUp(0))
        {
            Collider2D[] thingsInRange = Physics2D.OverlapCircleAll(attractPoint, clickAttractRadius, 1 << 8);

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePos - attractPoint);

            float flingForce = direction.magnitude * flingForceMultiplier;
            flingForce = Mathf.Clamp(flingForce, 0, maxFlingForce);

            Vector2 force = -direction.normalized * flingForce;

            foreach (Collider2D thing in thingsInRange)
            {
                // impulse should be a multiple of the things mass 
                Vector2 individualForce = force * thing.attachedRigidbody.mass;

                thing.GetComponent<Rigidbody2D>().AddForce(individualForce, ForceMode2D.Impulse);
            }

            thingsInRange = null;
        }
    }


    void FixedUpdate()
    {
        if (!Input.GetMouseButton(0))
        {
            thingsInRange = null;
            return;
        }

        if (thingsInRange != null)
        {
            foreach (Collider2D thing in thingsInRange)
            {
                Rigidbody2D rbody = thing.GetComponent<Rigidbody2D>();

                Vector2 dist = attractPoint - (Vector2) thing.transform.position;

                // calc a target vel proportional to distance (clamped to maxVel)
                Vector2 tgtVel = Vector2.ClampMagnitude(toVel * dist, maxVel);
                // calculate the velocity error
                Vector2 error = tgtVel - rbody.velocity;
                // calc a force proportional to the error (clamped to maxForce)
                Vector2 force = Vector2.ClampMagnitude(gain * error * rbody.mass, maxForce);
                rbody.AddForce(force);
            }
        }
    }
}
