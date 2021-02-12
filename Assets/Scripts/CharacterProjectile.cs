using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterProjectile : MonoBehaviour
{
    public GameObject projectile;
    private float atkCd;
    [SerializeField] private Transform projArea;
    private Transform projDirection;

    // Start is called before the first frame update
    void Start()
    {
        atkCd = 30.0f;
    }

    // Update is called once per frame
    void Update()
    {
        OnThrow();
        projectile.transform.position += projDirection.transform.position * Time.deltaTime * 6f;
        if (atkCd >= 0)
        {
            atkCd -= 1.0f;
        }
    }
    private void OnThrow()
    {
        projDirection = projArea.transform;
        //projectile.transform.position += -projArea.forward * Time.deltaTime * 6f;
        if (atkCd == 0)
        {
            if (Input.GetButton("Fire2"))
            {
                //projDirection = atkArea;
                Instantiate(projectile, projArea.position, transform.rotation);
            }
        }
    }
}
