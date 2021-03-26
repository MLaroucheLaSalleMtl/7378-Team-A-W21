using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerSpells : MonoBehaviour
{
    [SerializeField] public GameObject undead;
    [SerializeField] public float summonCounterMax;
    private float summonCounter;
    private int undeadCount;

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
    }

    private void SummonUndead()
    {
        if(summonCounter <= 0)
        {
            Vector3 summon1 = new Vector3(0, -5, 0);
            Instantiate(undead, summon1, transform.rotation);
            undeadCount++;
            summonCounter = summonCounterMax;
        }
        else if(summonCounter > 0)
        {
            summonCounter -= Time.deltaTime;
        }
    }
}
