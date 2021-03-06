using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PotionHandler : MonoBehaviour
{

    [SerializeField]private GameObject player;
    [SerializeField] private GameObject[] potImage;

    public int numHealthPotion = 0;
    public int numStaminaPotion = 0;
    public int numSpeedPotion = 0;
    public int numDamagePotion = 0;

    public bool hasHealPotion = false;
    public bool hasStaminaPotion = false;
    public bool hasSpeedPotion = false;
    public bool hasDamagePotion = false;

    private int healthPotionValue = 60;
    private int staminaPotionValue = 60;
    private float speedPotionValue = 1.5f;
    private int dmgPotionValue = 10;

    private float speedTimer = 5.0f;
    private bool isSpeed = false;
    private float dmgTimer = 5.0f;
    private bool isDmg = false;

    public static PotionHandler instance;

    [Header("Script References")]
    private PauseSettings pause;

    [SerializeField] private AudioClip drinkSound;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GameObject pauseObject = GameObject.FindGameObjectWithTag("Pause"); // John Trihas
        pause = pauseObject.GetComponent<PauseSettings>();
    }

    // Update is called once per frame
    void Update()
    {
        if (speedTimer >= 0 && isSpeed)
        {
            speedTimer -= Time.deltaTime;

        }
        if (dmgTimer >= 0 && isDmg)
        {
            dmgTimer -= Time.deltaTime;

        }
        if (Input.GetButton("HealthPotion") && pause.isPaused == false)
        {
            if(hasHealPotion)
            {
                player.GetComponent<HealthBar>().GiveHealth(healthPotionValue);
                numHealthPotion = 0;
                hasHealPotion = false;
                potImage[0].SetActive(false);
                AudioClipManager.instance.PlayHitSound(drinkSound);

            } 
        }
        else if(Input.GetButton("StaminaPotion") && pause.isPaused == false)
        {
            if (hasStaminaPotion)
            {
                player.GetComponent<StaminaBar>().GiveStamina(staminaPotionValue);
                numStaminaPotion = 0;
                hasStaminaPotion = false;
                potImage[1].SetActive(false);
                AudioClipManager.instance.PlayHitSound(drinkSound);
            }
        }
        else if (Input.GetButton("SpeedPotion") && pause.isPaused == false)
        {
            if (hasSpeedPotion)
            {
                if(speedTimer >= 0)
                {
                    isSpeed = true;
                    player.GetComponent<CharacterMovement>().Speed *= speedPotionValue;
                    numSpeedPotion = 0;
                    hasSpeedPotion = false;
                    potImage[2].SetActive(false);
                    AudioClipManager.instance.PlayHitSound(drinkSound);
                }

            }
        }
        if (speedTimer <= 0)
        {
            player.GetComponent<CharacterMovement>().Speed /= speedPotionValue;
            isSpeed = false;
            speedTimer = 5.0f;
        }
        else if (Input.GetButton("DamagePotion") && pause.isPaused == false)
        {
            if (hasDamagePotion)
            {
                if (dmgTimer >= 0)
                {
                    isDmg = true;
                    player.GetComponent<CharacterAttack>().dmg += dmgPotionValue;
                    numDamagePotion = 0;
                    hasDamagePotion = false;
                    potImage[3].SetActive(false);
                    AudioClipManager.instance.PlayHitSound(drinkSound);
                }


            }
        }
        if (dmgTimer <= 0)
        {
            player.GetComponent<CharacterAttack>().dmg -= dmgPotionValue;
            isDmg = false;
            dmgTimer = 5.0f;
        }
        if (hasHealPotion)
            potImage[0].SetActive(true);
        if (hasStaminaPotion)
            potImage[1].SetActive(true);
        if (hasSpeedPotion)
            potImage[2].SetActive(true);
        if (hasDamagePotion)
            potImage[3].SetActive(true);
    }

}
