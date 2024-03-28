using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleton : MonoBehaviour
{
    public static PlayerSingleton Instance { get; private set; }

    public string PlayerName { get; set; }
    public bool hasSound { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            hasSound = true;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }
}
