using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potions : MonoBehaviour
{
    enum potionType
    {
        Healing,
        Stamina,
        Speed,
        Damage
    }

    [SerializeField] private potionType typeOfPotion;
    private PotionHandler pot;

    // Start is called before the first frame update
    void Start()
    {
        pot = PotionHandler.instance;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (typeOfPotion == potionType.Healing)
            {
                if (pot.hasStaminaPotion == false)
                {
                    pot.numHealthPotion++;
                    pot.hasHealPotion = true;
                }
                Destroy(this.gameObject);
            }
            else if (typeOfPotion == potionType.Stamina)
            {
                if (pot.hasStaminaPotion == false)
                {
                    pot.numStaminaPotion++;
                    pot.hasStaminaPotion = true;
                }
                Destroy(this.gameObject);
            }
            else if (typeOfPotion == potionType.Speed)
            {
                if (pot.hasStaminaPotion == false)
                {
                    pot.numSpeedPotion++;
                    pot.hasSpeedPotion = true;
                }
                Destroy(this.gameObject);
            }
            else if (typeOfPotion == potionType.Damage)
            {
                if (pot.hasStaminaPotion == false)
                {
                    pot.numDamagePotion++;
                    pot.hasDamagePotion = true;
                }
                Destroy(this.gameObject);
            }
        }
    }
}
