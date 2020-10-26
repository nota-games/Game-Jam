using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public Vector2 input;
    Rigidbody2D playerbody;
    float speed = 5f;
    float airSpeed = 5.25f;
    float fallSpeed = 5.75f;

    [Tooltip("Сила прыжка")]
    public float jumpPower = 6f;
    [Tooltip("Гравитация при переходе в фазу падения")]
    public float fallingGravityScale = 0.95f;

    
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
                         + Convert.ToInt32(!isGrounded && playerbody.velocity.y >= 0f) * airSpeed
                         + Convert.ToInt32(!isGrounded && playerbody.velocity.y < 0f) * fallSpeed;
        playerbody.velocity = new Vector2(input.x * speedScale, playerbody.velocity.y);
    }

    void Falling()
    {
        playerbody.gravityScale = Convert.ToInt32(playerbody.velocity.y >= 0f)
                                + Convert.ToInt32(playerbody.velocity.y < 0f) * fallingGravityScale;
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
        playerbody.AddForce(transform.up * jumpPower, ForceMode2D.Impulse);
    }
}
