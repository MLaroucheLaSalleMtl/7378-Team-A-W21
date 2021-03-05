using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitSpawner : MonoBehaviour
{
    private static PitSpawner instance;
    public Vector2 lastCheckPointPos;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
