using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealSecretRoom : MonoBehaviour
{
    [SerializeField] private GameObject secretRoomWalls;
    [SerializeField] private GameObject secretRoomFloor;
    [SerializeField] private GameObject key;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            secretRoomWalls.SetActive(true);
            secretRoomFloor.SetActive(true);
            key.SetActive(true);
        }
    }
}
