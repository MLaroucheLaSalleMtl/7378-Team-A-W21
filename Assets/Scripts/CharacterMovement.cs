using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D player;
    [SerializeField] private float dashAmount = 3f;

    private bool isDashing = false;

    Vector3 dir = new Vector3();
    public float speed = 275.0f;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    
    public void OnMove()
    {
        dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void Movement()
    {
        Vector3 pos = new Vector3();
        pos += (dir.normalized * speed) * Time.fixedDeltaTime;
        player.velocity = pos;
    }

    // Update is called once per frame
    void Update()
    {
        OnMove();

        if (Input.GetButtonDown("Dash"))
        {
            isDashing = true;
        }
    }

    private void FixedUpdate()
    {
        Animate();
        Movement();

        if (isDashing == true)
        {
            player.MovePosition(transform.position + dir * dashAmount);
            StaminaBar.instance.UseStamina(15);
            isDashing = false;
        }
    }

    private void Animate()
    {
        //anim.SetFloat("Magnitude", player.velocity.magnitude);
        anim.SetFloat("X", dir.x);
        anim.SetFloat("Y", dir.y);
    }


}
