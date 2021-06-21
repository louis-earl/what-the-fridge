using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Gravity Settings")]
    public float gravitationalConstant;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
}
