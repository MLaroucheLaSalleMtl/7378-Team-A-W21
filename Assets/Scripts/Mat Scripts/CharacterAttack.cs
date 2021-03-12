using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class CharacterAttack : MonoBehaviour
{
    [Header("Attack Area")]
    public GameObject sword;
    [SerializeField] public Transform atkArea;
    [SerializeField] private float atkSize;
    private float atkCd;
    Vector2 direction = new Vector2();
    private bool isAttacking = false;
    Animator anim;

    [Header("Ultimate Area")]
    [SerializeField] private Transform ultArea;
    [SerializeField] private float ultSize;
    [SerializeField] ParticleSystem ultPoof;
    [SerializeField] private Text ultCdTimer;
    private int staminaUlt = 20;
    private float ultCd = 0f;
    private bool canUseStamina;

    [Header("Enemy Layer")]
    [SerializeField] private LayerMask enemyLayer;
    
    [Header("Projectile")]
    [SerializeField] public GameObject projectile;
    private int staminaThrow = 20;
    private float projCd;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("IsAttack", isAttacking);
        AttackDirection();
        OnAttack();

        canUseStamina = GetComponent<CharacterMovement>().CanDash1;
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
            ultCdTimer.text = "Ultimate Cooldown: " + ultCd.ToString("0");
        }
        else
        {
            ultCdTimer.gameObject.SetActive(false);
        }
        
    }
    private void OnAttack()
    {
        int dmg = 15;
        //Basic attack
        if (!isAttacking) //atkCd == 0)
        {
            
            if (Input.GetButton("Fire1") && atkCd <= 0)
            {
                atkCd = 0.3f;
                isAttacking = true;
                anim.SetBool("IsAttack", isAttacking);
                sword.SetActive(true);
                Collider2D[] enemies = Physics2D.OverlapCircleAll(atkArea.position, atkSize, enemyLayer);
                for (int i = 0; i < enemies.Length; i++)
                {
                    Transform enemyTrans = enemies[i].GetComponent<Transform>();
                    enemies[i].GetComponent<EnemyBehavior>().TakeDamage(dmg);
                    Vector2 knock = enemyTrans.position - transform.position;
                    enemyTrans.position = new Vector2(enemyTrans.position.x + knock.x, enemyTrans.position.y + knock.y);
                }
            }
            if (Input.GetButton("Ultimate") && ultCd <= 0 && canUseStamina == true)
            {
                ultCd = 15f;
                ultPoof.Play();
                StaminaBar.instance.UseStamina(staminaUlt);
                Collider2D[] enemies = Physics2D.OverlapCircleAll(ultArea.position, ultSize, enemyLayer);
                for (int i = 0; i < enemies.Length; i++)
                {
                    //Transform enemyTrans = enemies[i].GetComponent<Transform>();
                    enemies[i].GetComponent<EnemyBehavior>().TakeDamage(dmg*3);
                    //Vector2 knock = enemyTrans.position - transform.position;
                    //enemyTrans.position = new Vector2(enemyTrans.position.x + knock.x, enemyTrans.position.y + knock.y);
                }
            }
            if (Input.GetButton("Fire2") && projCd <= 0 && canUseStamina == true)
            {
                projCd = 1.0f;
                float rotate = 0f;
                if (atkArea.localPosition.x < 0 && atkArea.localPosition.y == 0)
                {
                    //projectile.transform.rotation = new Quaternion (0, 0, 180, 0);
                    rotate = 180;
                }
                else if (atkArea.localPosition.y > 0 && atkArea.localPosition.x == 0)
                {
                    //projectile.transform.rotation = new Quaternion(0, 0, 90, 0);
                    rotate = 90;
                }
                else if (atkArea.localPosition.y < 0 && atkArea.localPosition.x == 0)
                {
                    //projectile.transform.rotation = new Quaternion(0, 0, 270, 0);
                    rotate = 270;
                }
                else
                {
                    //projectile.transform.rotation = new Quaternion(0, 0, 0, 0);
                    rotate = 0;
                }
                StaminaBar.instance.UseStamina(staminaThrow);
                GameObject proj = Instantiate(projectile, atkArea.transform.position, Quaternion.Euler (0, 0, rotate), null);
             
                //proj.transform.rotation = ne
                //new Quaternion(0, 0, rotate, 0)
                //Quaternion projRotate = proj.transform.rotation;
                //projRotate.z = rotate.z;
                Debug.Log("x " + atkArea.localPosition.x + " y " + atkArea.localPosition.y);
            }
        }
    }
    public Vector2 AttackDirection()
    {
        //Change the position of attack area in 4 directions. 
        //Do we want to attack in 8 direction? (wouldn't be too hard to change) Also is there a better way of doing this?
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //Vector2 temp = new Vector2(0, 0);
        if (direction.x > 0)
        {
            Vector2 temp = new Vector2(0.85f, 0);
            atkArea.localPosition = temp;
            //atkArea.localPosition = (0.85f, 0, 0);
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

}
