using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thing : MonoBehaviour
{
    public GameObject[] fragments;

    public float points;
    public float health;
    public float radius;

    public static float minimumDamageThreshold = 2;
    public static float damageScale = 2;

    public bool isBroken = false;
    private bool hasRunBreakRoutine = false;

    Rigidbody2D rb;

    public GameObject childCollider;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Error checking 
        if (rb == null) Debug.LogWarning("Invalid rb");
        if (health <= 0) Debug.LogWarning("Invalid health");
        if (radius <= 0) Debug.LogWarning("Invalid radius");
        if (childCollider == null) Debug.LogWarning("Invalid childCollider");
        else childCollider.SetActive(false);

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (rb == null) return;

        Vector2 normal = collision.GetContact(0).normal;
        Vector2 impulse = ComputeTotalImpulse(collision);

        Vector2 myIncidentVelocity = rb.velocity - impulse / rb.mass;

        Vector2 otherIncidentVelocity = Vector2.zero;
        var otherBody = collision.rigidbody;
        if (otherBody != null)
        {
            otherIncidentVelocity = otherBody.velocity;
            if (!otherBody.isKinematic)
                otherIncidentVelocity += impulse / otherBody.mass;
        }

        // Compute how fast each one was moving along the collision normal,
        // Or zero if we were moving against the normal.
        float myApproach = Mathf.Max(0f, Vector3.Dot(myIncidentVelocity, normal));
        float otherApproach = Mathf.Max(0f, Vector3.Dot(otherIncidentVelocity, normal));

        float damage = Mathf.Max(0f, otherApproach - myApproach - minimumDamageThreshold);

        health -= damage / rb.mass;

        if ((health < 0 || isBroken) && !hasRunBreakRoutine)
        {
            isBroken = true;
            hasRunBreakRoutine = true;
            Break();
        }

    }


    private void Break()
    {
        for (int i=0; i < fragments.Length; i++)
        {
            SpawnChild(fragments[i]);
        }
        // Destroy(gameObject);
        gameObject.SetActive(false);
    }

    public void SpawnChild(GameObject fragment)
    {
        // Turn on childCollider 
        childCollider.SetActive(true);

        // Pick a random spawn point in radius of parent 
        float x = Random.Range(-radius, radius);
        float y = Random.Range(-radius, radius);
        Vector2 spawnPoint = new Vector2(x, y) + (Vector2)gameObject.transform.position;

            GameObject fragmentInstance = Instantiate(
                    fragment,
                    spawnPoint,
                    gameObject.transform.rotation,
                    gameObject.transform.parent
                    );

        float velocityVariationX = Random.Range(-radius, radius);
        float velocityVariationY = Random.Range(-radius, radius);

        fragmentInstance.GetComponent<Rigidbody2D>().velocity = rb.velocity + new Vector2(velocityVariationX, velocityVariationY);
    }

    private Vector2 ComputeTotalImpulse(Collision2D collision)
    {
        Vector2 impulse = Vector2.zero;

        int contactCount = collision.contactCount;
        for (int i = 0; i < contactCount; i++)
        {
            var contact = collision.GetContact(0);
            impulse += contact.normal * contact.normalImpulse;
            impulse.x += contact.tangentImpulse * contact.normal.y;
            impulse.y -= contact.tangentImpulse * contact.normal.x;
        }

        return impulse;
    }
}
