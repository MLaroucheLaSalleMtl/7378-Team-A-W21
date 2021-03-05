using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileEast : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int damage;
    
    private Transform player;
    private Vector2 target;

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        player = GameObject.Find("East").transform;
        target = new Vector2(player.position.x, player.position.y);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        transform.Rotate(0.0f, 0.0f, Mathf.Atan2(gameObject.transform.position.x, gameObject.transform.position.y) * Mathf.Rad2Deg);
        
        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyProjectile();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<HealthBar>().TakeDamage(damage);
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
