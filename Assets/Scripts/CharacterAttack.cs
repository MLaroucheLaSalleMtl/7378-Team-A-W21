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
    [SerializeField] private LayerMask enemyLayer;
    Vector2 direction = new Vector2();
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        AttackDirection();
        OnAttack();
        if(atkCd >= 0)
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
                //sword.SetActive(true);
                Collider2D[] enemies = Physics2D.OverlapCircleAll(atkArea.position, atkSize, enemyLayer);
                for (int i = 0; i < enemies.Length; i++)
                {
                    Debug.Log("Hit");
                }
            }
        }

        //sword.SetActive(false);
    }
    private void AttackDirection()
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
}
