using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    [SerializeField] private Slider staminaBar;

    private int maxStamina = 100;
    private int currentStamina;
    private float regenTimer = 1.5f;
    [SerializeField] private int regenUpgrade = 100;

    public WaitForSeconds regeneration = new WaitForSeconds(0.1f);
    private Coroutine regen;

    public static StaminaBar instance;

    private CharacterMovement CanDashBool;

    public int CurrentStamina { get; set; }
    public int MaxStamina { get; set; }
    public float RegenTimer { get; set; }
    public int RegenUpgrade { get => regenUpgrade; set => regenUpgrade = value; }

    private void Awake()
    {
        instance = this;
        CurrentStamina = currentStamina;
        MaxStamina = maxStamina;
        RegenTimer = regenTimer;
        RegenUpgrade = regenUpgrade;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject scriptReference = GameObject.FindGameObjectWithTag("Player");
        CanDashBool = scriptReference.GetComponent<CharacterMovement>();

        CurrentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = currentStamina;
    }

    private void Update()
    {
        staminaBar.value = CurrentStamina;

        if (CurrentStamina >= 20)
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
        if (CurrentStamina - amount >= 0)
        {
            CurrentStamina -= amount;
            staminaBar.value = CurrentStamina;

            if (regen != null)
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
        yield return new WaitForSeconds(RegenTimer);

        while (CurrentStamina < MaxStamina)
        {
            CurrentStamina += MaxStamina / regenUpgrade;
            staminaBar.value = CurrentStamina;
            yield return regeneration;
        }
        regen = null;
    }

    public void GiveStamina(int amount)
    {
        if (CurrentStamina < MaxStamina)
        {
            CurrentStamina += amount;
            staminaBar.value = CurrentStamina;
        }
    }
}
