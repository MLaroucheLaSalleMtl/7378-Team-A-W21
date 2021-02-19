using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    [SerializeField] private Slider staminaBar;

    private int maxStamina = 100;
    private int currentStamina;

    private WaitForSeconds regeneration = new WaitForSeconds(0.1f);
    private Coroutine regen;

    public bool CanDash = true;

    public static StaminaBar instance;

    private CharacterMovement CanDashBool;

    private void Awake()
    {
        instance = this;    
    }

    // Start is called before the first frame update
    void Start()
    {
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;
    }

    public void UseStamina(int amount)
    {
        if(currentStamina - amount >= 0)
        {
            currentStamina -= amount;
            staminaBar.value = currentStamina;

            if(regen != null)
            {
                StopCoroutine(regen);
            }

            regen = StartCoroutine(regenStamina());
        }
        else
        {
            
        }
    }

    private IEnumerator regenStamina()
    {
        yield return new WaitForSeconds(2);

        while(currentStamina < maxStamina)
        {
            currentStamina += maxStamina / 100;
            staminaBar.value = currentStamina;
            yield return regeneration;
        }
        regen = null;
    }
}