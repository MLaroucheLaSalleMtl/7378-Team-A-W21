using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D player;
    Vector2 dir = new Vector2();
    public float speed = 275.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    public void OnMove()
    {
        dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void Movement()
    {
        Vector2 pos = new Vector2();
        pos += (dir.normalized * speed) * Time.fixedDeltaTime;
        player.velocity = pos;
    }

    // Update is called once per frame
    void Update()
    {
        OnMove();
    }

    private void FixedUpdate()
    {
        Movement();
    }
}
