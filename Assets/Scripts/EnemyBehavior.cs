using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [Header("Enemy Options:")]
    [SerializeField] public int HP;
    [SerializeField] public int attack;
    [SerializeField] public float speed;
    [SerializeField] public bool rangedEnemy;
    [Space]
    [Header("Patrol Options:")]
    [SerializeField] public float patrolSpeed;
    [SerializeField] public Transform[] patrolPoint;
    [SerializeField] public float patrolCounter;
    private float patrolWait;
    private int points = 1;
    [Space]
    [Header("Attack Options:")]
    [SerializeField] public GameObject rangedWeapon;
    [SerializeField] public float atkCounter;
    private float atkWait;
    [Space]
    [Header("Pursuit Options:")]
    [SerializeField] public float pursuitSpeed;
    [SerializeField] public float playerFoundDistance;
    [SerializeField] public float stopDistance;
    [SerializeField] public float abandonDistance;

    private Transform player;
    Rigidbody2D rigid;
    Animator anim;
    Vector2 direction = new Vector2();
    private bool isStopped = false;
    private bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        patrolWait = patrolCounter;
        atkWait = atkCounter;
    }

    // Update is called once per frame
    void Update()
    {
        Animate();
        OnPursuit();
    }

    void OnPatrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, patrolPoint[points].position, patrolSpeed * Time.deltaTime);
        direction = (patrolPoint[points].transform.position - transform.position).normalized;

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
                isStopped = false;
            }
            else
            {
                patrolWait -= Time.deltaTime;
                isStopped = true;
            }
        }
    }

    void OnAttack()
    {
        if (atkWait <= 0)
        {
            atkWait = atkCounter;
            isAttacking = true;

            if(rangedEnemy == true)
            {
                Instantiate(rangedWeapon, transform.position, Quaternion.identity);
            }
           
        }
        else
        {
            atkWait -= Time.deltaTime;
            isAttacking = false;
        }
    }

    void OnPursuit()
    {
        direction = (player.transform.position - transform.position).normalized;

        if (Vector2.Distance(transform.position, player.position) < playerFoundDistance && Vector2.Distance(transform.position, player.position) > stopDistance)
        {
            isStopped = false;
            isAttacking = false;
            transform.position = Vector2.MoveTowards(transform.position, player.position, pursuitSpeed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, player.position) <= stopDistance)
        {
            transform.position = this.transform.position;
            isStopped = true;
            //isAttacking = true;
            OnAttack();
        }
        else if (Vector2.Distance(transform.position, player.position) > abandonDistance)
        {
            OnPatrol();
        }
    }

    void Animate()
    {
        anim.SetFloat("X", direction.x);
        anim.SetFloat("Y", direction.y);
        anim.SetBool("IsStopped", isStopped);
        anim.SetBool("IsAttacking", isAttacking);
    }
}
