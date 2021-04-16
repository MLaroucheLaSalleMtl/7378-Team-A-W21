using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float currentHealth = 100f;
    [SerializeField] private float maxHealth = 100f;

    [Header("Variables")]
    private float speed;
    [SerializeField] private int removePoints = -5;

    [Header("Slider")]
    [SerializeField] private Slider healthBar;

    [Header("Settings")]
    private Rigidbody2D player;
    public static HealthBar instance;
    private ScoreAdded score;
    //private Collider2D playerCollider;
    //private SpriteRenderer sprite;
    //private CharacterMovement movement;
    //private CharacterAttack attack;
    //private CharacterProjectile projectile;

    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }

    private void Awake()
    {
        instance = this;
        CurrentHealth = currentHealth;
        MaxHealth = maxHealth;
    }

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;

        //player = GetComponent<Rigidbody2D>();
        //playerCollider = GetComponent<Collider2D>();
        //sprite = GetComponent<SpriteRenderer>();
        //movement = GetComponent<CharacterMovement>();
        //attack = GetComponent<CharacterAttack>();
        //projectile = GetComponent<CharacterProjectile>();

        GameObject scoreObject = GameObject.FindGameObjectWithTag("Score");
        score = scoreObject.GetComponent<ScoreAdded>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = CurrentHealth;
    }

    public void TakeDamage(int damage)
    {
        if(CurrentHealth <= 0)
        {
            return;
        }

        CurrentHealth -= damage;
        healthBar.value = CurrentHealth;
        CamShake.instance.ShakeCam(3f, 0.05f);
        score.GainScore(removePoints);

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void GiveHealth(int health)
    {
        if(CurrentHealth < MaxHealth)
        {
            CurrentHealth += health;
            healthBar.value = CurrentHealth;
        }
    }

    private void Die()
    {
        SceneManager.LoadScene(sceneBuildIndex: 6);

        if (player != null)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            player.velocity = new Vector2(x * speed, y * speed);

            //playerCollider.enabled = false;
            //sprite.enabled = false;
            //movement.enabled = false;
            //attack.enabled = false;
            //projectile.enabled = false;
        }   
    }
}
