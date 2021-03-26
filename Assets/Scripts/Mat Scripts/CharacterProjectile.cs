using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class CharacterProjectile : MonoBehaviour
{
    enum ProjectileType
    {
        Throwable,
        Pickup
    }
    [SerializeField] private ProjectileType typeOfProjectile;
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
    }
    // Update is called once per frame
    void Update()
    {
        if(typeOfProjectile == ProjectileType.Throwable)
        {
            transform.position += throwArea * (Time.deltaTime * projSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && typeOfProjectile == ProjectileType.Throwable)
        {
            collision.GetComponent<EnemyBehavior>().TakeDamage(projDmg);
            Destroy(this.gameObject);
        }
        else if (collision.tag == "Player" && typeOfProjectile == ProjectileType.Pickup)
        {
            myCharacter.GetComponent<CharacterAttack>().projCount++;
            Destroy(this.gameObject);
        }
    }



}
