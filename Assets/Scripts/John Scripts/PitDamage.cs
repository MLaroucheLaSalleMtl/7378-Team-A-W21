using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitDamage : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private int damageTaken = 10;
    private float speed;
    private Animator anim;

    [Header("Rigidbody")]
    private Rigidbody2D player;

    [Header("Script References")]
    private HealthBar damage;
    private CharacterMovement movement;
    private SaveLocation savedPoint;
    private PitSpawner pitSpawner;

    private void Start()
    {
        GameObject healthReference = GameObject.FindGameObjectWithTag("Player");
        damage = healthReference.GetComponent<HealthBar>();
        GameObject movementReference = GameObject.FindGameObjectWithTag("Player");
        movement = movementReference.GetComponent<CharacterMovement>();
        GameObject characterReference = GameObject.FindGameObjectWithTag("Player");
        player = characterReference.GetComponent<Rigidbody2D>();
        GameObject animatorReference = GameObject.FindGameObjectWithTag("Player");
        anim = animatorReference.GetComponent<Animator>();
        pitSpawner = GameObject.FindGameObjectWithTag("PitSpawner").GetComponent<PitSpawner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            damage.TakeDamage(damageTaken);
            //movement.enabled = false;
            //anim.enabled = false;
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            player.velocity = new Vector2(x * speed, y * speed);
            collision.transform.position = pitSpawner.lastCheckPointPos;
        }
    }
}
