using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{

    private Animator anim;
    private bool animIsDone = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("animIsDone", animIsDone);

        if(animIsDone)
        {
            Destroy(this.gameObject);
            animIsDone = false;
        }
    }

    public void SetAnimBool()
    {
        animIsDone = true;
    }
}
