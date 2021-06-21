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
    }
}
