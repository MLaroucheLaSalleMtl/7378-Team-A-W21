using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class CharacterProjectile : MonoBehaviour
{
    public GameObject myCharacter;
    private Vector3 throwArea;
    [SerializeField] private float projSpeed = 3f;
    private GameObject enemy;
    [SerializeField] private int projDmg = 30;
    [SerializeField] private int projStaminaCost = 20;


    // Start is called before the first frame update
    void Start()
    {
        myCharacter = GameObject.FindGameObjectWithTag("Player");
        throwArea = myCharacter.GetComponent<CharacterAttack>().atkArea.localPosition;
        //enemy = GameObject.FindGameObjectWithTag("Enemy");
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += throwArea * (Time.deltaTime * projSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyBehavior>().TakeDamage(projDmg);
            //enemy.GetComponent<EnemyBehavior>().TakeDamage(projDmg);
            Destroy(this.gameObject);
        }
    }



}
