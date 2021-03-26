using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpButtonHandler : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private Text experienceText; 
    [SerializeField] private int scoreDivider = 2;
    [SerializeField] private int endLevelScore;
    private Vector2 currentHealthRect;
    private Vector2 currentStaminaRect;

    [Header("Experience Costs")]
    [SerializeField] private int experienceCostHealth = 100;
    [SerializeField] private int experienceCostStamina = 100;
    [SerializeField] private int experienceCostUlt = 100;
    [SerializeField] private int experienceCostAttack = 100;
    [SerializeField] private int experienceCostRegen = 100;
    [SerializeField] private int experienceCostHealthPotion = 50;
    [SerializeField] private int experienceCostStaminaPotion = 50;
    [SerializeField] private int experienceCostAttackPotion = 50;
    [SerializeField] private int experienceCostSpeedPotion = 50;

    [Header("Sciprt References")]
    private ScoreAdded score;
    private HealthBar health;
    private CharacterAttack attack;
    private StaminaBar stamina;
    private PotionHandler potions;

    [Header("Sliders")]
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Slider staminaSlider;
    [SerializeField] private RectTransform healthRectTransform;
    [SerializeField] private RectTransform staminaRectTransform;

    // Start is called before the first frame update
    void Start()
    {
        GameObject scoreObject = GameObject.FindGameObjectWithTag("Score");
        score = scoreObject.GetComponent<ScoreAdded>();

        GameObject healthUp = GameObject.FindGameObjectWithTag("Player");
        health = healthUp.GetComponent<HealthBar>();

        GameObject staminaUp = GameObject.FindGameObjectWithTag("Player");
        stamina = staminaUp.GetComponent<StaminaBar>();

        GameObject UltCD = GameObject.FindGameObjectWithTag("Player");
        attack = UltCD.GetComponent<CharacterAttack>();

        GameObject potionsScript = GameObject.FindGameObjectWithTag("Player");
        potions = potionsScript.GetComponent<PotionHandler>();

        currentHealthRect = healthRectTransform.sizeDelta;
        currentStaminaRect = staminaRectTransform.sizeDelta;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            DivideScore();
            experienceText.text = "Experience Points: " + endLevelScore.ToString();
        }
    }

    public void DivideScore()
    {
        endLevelScore = score.currentScore;
        endLevelScore /= scoreDivider;
    }

    public void OnHealthUp()
    {
        if(endLevelScore >= experienceCostHealth)
        {
            endLevelScore -= experienceCostHealth;
            experienceCostHealth *= 2;

            health.MaxHealth += 25;
            health.CurrentHealth += 25;
            hpSlider.maxValue = health.MaxHealth;
            hpSlider.value = health.CurrentHealth;

            currentHealthRect += new Vector2(25, 0);
            healthRectTransform.sizeDelta = currentHealthRect;

            experienceText.text = "Experience Points: " + endLevelScore.ToString();
        }
    }

    public void OnStaminaUp()
    {
        if (endLevelScore >= experienceCostStamina)
        {
            endLevelScore -= experienceCostStamina;
            experienceCostStamina *= 2;

            stamina.MaxStamina += 25;
            stamina.CurrentStamina += 25;
            staminaSlider.maxValue = stamina.MaxStamina;
            staminaSlider.value = stamina.CurrentStamina;

            currentStaminaRect += new Vector2(25, 0);
            staminaRectTransform.sizeDelta = currentStaminaRect;

            experienceText.text = "Experience Points: " + endLevelScore.ToString();
        }
    }

    public void OnUltCDDown()
    {
        if (endLevelScore >= experienceCostUlt)
        {
            endLevelScore -= experienceCostUlt;
            experienceCostUlt *= 2;

            attack.upgradeUltCd -= 2.5f;

            experienceText.text = "Experience Points: " + endLevelScore.ToString();
        }     
    }

    public void OnAttackDamageUp()
    {
        if (endLevelScore >= experienceCostAttack)
        {
            endLevelScore -= experienceCostAttack;
            experienceCostAttack *= 2;

            attack.dmg += 1;

            experienceText.text = "Experience Points: " + endLevelScore.ToString();
        }      
    }

    public void OnReduceStaminaRegenTimer()
    {
        if(endLevelScore >= experienceCostRegen)
        {
            endLevelScore -= experienceCostRegen;
            experienceCostRegen *= 2;

            stamina.RegenTimer -= 0.25f;

            experienceText.text = "Experience Points: " + endLevelScore.ToString();
        }
        
    }

    public void OnHealthUpPotion()
    {
        if(potions.hasHealPotion == false && endLevelScore >= experienceCostHealthPotion)
        {
            endLevelScore -= experienceCostHealthPotion;

            potions.numHealthPotion++;
            potions.hasHealPotion = true;

            experienceText.text = "Experience Points: " + endLevelScore.ToString();
        }
    }

    public void OnStaminaUpPotion()
    {
        if (potions.hasStaminaPotion == false && endLevelScore >= experienceCostStaminaPotion)
        {
            endLevelScore -= experienceCostStaminaPotion;

            potions.numStaminaPotion++;
            potions.hasStaminaPotion = true;

            experienceText.text = "Experience Points: " + endLevelScore.ToString();
        }
    }

    public void OnAttackDamageUpPotion()
    {
        if(potions.hasDamagePotion == false && endLevelScore >= experienceCostAttackPotion)
        {
            endLevelScore -= experienceCostAttackPotion;

            potions.numDamagePotion++;
            potions.hasDamagePotion = true;

            experienceText.text = "Experience Points: " + endLevelScore.ToString();
        }
    }

    public void OnSpeedUpPotion()
    {
        if(potions.hasSpeedPotion == false && endLevelScore >= experienceCostSpeedPotion)
        {
            endLevelScore -= experienceCostSpeedPotion;

            potions.numSpeedPotion++;
            potions.hasSpeedPotion = true;

            experienceText.text = "Experience Points: " + endLevelScore.ToString();
        }
    }
}
