using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NecromancerBehaviour : MonoBehaviour
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

    private Transform player;
    Rigidbody2D rigid;
    Animator anim;
    Vector2 direction = new Vector2();
    private bool isStopped = false;
    private bool isAttacking = false;
    private bool isShielded = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
    }

    void OnPursuit()
    {
        direction = (player.transform.position - transform.position).normalized;

        if (lockedIn)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, pursuitSpeed * Time.deltaTime);
        }

        if (Vector2.Distance(transform.position, player.position) < playerFoundDistance && Vector2.Distance(transform.position, player.position) > stopDistance)
        {
            isStopped = false;
            isAttacking = false;
            lockedIn = true;

            hpBar.SetActive(true);
        }
        else if (Vector2.Distance(transform.position, player.position) <= stopDistance)
        {
            transform.position = this.transform.position;
            isStopped = true;
            lockedIn = false;
            OnAttack();
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
       if(isShielded == false)
        {
            HP -= damage;

            if (HP <= 0)
            {
                Destroy(gameObject);
                hpBar.SetActive(false);
            }
        }
    }
}
