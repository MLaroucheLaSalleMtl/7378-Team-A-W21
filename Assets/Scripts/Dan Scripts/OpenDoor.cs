using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] public int keysRequired;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && GameObject.FindGameObjectWithTag("Player").GetComponent<Keys>().numberOfKeys >= keysRequired)
        {
            gameObject.SetActive(false);
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && GameObject.FindGameObjectWithTag("Player").GetComponent<Keys>().numberOfKeys == keysRequired)
        {
            gameObject.SetActive(false);
        }
    }*/
}
