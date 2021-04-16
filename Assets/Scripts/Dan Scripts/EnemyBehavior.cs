using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    [Header("Enemy Options:")]
    private int HP;
    [SerializeField] public int maxHp;
    [SerializeField] public GameObject hpBar;
    [SerializeField] public Slider hpSlider;
    [SerializeField] public int attack;
    [SerializeField] public float speed;
    [SerializeField] public bool rangedEnemy;
    [SerializeField] public bool isBoss;
    [SerializeField] public bool isFinal;
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
    [Space]
    [Header("Ultimate Attack:")]
    [SerializeField] public GameObject[] ultimate;
    [SerializeField] public float ultiCounter;
    private float ultiWait;
    [Space]
    [Header("Keys")]
    [SerializeField] public GameObject key;
    [SerializeField] public bool keyHolder;

    private NavMeshAgent agent;
    private Transform player;
    Rigidbody2D rigid;
    Animator anim;
    Vector2 direction = new Vector2();
    private bool isStopped = false;
    private bool isAttacking = false;

    [Header("Score Purposes:")]
    private ScoreAdded score;
    private int bossScore = 200;
    private int addingScore = 20;

    // Start is called before the first frame update
    void Start()
    {
        //Code to make navmesh2D to work (doesnt let the sprite rotate like paper mario)
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        patrolWait = patrolCounter;
        atkWait = atkCounter;
        HP = maxHp;
        hpBar.GetComponent<Slider>().maxValue = maxHp;
        hpBar.GetComponent<Slider>().value = maxHp;

        GameObject scoreObject = GameObject.FindGameObjectWithTag("Score");
        score = scoreObject.GetComponent<ScoreAdded>();
    }

    // Update is called once per frame
    void Update()
    {
        Animate();
        OnPursuit();

        hpSlider.value = HP;
    }

    void OnPatrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, patrolPoint[points].position, patrolSpeed * Time.deltaTime);
        direction = (patrolPoint[points].transform.position - transform.position).normalized;
        //agent.SetDestination(patrolPoint[points].position); ;

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

    void OnUltimate()
    {
        if (ultiWait <= 0)
        {
            ultiWait = ultiCounter;
            isAttacking = true;

            Instantiate(ultimate[0], transform.position, Quaternion.identity);
            Instantiate(ultimate[1], transform.position, Quaternion.identity);
            Instantiate(ultimate[2], transform.position, Quaternion.identity);
            Instantiate(ultimate[3], transform.position, Quaternion.identity);

        }
        else
        {
            ultiWait -= Time.deltaTime;
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
            //transform.position = Vector2.MoveTowards(transform.position, player.position, pursuitSpeed * Time.deltaTime);
            agent.SetDestination(player.position);

            hpBar.SetActive(true);
        }
        else if (Vector2.Distance(transform.position, player.position) <= stopDistance)
        {
            transform.position = this.transform.position;
            isStopped = true;
            //isAttacking = true;
            OnAttack();
            
            if(isBoss == true)
            {
                OnUltimate();
            }

        }
        else if (Vector2.Distance(transform.position, player.position) > abandonDistance)
        {
            OnPatrol();

            hpBar.SetActive(false);
        }
    }

    void Animate()
    {
        anim.SetFloat("X", agent.velocity.x);
        anim.SetFloat("Y", agent.velocity.y);
        anim.SetFloat("X", direction.x);
        anim.SetFloat("Y", direction.y);
        anim.SetBool("IsStopped", isStopped);
        anim.SetBool("IsAttacking", isAttacking);
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;

        if(HP <= 0)
        {
            if(isFinal == true)
            {
                gameObject.GetComponent<SummonedDeath>().OnDeath();
            }
            Destroy(gameObject);
            if (isBoss == true)
            {
                score.GainScore(bossScore);
            }
            else
            {
                score.GainScore(addingScore);
            }
            hpBar.SetActive(false);
            if(keyHolder)
            {
                Instantiate(key, transform.position, transform.rotation);
            }
        }
    }

}
