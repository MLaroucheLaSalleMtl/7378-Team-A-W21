using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTrap : MonoBehaviour
{
    [Header("Script References")]
    private HealthBar damage;

    [Header("SpriteRenderer")]
    [SerializeField] private Sprite noSpikes;
    [SerializeField] private Sprite smallSpikes;
    [SerializeField] private Sprite actualSpikes;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Seconds to Wait")]
    private float SpikePrep = 0.3f;
    private float SpikeActive = 1f;
    private float SpikeReactived = 0.5f;

    [Header("Boolean")]
    private bool onlySpikeOnce = false;
    private bool spikesActivatedbool = false;
    private bool onlyOnce = false;

    [Header("Variables")]
    private int spikeTrapDamage = 5;

    private void Start()
    {
        GameObject healthReference = GameObject.FindGameObjectWithTag("Player");
        damage = healthReference.GetComponent<HealthBar>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !onlySpikeOnce)
        {
            onlySpikeOnce = true;
            StartCoroutine(SpikeTrapCoroutine());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && onlyOnce == false && spikesActivatedbool == true)
        {
            damage.TakeDamage(spikeTrapDamage);
            onlyOnce = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        onlyOnce = false;
    }


    private IEnumerator SpikeTrapCoroutine()
    {
        spriteRenderer.sprite = smallSpikes;
        
        yield return new WaitForSeconds(SpikePrep);
        spikesActivatedbool = true;
        spriteRenderer.sprite = actualSpikes;
        
        yield return new WaitForSeconds(SpikeActive);
        spriteRenderer.sprite = noSpikes;
        
        yield return new WaitForSeconds(SpikeReactived);
        onlySpikeOnce = false;
        spikesActivatedbool = false;
    }
}
