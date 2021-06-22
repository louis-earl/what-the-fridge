using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pest : MonoBehaviour
{
    private void OnCollisionExit2D(Collision2D collision)
    {
        gameObject.layer = 8;
    }
}
