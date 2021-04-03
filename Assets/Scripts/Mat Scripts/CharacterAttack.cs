using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class CharacterAttack : MonoBehaviour
{
    [Header("Attack Area")]
    [SerializeField] public Transform atkArea;
    [SerializeField] private float atkSize;
    private float atkCd;
    Vector2 direction = new Vector2();
    private bool isAttacking = false;
    Animator anim;
    public int dmg = 15;

    [Header("Ultimate Area")]
    [SerializeField] private Transform ultArea;
    [SerializeField] private float ultSize;
    [SerializeField] ParticleSystem ultPoof;
    [SerializeField] private Text ultCdTimer;
    private int staminaUlt = 20;
    private float ultCd = 0f;
    public float upgradeUltCd = 15f;
    private bool canUseStamina;

    [Header("Enemy Layer")]
    [SerializeField] private LayerMask enemyLayer;
    
    [Header("Projectile")]
    [SerializeField] public GameObject projectile;
    private int staminaThrow = 20;
    private float projCd;
    public int maxProjCount = 3;
    public int projCount;
    [SerializeField] private Text projCounter;

    [Header("Audio")]
    [SerializeField] private AudioClip stabSound;
    [SerializeField] private AudioClip missSound;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        projCount = maxProjCount;
        projCounter.text = projCount.ToString("0");
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("IsAttack", isAttacking);
        AttackDirection();
        OnAttack();
        canUseStamina = GetComponent<CharacterMovement>().CanDash1;
        CooldownTimer();
    }
    private void OnAttack()
    {
        if (!isAttacking)
        {
            
            if (Input.GetButton("Fire1") && atkCd <= 0)
            {
                AudioClipManager.instance.PlayHitSound(missSound);
                atkCd = 0.3f;
                isAttacking = true;
                anim.SetBool("IsAttack", isAttacking);
                Collider2D[] enemies = Physics2D.OverlapCircleAll(atkArea.position, atkSize, enemyLayer);
                for (int i = 0; i < enemies.Length; i++)
                {
                    Transform enemyTrans = enemies[i].GetComponent<Transform>();
                    if(enemies[i].CompareTag("Enemy"))
                    {
                        AudioClipManager.instance.PlayHitSound(stabSound);
                        enemies[i].GetComponent<EnemyBehavior>().TakeDamage(dmg);
                    }
                    else if(enemies[i].CompareTag("Berzerk"))
                    {
                        AudioClipManager.instance.PlayHitSound(stabSound);
                        enemies[i].GetComponent<BerzerkerBehaviour>().TakeDamage(dmg);
                    }
                    Vector2 knock = enemyTrans.position - transform.position;
                    enemyTrans.position = new Vector2(enemyTrans.position.x + knock.x, enemyTrans.position.y + knock.y);
                }
                
            }
            if (Input.GetButton("Ultimate") && ultCd <= 0 && canUseStamina == true)
            {
                ultCd = upgradeUltCd;
                ultPoof.Play();
                StaminaBar.instance.UseStamina(staminaUlt);
                Collider2D[] enemies = Physics2D.OverlapCircleAll(ultArea.position, ultSize, enemyLayer);
                for (int i = 0; i < enemies.Length; i++)
                {
                    if (enemies[i].CompareTag("Enemy"))
                    {
                        enemies[i].GetComponent<EnemyBehavior>().TakeDamage(dmg*3);
                    }
                    else if (enemies[i].CompareTag("Berzerk"))
                    {
                        enemies[i].GetComponent<BerzerkerBehaviour>().TakeDamage(dmg*3);
                    }
                }
            }
            if (Input.GetButton("Fire2") && projCd <= 0 && canUseStamina == true && projCount > 0)
            {
                projCd = 1.0f;
                float rotate = 0f;
                if (atkArea.localPosition.x < 0 && atkArea.localPosition.y == 0)
                {
                    rotate = 180;
                }
                else if (atkArea.localPosition.y > 0 && atkArea.localPosition.x == 0)
                {
                    rotate = 90;
                }
                else if (atkArea.localPosition.y < 0 && atkArea.localPosition.x == 0)
                {
                    rotate = 270;
                }
                else
                {

                    rotate = 0;
                }
                StaminaBar.instance.UseStamina(staminaThrow);
                GameObject proj = Instantiate(projectile, atkArea.transform.position, Quaternion.Euler (0, 0, rotate), null);
                projCount--;
                projCounter.text = projCount.ToString("0");


                Debug.Log("x " + atkArea.localPosition.x + " y " + atkArea.localPosition.y);
            }
        }
    }
    public Vector2 AttackDirection()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (direction.x > 0)
        {
            Vector2 temp = new Vector2(0.85f, 0);
            atkArea.localPosition = temp;
            return temp;
        }
        else if (direction.x < 0)
        {
            Vector2 temp = new Vector2(-0.85f, 0);
            atkArea.localPosition = temp;
            return temp;
        }
        else if (direction.y > 0)
        {
            Vector2 temp = new Vector2(0, 0.85f);
            atkArea.localPosition = temp;
            return temp;
        }
        else if (direction.y < 0)
        {
            Vector2 temp = new Vector2(0, -0.85f);
            atkArea.localPosition = temp;
            return temp;
        }
        return direction;
    } 

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(ultArea.position, ultSize);
        Gizmos.DrawWireSphere(atkArea.position, atkSize);
    }
    public void SetAttackBool()
    {
        isAttacking = false;
    }

    private void CooldownTimer()
    {
        if (atkCd >= 0)
        {
            atkCd -= Time.deltaTime;

        }
        if (projCd >= 0)
        {
            projCd -= Time.deltaTime;
        }
        if (ultCd >= 0)
        {
            ultCd -= Time.deltaTime;
            ultCdTimer.gameObject.SetActive(true);
            ultCdTimer.text = ultCd.ToString("0");
        }
        else
        {
            ultCdTimer.gameObject.SetActive(false);
        }
    }

}
