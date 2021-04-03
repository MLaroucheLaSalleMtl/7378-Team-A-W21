using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerSpells : MonoBehaviour
{
    [SerializeField] public GameObject undead;
    [SerializeField] public GameObject Boss; 
    [SerializeField] public float summonCounterMax;
    private float summonCounter;
    private int undeadCount = 0;
    private int requirmentCount = 0;
    private int shieldDrop = 1;

    // Start is called before the first frame update
    void Start()
    {
        summonCounter = summonCounterMax;
    }

    // Update is called once per frame
    void Update()
    {
        if(undeadCount < 2)
        {
            SummonUndead();
        }

        if(requirmentCount >= 3)
        {
            SummonBoss();
        }

        if(GameObject.FindGameObjectWithTag("Berzerk").GetComponent<BerzerkerBehaviour>().Hp <= 0)
        {
            undeadCount--;
        }

        if(shieldDrop <= 0)
        {
            GameObject.FindGameObjectWithTag("Shield").GetComponent<CircleCollider2D>().enabled = false;
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            shieldDrop -= 2;
        }
    }

    private void SummonUndead()
    {
        if(summonCounter <= 0)
        {
            Vector3 summon1 = new Vector3(0, -5, 0);
            Instantiate(undead, summon1, transform.rotation);
            undeadCount++;
            requirmentCount++;
            summonCounter = summonCounterMax;
        }
        else if(summonCounter > 0)
        {
            summonCounter -= Time.deltaTime;
        }
    }

    private void SummonBoss()
    {
        if (summonCounter <= 0)
        {
            Vector3 summon1 = new Vector3(0, -5, 0);
            Instantiate(Boss, summon1, transform.rotation);
            undeadCount++;
            shieldDrop++;
            summonCounter = summonCounterMax;
        }
        else if (summonCounter > 0)
        {
            summonCounter -= Time.deltaTime;
        }
    }
}
