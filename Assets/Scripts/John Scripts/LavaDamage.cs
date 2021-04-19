using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaDamage : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private float slowSpeed = 200f;
    [SerializeField] private int lavaDamage = 1;

    private bool startTimer = false;
    private bool onlyOnce = true;

    private float timer = 0;
    private float tempSpeed;

    [Header("Script References")]
    private HealthBar damage;
    private CharacterMovement movement;

    private void Start()
    {
        GameObject healthReference = GameObject.FindGameObjectWithTag("Player");
        damage = healthReference.GetComponent<HealthBar>();

        GameObject movementReference = GameObject.FindGameObjectWithTag("Player");
        movement = movementReference.GetComponent<CharacterMovement>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && onlyOnce == true)
        {
            tempSpeed = movement.Speed;
            movement.Speed -= slowSpeed;
            InvokeRepeating("TakeLavaDamage", 0.5f, 0);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player" && onlyOnce == true)
        {
            onlyOnce = false;
            startTimer = true;
        }
    }

    private void Update()
    {
        if (startTimer == true)
        {
            timer += Time.deltaTime;
            if(timer >= 2)
            {
                movement.Speed = tempSpeed;
                CancelInvoke("TakeLavaDamage");
                startTimer = false;
                timer = 0;
                onlyOnce = true;
            }
        }
    }

    private void TakeLavaDamage()
    {
        damage.TakeDamage(lavaDamage);
    }
}
