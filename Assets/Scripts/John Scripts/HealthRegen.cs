using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRegen : MonoBehaviour
{
    [Header("Script References")]
    private HealthBar health;

    [Header("Variables")]
    [SerializeField] private int healthGiven = 1;
    private float totaltimepassed = 0;

    private void Start()
    {
        GameObject healthReference = GameObject.FindGameObjectWithTag("Player");
        health = healthReference.GetComponent<HealthBar>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            InvokeRepeating("RegeningHealth", 1, 0.1f);   
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CancelInvoke("RegeningHealth");
    }

    private void RegeningHealth()
    {
        health.GiveHealth(healthGiven);
    }


}
