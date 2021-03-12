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

    public static StaminaBar instance;

    private CharacterMovement CanDashBool;

    public int CurrentStamina { get => currentStamina; set => currentStamina = value; }

    private void Awake()
    {
        instance = this;    
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject scriptReference = GameObject.FindGameObjectWithTag("Player");
        CanDashBool = scriptReference.GetComponent<CharacterMovement>();

        CurrentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;
    }

    private void Update()
    {
        if(CurrentStamina >= 20)
        {
            CanDashBool.CanDash1 = true;
        }
        else
        {
            CanDashBool.CanDash1 = false;
        }
    }

    public void UseStamina(int amount)
    {
        if(CurrentStamina - amount >= 0)
        {
            CurrentStamina -= amount;
            staminaBar.value = CurrentStamina;

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
        yield return new WaitForSeconds(1.5f);

        while(CurrentStamina < maxStamina)
        {
            CurrentStamina += maxStamina / 100;
            staminaBar.value = CurrentStamina;
            yield return regeneration;
        }
        regen = null;
    }
}
