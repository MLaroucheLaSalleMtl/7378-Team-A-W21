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
    // private int requirmentCount = 0;
    [SerializeField] public GameObject shield;
    public int shieldDrop = 2;

    //public static NecromancerSpells instance;
    /*private void Awake()
    {
        instance = this;
    }*/

    // Start is called before the first frame update
    void Start()
    {
        //summonCounter = summonCounterMax;
    }

    // Update is called once per frame
    void Update()
    {
        /*if(undeadCount < 2)
        {
            SummonUndead();
        }*/

        /*if(requirmentCount >= 2)
        {
            SummonBoss();
            requirmentCount = 0;
        }*/

        /*if(GameObject.FindGameObjectWithTag("Berzerk").GetComponent<BerzerkerBehaviour>().Hp <= 0)
        {
            //undeadCount--;
            requirmentCount++;
        }*/

        if(shieldDrop <= 0)
        {
            shield.SetActive(false);
            gameObject.GetComponent<NecromancerBehaviour>().isShielded = false;
        }

        /*if(Boss.GetComponent<BerzerkerBehaviour>().Hp <= 0)
        {
            shieldDrop--;
        }*/

        /*if(Input.GetKeyDown(KeyCode.P))
        {
            shieldDrop -= 2;
        }*/
    }

    private void SummonUndead()
    {
        if(summonCounter <= 0)
        {
            Vector3 summon1 = new Vector3(0, 5, 0);
            Instantiate(undead, gameObject.transform.position - summon1, Quaternion.identity);
            undeadCount++;
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
            Vector3 summon1 = new Vector3(0, 5, 0);
            Instantiate(Boss, gameObject.transform.position - summon1, Quaternion.identity);
            //undeadCount++;
            summonCounter = summonCounterMax;
        }
        else if (summonCounter > 0)
        {
            summonCounter -= Time.deltaTime;
        }
    }
}
