using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLocation : MonoBehaviour
{
    private PitSpawner pitSpawner;

    private void Start()
    {
        pitSpawner = GameObject.FindGameObjectWithTag("PitSpawner").GetComponent<PitSpawner>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            pitSpawner.lastCheckPointPos = transform.position;
        }
    }
}
