using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpButtonHandler : MonoBehaviour
{
    [Header("Variables")]
    private Vector2 currentHealthRect;
    private Vector2 currentStaminaRect;

    [Header("Texts")]
    [SerializeField] private Text experienceText;
    [SerializeField] private Text healthCostText;
    [SerializeField] private Text staminaCostText;
    [SerializeField] private Text staminaRegenCostText;
    [SerializeField] private Text attackCostText;
    [SerializeField] private Text ultCostText;
    [SerializeField] private Text healthPotionCostText;
    [SerializeField] private Text staminaPotionCostText;
    [SerializeField] private Text damageUpCostText;
    [SerializeField] private Text speedUpCostText;
    [SerializeField] private Text daggerCostText;

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
    [SerializeField] private int experienceCostDaggers = 50;

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

    [Header("Panels")]
    [SerializeField] private GameObject levelUpPanel;

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
        if (Input.GetKeyDown(KeyCode.P))
        {
            experienceText.text = "Score Points: " + score.currentScore.ToString();
        }
    }

    public void OnHealthUp()
    {
        if(score.currentScore >= experienceCostHealth)
        {
            score.currentScore -= experienceCostHealth;
            experienceCostHealth *= 2;

            health.MaxHealth += 25;
            health.CurrentHealth += 25;
            hpSlider.maxValue = health.MaxHealth;
            hpSlider.value = health.CurrentHealth;

            currentHealthRect += new Vector2(25, 0);
            healthRectTransform.sizeDelta = currentHealthRect;

            experienceText.text = "Score Points: " + score.currentScore.ToString();
            healthCostText.text = "Cost: " + experienceCostHealth.ToString();
            score.ScoreText.text = "Score: " + score.currentScore.ToString();
        }
    }

    public void OnStaminaUp()
    {
        if (score.currentScore >= experienceCostStamina)
        {
            score.currentScore -= experienceCostStamina;
            experienceCostStamina *= 2;

            stamina.MaxStamina += 25;
            stamina.CurrentStamina += 25;
            staminaSlider.maxValue = stamina.MaxStamina;
            staminaSlider.value = stamina.CurrentStamina;

            currentStaminaRect += new Vector2(25, 0);
            staminaRectTransform.sizeDelta = currentStaminaRect;

            experienceText.text = "Score Points: " + score.currentScore.ToString();
            staminaCostText.text = "Cost: " + experienceCostStamina.ToString();
            score.ScoreText.text = "Score: " + score.currentScore.ToString();
        }
    }

    public void OnUltCDDown()
    {
        if (score.currentScore >= experienceCostUlt)
        {
            score.currentScore -= experienceCostUlt;
            experienceCostUlt *= 2;

            attack.upgradeUltCd -= 2.5f;

            experienceText.text = "Score Points: " + score.currentScore.ToString();
            ultCostText.text = "Cost: " + experienceCostUlt.ToString();
            score.ScoreText.text = "Score: " + score.currentScore.ToString();
        }     
    }

    public void OnAttackDamageUp()
    {
        if (score.currentScore >= experienceCostAttack)
        {
            score.currentScore -= experienceCostAttack;
            experienceCostAttack *= 2;

            attack.dmg += 1;

            experienceText.text = "Score Points: " + score.currentScore.ToString();
            attackCostText.text = "Cost: " + experienceCostAttack.ToString();
            score.ScoreText.text = "Score: " + score.currentScore.ToString();
        }      
    }

    public void OnIncreaseStaminaRegen()
    {
        if(score.currentScore >= experienceCostRegen && stamina.RegenUpgrade >= 0)
        {
            score.currentScore -= experienceCostRegen;
            experienceCostRegen *= 2;

            stamina.RegenUpgrade -= 15;

            experienceText.text = "Score Points: " + score.currentScore.ToString();
            staminaRegenCostText.text = "Cost: " + experienceCostRegen.ToString();
            score.ScoreText.text = "Score: " + score.currentScore.ToString();
        }
        
    }

    public void OnHealthUpPotion()
    {
        if(potions.hasHealPotion == false && score.currentScore >= experienceCostHealthPotion)
        {
            score.currentScore -= experienceCostHealthPotion;

            potions.numHealthPotion++;
            potions.hasHealPotion = true;

            experienceText.text = "Score Points: " + score.currentScore.ToString();
            healthPotionCostText.text = "Cost: " + experienceCostHealthPotion.ToString();
            score.ScoreText.text = "Score: " + score.currentScore.ToString();
        }
    }

    public void OnStaminaUpPotion()
    {
        if (potions.hasStaminaPotion == false && score.currentScore >= experienceCostStaminaPotion)
        {
            score.currentScore -= experienceCostStaminaPotion;

            potions.numStaminaPotion++;
            potions.hasStaminaPotion = true;

            experienceText.text = "Score Points: " + score.currentScore.ToString();
            staminaPotionCostText.text = "Cost: " + experienceCostStaminaPotion.ToString();
            score.ScoreText.text = "Score: " + score.currentScore.ToString();
        }
    }

    public void OnAttackDamageUpPotion()
    {
        if(potions.hasDamagePotion == false && score.currentScore >= experienceCostAttackPotion)
        {
            score.currentScore -= experienceCostAttackPotion;

            potions.numDamagePotion++;
            potions.hasDamagePotion = true;

            experienceText.text = "Score Points: " + score.currentScore.ToString();
            damageUpCostText.text = "Cost: " + experienceCostAttackPotion.ToString();
            score.ScoreText.text = "Score: " + score.currentScore.ToString();
        }
    }

    public void OnSpeedUpPotion()
    {
        if(potions.hasSpeedPotion == false && score.currentScore >= experienceCostSpeedPotion)
        {
            score.currentScore -= experienceCostSpeedPotion;

            potions.numSpeedPotion++;
            potions.hasSpeedPotion = true;

            experienceText.text = "Score Points: " + score.currentScore.ToString();
            speedUpCostText.text = "Cost: " + experienceCostSpeedPotion.ToString();
            score.ScoreText.text = "Score: " + score.currentScore.ToString();
        }
    }

    public void OnDaggerIncrease()
    {
        if (score.currentScore >= experienceCostDaggers)
        {
            score.currentScore -= experienceCostDaggers;

            attack.projCount++;

            experienceText.text = "Score Points: " + score.currentScore.ToString();
            daggerCostText.text = "Cost: " + experienceCostDaggers.ToString();
            score.ScoreText.text = "Score: " + score.currentScore.ToString();
        }
    }

    public void OnClosePanel()
    {
        levelUpPanel.SetActive(false);
    }
}
