using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D player;
    [SerializeField] private LayerMask dashLayerMask;
    Animator anim;

    [Header("Variables")]
    [SerializeField] private float dashAmount = 3f;
    [SerializeField] private int dashCost = 20;
    private float speed = 300.0f;


    [Header("Boolean")]
    private bool isDashing = false;
    private bool CanDash;

    [Header("Audio")]
    [SerializeField] private AudioClip dashSound;

    Vector3 dir = new Vector3();



    public bool CanDash1 { get => CanDash; set => CanDash = value; }
    public float Speed { get => speed; set => speed = value; }


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
            pos += (dir.normalized * Speed) * Time.fixedDeltaTime;
            player.velocity = pos; 
    }

    // Update is called once per frame
    void Update()
    {
        OnMove();

        if (Input.GetButtonDown("Dash") && CanDash1 == true && player.velocity.magnitude > 0)
        {
            isDashing = true;
            AudioClipManager.instance.PlayHitSound(dashSound);
        }
    }

    private void FixedUpdate()
    {
        Animate();
        Movement();

        
        if (isDashing == true)
        {
            Vector3 dashPosition = transform.position + dir * dashAmount;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, dir, dashAmount, dashLayerMask);
            if(raycastHit2D.collider != null)
            {
                dashPosition = raycastHit2D.point;
            }

            StaminaBar.instance.UseStamina(dashCost);
            player.MovePosition(dashPosition);
            isDashing = false;
        }
        
    }

    private void Animate()
    {
        anim.SetFloat("Magnitude", player.velocity.magnitude);
        anim.SetFloat("X", dir.x);
        anim.SetFloat("Y", dir.y);
    }


}
