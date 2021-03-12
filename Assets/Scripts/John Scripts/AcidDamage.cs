using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidDamage : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private float slowSpeed = 100f;
    [SerializeField] private float normalSpeed = 300f;
    [SerializeField] private int poisonDamage = 1;


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
        if (collision.gameObject.tag == "Player")
        {
            movement.speed = slowSpeed;
            InvokeRepeating("PoisonDamage", 1f, 0.25f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            movement.speed = normalSpeed;
            CancelInvoke("PoisonDamage");
        }
    }

    private void PoisonDamage()
    {
        damage.TakeDamage(poisonDamage);
    }
}
