using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class CharacterProjectile : MonoBehaviour
{
    public GameObject projectile;
    private float atkCd;
    private Transform projDirection;
    private Vector3 projDir;
    private Vector3 lastDir;// = new Vector3(0,1,0);
    private Vector3 ifnotmove;
    private float projSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        //projDir = GetComponent<CharacterAttack>().AttackDirection();
        projDir = projectile.GetComponent<CharacterAttack>().AttackDirection();
        //lastDir = projDir;
        //lastDir = projectile.GetComponent<CharacterMovement>().dir;
    }
    // Update is called once per frame
    void Update()
    {
        if (projDir.x > 0)
        {
            lastDir = new Vector3(1, 0, 0);
            transform.position += lastDir.normalized * (Time.deltaTime * projSpeed);
        }
        else if (projDir.x < 0)
        {
            lastDir = new Vector3(-1, 0, 0);
            transform.position += lastDir.normalized * (Time.deltaTime * projSpeed);
        }
        else if (projDir.y > 0)
        {
            lastDir = new Vector3(0, 1, 0);
            transform.position += lastDir.normalized * (Time.deltaTime * projSpeed);
        }
        else if (projDir.y < 0)
        {
            lastDir = new Vector3(0, -1, 0);
            transform.position += lastDir.normalized * (Time.deltaTime * projSpeed);
        }
        else
        {
            transform.position += lastDir.normalized * (Time.deltaTime * projSpeed);
        }
        
        //if (projDir != new Vector3(0,0,0))
        //{
        //    lastDir = projDir;
        //}
        //transform.position += lastDir.normalized * (Time.deltaTime * projSpeed);

    }



}
