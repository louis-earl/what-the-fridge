using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalEffector : MonoBehaviour
{
    public float radius;
    public int charge;
    public bool unlimitedCharge;

    void Start()
    {
        gameObject.GetComponent<CircleCollider2D>().radius = radius;

        // Error checking
        if (radius <= 0) Debug.LogWarning("Invalid radius");
        if (charge <= 0) Debug.LogWarning("Invalid charge");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ElectricalReceiver electricalReceiver = collision.gameObject.GetComponent<ElectricalReceiver>();

        if (electricalReceiver == null) return;

        // Only add self once
        if (!electricalReceiver.effectors.Contains(this))
        {
            electricalReceiver.effectors.Add(this);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        ElectricalReceiver electricalReceiver = collision.gameObject.GetComponent<ElectricalReceiver>();

        if (electricalReceiver == null) return;

        electricalReceiver.effectors.Remove(this);
    }

    public bool Power()
    {
        if (charge > 0)
        {
            charge--;
            return true;
        }
        return false;
    }
}
