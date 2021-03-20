using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public static PotionHandler instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("HealthPotion"))
        {
            if(hasHealPotion)
            {
                player.GetComponent<HealthBar>().GiveHealth(healthPotionValue);
                numHealthPotion = 0;
                hasHealPotion = false;
                potImage[0].SetActive(false);

            } 
        }
        if(Input.GetButton("StaminaPotion"))
        {
            if (hasStaminaPotion)
            {
                player.GetComponent<StaminaBar>().GiveStamina(staminaPotionValue);
                numStaminaPotion = 0;
                hasStaminaPotion = false;
                potImage[1].SetActive(false);
            }
        }
        if(hasHealPotion)
            potImage[0].SetActive(true);
        if (hasStaminaPotion)
            potImage[1].SetActive(true);
        if (hasSpeedPotion)
            potImage[2].SetActive(true);
        if (hasDamagePotion)
            potImage[3].SetActive(true);
    }

}
