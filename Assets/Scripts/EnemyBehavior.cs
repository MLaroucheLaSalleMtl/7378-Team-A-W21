using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [Header("Player Options:")]
    [SerializeField] public float speed;
    [Space]
    [Header("Patrol Options:")]
    [SerializeField] public float patrolSpeed;
    [SerializeField] public Transform[] patrolPoint;
    [SerializeField] public float patrolCounter;
    private float patrolWait;
    private int points = 1;
    [Space]
    [Header("Pursuit Options:")]
    [SerializeField] public float pursuitSpeed;
    [SerializeField] public float playerFoundDistance;
    [SerializeField] public float stopDistance;
    [SerializeField] public float abandonDistance;
    [SerializeField] public GameObject melee;

    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        patrolWait = patrolCounter;
    }

    // Update is called once per frame
    void Update()
    {
        OnPursuit();
    }

    void OnPatrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, patrolPoint[points].position, patrolSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, patrolPoint[points].position) < 0.2f)
        {
            if(patrolWait <= 0)
            {
                points++;

                if (points >= patrolPoint.Length)
                {
                    points = 0;
                }

                patrolWait = patrolCounter;
            }
            else
            {
                patrolWait -= Time.deltaTime;
            }
        }
    }

    void OnPursuit()
    {
        if (Vector2.Distance(transform.position, player.position) < playerFoundDistance && Vector2.Distance(transform.position, player.position) > stopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, pursuitSpeed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, player.position) <= stopDistance)
        {
            transform.position = this.transform.position;
            //Instantiate(melee, transform.position, Quaternion.identity);
        }
        else if (Vector2.Distance(transform.position, player.position) > abandonDistance)
        {
            OnPatrol();
        }
    }

}
