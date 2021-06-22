using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : MonoBehaviour
{
    public GameObject IcePrefab;

    [Range(0,1)]
    public float spawnChance;

    private ElectricalReceiver electricalReceiver;
    private Thing thing;

    void Start()
    {
        electricalReceiver = gameObject.GetComponent<ElectricalReceiver>();
        thing = gameObject.GetComponent<Thing>();
    }

    void FixedUpdate()
    {
        if (electricalReceiver.isPowered)
        {
            thing.SpawnChild(IcePrefab);
        }

        /*
        int angle = Random.Range(0, 360);
        Vector2 unitVector = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        Vector2 outerPoint = (unitVector * 10) + (Vector2)transform.position;

        // Cast a ray in random rotation 
        RaycastHit2D hit = Physics2D.Raycast(outerPoint, -unitVector);

        // If it hits something...
        if (hit.collider != null)
        {
            Debug.DrawRay(hit.point, unitVector, Color.red, 2f);
            GameObject fragmentInstance = Instantiate(
                    IcePrefab,
                    hit.point,
                    gameObject.transform.rotation,
                    gameObject.transform.parent
                    );
        }
        */

    }
}
