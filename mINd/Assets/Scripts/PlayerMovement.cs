using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public Vector2 input;
    Rigidbody2D playerbody;
    float speed = 4f;
    float airSpeed = 4.25f;

    [Tooltip("Сила прыжка")]
    public float jumpPower = 6f;
    [Tooltip("Гравитация в воздухе")]
    public float fallingGravityScale = 2f;


    public Transform groundCheck;
    public LayerMask ground;
    public float radius = 0.2f;
    public bool isGrounded;

    void Awake()
    {
        playerbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (playerbody.velocity.x > 0f)
            transform.rotation = new Quaternion(0f, 0f, 0f, 1f);
        else if (playerbody.velocity.x < 0f)
            transform.rotation = new Quaternion(0f, 180f, 0f, 1f);
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, radius, ground);
        Move();
        Falling();
    }

    void Move()
    {
        float speedScale = Convert.ToInt32(isGrounded) * speed
                         + Convert.ToInt32(!isGrounded) * airSpeed;
        playerbody.velocity = new Vector2(input.x * speedScale, playerbody.velocity.y);
    }

    void Falling()
    {
        playerbody.gravityScale = Convert.ToInt32(isGrounded) 
                                + Convert.ToInt32(!isGrounded) * fallingGravityScale;
    }

    public void GetAxis(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        input = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.performed || !isGrounded)
            return;
        playerbody.velocity = new Vector2(playerbody.velocity.x, 0f);
        playerbody.AddForce(transform.up * jumpPower, ForceMode2D.Impulse);
    }
}
