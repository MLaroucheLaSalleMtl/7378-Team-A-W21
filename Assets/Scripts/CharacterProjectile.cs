using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterProjectile : MonoBehaviour
{
    public GameObject projectile;
    private float atkCd;
    private Transform projDirection;
    private Vector3 projDir;
    [SerializeField]private float projSpeed = 6f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        OnThrow();
        transform.position += projDir * Time.deltaTime * projSpeed;
        if (atkCd >= 0)
        {
            atkCd -= 1.0f;
        }
    }
    private void OnThrow()
    {
        //projectile.transform.position += -projArea.forward * Time.deltaTime * 6f;
        if (atkCd == 0)
        {
            if (Input.GetButton("Fire2"))
            {
                //projDirection = atkArea;
                //Instantiate(projectile, projDir, )
            }
        }
    }




}
