using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterSettings : MonoBehaviour
{

    [SerializeField] GameObject settingsCanvas;

    // Start is called before the first frame update
    void Start()
    {
        //settingsCanvas = GetComponentInChildren<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PauseSettings.instance.PauseGame();
        }
    }
}
