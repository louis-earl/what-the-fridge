using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalReceiver : MonoBehaviour
{
    public bool isPowered;
    public List<ElectricalEffector> effectors;

    private int index = 0;

    private void FixedUpdate()
    {
        if (effectors.Count > 0)
        {
            // Reset index if necessary
            if (index >= effectors.Count)
            {
                index = 0;
            }

            // Draw power and apply
            if (effectors[index].Power())
            {
                isPowered = true;
            }
            else isPowered = false;

            index++;    
        }
        else isPowered = false;
 
    }
}
