using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BerzerkerBehaviour : MonoBehaviour
{
    [Header("Enemy Options:")]
    private int HP;
    [SerializeField] public int maxHp;
    [SerializeField] public GameObject hpBar;
    [SerializeField] public Slider hpSlider;
    [SerializeField] public bool isBoss;
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
    [SerializeField] public bool lockedIn = false;
    [Header("Ultimate Attack:")]
    [SerializeField] public float ultiCounter;
    private float ultiWait;
    private bool isUlt = false;

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
        atkWait = atkCounter;
        ultiWait = ultiCounter;
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
    }

    void OnPursuit()
    {
        direction = (player.transform.position - transform.position).normalized;

        if(lockedIn)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, pursuitSpeed * Time.deltaTime);
        }

        if (Vector2.Distance(transform.position, player.position) < playerFoundDistance && Vector2.Distance(transform.position, player.position) > stopDistance)
        {
            isStopped = false;
            isAttacking = false;
            lockedIn = true;
            isUlt = false;

            hpBar.SetActive(true);
        }
        else if (Vector2.Distance(transform.position, player.position) <= stopDistance)
        {
            transform.position = this.transform.position;
            isStopped = true;
            lockedIn = false;
            //OnAttack();

            if (isBoss == true)
            {
                OnUltimate();
            }
        }
    }

    void OnAttack()
    {
        if (atkWait <= 0)
        {
            atkWait = atkCounter;
            isAttacking = true;
            Instantiate(rangedWeapon, transform.position, Quaternion.identity);
        }
        else
        {
            atkWait -= Time.deltaTime;
            isAttacking = false;
            isUlt = false;
        }
    }

    void OnUltimate()
    {
        if (ultiWait <= 0)
        {
            ultiWait = ultiCounter;
            isAttacking = true;
            isUlt = true;
            pursuitSpeed = pursuitSpeed * 10;
            transform.position = Vector2.MoveTowards(transform.position, player.position, pursuitSpeed * Time.deltaTime);
        }
        else
        {
            ultiWait -= Time.deltaTime;
            isAttacking = false;
            isUlt = false;
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

        if (HP <= 0)
        {
            Destroy(gameObject);
            hpBar.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player") && isUlt)
        {
            collision.collider.GetComponent<HealthBar>().TakeDamage(40);
            ultiWait = ultiCounter;
        }
    }
}
