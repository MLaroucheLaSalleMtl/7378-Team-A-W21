using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidDamage : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private float slowSpeed = 100f;
    [SerializeField] private float normalSpeed = 300f;
    [SerializeField] private int poisonDamage = 1;
    private float totaltimepassed = 0;


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



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            movement.speed = slowSpeed;
            totaltimepassed += Time.deltaTime;
            if(totaltimepassed > 0.2)
            {
                damage.TakeDamage(poisonDamage);
                totaltimepassed = 0;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            movement.speed = normalSpeed;
        }
    }
}
