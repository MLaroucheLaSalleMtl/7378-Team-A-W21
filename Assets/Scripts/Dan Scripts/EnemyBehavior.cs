using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        HP = maxHp;
        hpBar.GetComponent<Slider>().maxValue = maxHp;
        hpBar.GetComponent<Slider>().value = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        Animate();
        OnPursuit();

        hpSlider.value = HP;

        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(20);
        }
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
            transform.position = Vector2.MoveTowards(transform.position, player.position, pursuitSpeed * Time.deltaTime);

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
            Destroy(gameObject);
            hpBar.SetActive(false);

            if(GetComponent<KeyGiver>())
            {
                GetComponent<KeyGiver>().GiveKey();
            }
        }
    }

}
