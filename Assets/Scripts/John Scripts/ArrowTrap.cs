using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    [SerializeField] private float rateOfTime = 5;
    [SerializeField] private float rotate = 0;
    [SerializeField] private Vector3 spawnLocation = new Vector3(0, 0, 0);
    [SerializeField] private Vector2 arrowDir = new Vector2(0, 15);

    // Start is called before the first frame update
    void Start()
    {
        ShootArrows();
    }

    private void ShootArrows()
    {
        InvokeRepeating("ShootArrows", rateOfTime, 0);
        GameObject arrowGameObject = Instantiate(arrow, transform.position + spawnLocation, Quaternion.Euler(0, 0, rotate), null);
        Rigidbody2D arrowRB = arrowGameObject.GetComponent<Rigidbody2D>();
        arrowRB.velocity = arrowDir;
        Destroy(arrowGameObject, 7f);
    }


}
