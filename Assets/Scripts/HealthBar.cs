using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float currentHealth = 100f;
    [SerializeField] private float maxHealth = 100f;

    [Header("Destroy Object")]
    [SerializeField] private bool destroyObject;

    [Header("Slider")]
    [SerializeField] private Slider healthBar;

    [Header("Settings")]
    private Rigidbody2D player;
    private Collider2D playerCollider;
    private SpriteRenderer sprite;
    private CharacterMovement movement;
    private CharacterAttack attack;
    private CharacterProjectile projectile;
    public static HealthBar instance;

    public float CurrentHealth { get; set; }

    private void Awake()
    {
        instance = this;
        CurrentHealth = currentHealth;
    }

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;

        player = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        movement = GetComponent<CharacterMovement>();
        attack = GetComponent<CharacterAttack>();
        projectile = GetComponent<CharacterProjectile>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        if(CurrentHealth <= 0)
        {
            return;
        }

        CurrentHealth -= damage;
        healthBar.value = CurrentHealth;

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if(player != null)
        {
            playerCollider.enabled = false;
            sprite.enabled = false;
            movement.enabled = false;
            attack.enabled = false;
            projectile.enabled = false;
        }

        if(destroyObject)
        {
            DestroyObject();
        }
        
    }
 
    private void DestroyObject()
    {
        gameObject.SetActive(false);
    }
    

}
