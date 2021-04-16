using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDamage : MonoBehaviour
{
    private HealthBar damage;

    [SerializeField] private int arrowDamageTaken = 5;

    // Start is called before the first frame update
    void Start()
    {
        GameObject healthReference = GameObject.FindGameObjectWithTag("Player");
        damage = healthReference.GetComponent<HealthBar>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Destroy(gameObject);
            damage.TakeDamage(arrowDamageTaken);
        }
        else if(collision.tag == "ArrowTrap")
        {

        }
        else
        {
            Destroy(gameObject);
        }
    }
}
