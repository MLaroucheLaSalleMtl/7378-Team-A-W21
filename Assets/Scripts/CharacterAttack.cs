using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEditor;

public class CharacterAttack : MonoBehaviour
{
    private float atkCd;
    public GameObject sword;
    [SerializeField] private Transform atkArea;
    [SerializeField] private float atkSize;
    [Space]
    [SerializeField] private Transform ultArea;
    [SerializeField] private float ultSize;
    [Space]
    [SerializeField] private LayerMask enemyLayer;
    Vector2 direction = new Vector2();
    Animator anim;
    public bool isAttacking = false;
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
        
        if (atkCd >= 0)
        {
            atkCd -= 1.0f;
        }
        //Debug.Log(direction);
        
    }
    
    private void OnAttack()
    {
        //Basic attack
        if (atkCd == 0)
        {
            atkCd = 30.0f;
            if (Input.GetButton("Fire1"))
            {
                isAttacking = true;
                
                sword.SetActive(true);
                Collider2D[] enemies = Physics2D.OverlapCircleAll(atkArea.position, atkSize, enemyLayer);
                for (int i = 0; i < enemies.Length; i++)
                {
                    Debug.Log("Hit");
                }
            }
            if (Input.GetButton("Ultimate"))
            {
                Collider2D[] enemies = Physics2D.OverlapCircleAll(ultArea.position, ultSize, enemyLayer);
                for (int i = 0; i < enemies.Length; i++)
                {
                    Debug.Log("UltHit");
                }
            }
        }

        //sword.SetActive(false);
    }
    public void AttackDirection()
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
        }
        else if (direction.x < 0)
        {
            Vector2 temp = new Vector2(-0.85f, 0);
            atkArea.localPosition = temp;
        }
        else if (direction.y > 0)
        {
            Vector2 temp = new Vector2(0, 0.85f);
            atkArea.localPosition = temp;
        }
        else if (direction.y < 0)
        {
            Vector2 temp = new Vector2(0, -0.85f);
            atkArea.localPosition = temp;
        }
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
