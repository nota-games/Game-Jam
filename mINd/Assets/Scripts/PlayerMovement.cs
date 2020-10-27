using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public Vector2 input;
    Rigidbody2D playerbody;
    
    [Tooltip("Скорость передвижения на земле")]
    float speed = 4f;
    [Tooltip("Скорость передвижения в воздухе")]
    float airSpeed = 4.25f;
    [Tooltip("Гравитация в воздухе")]
    public float fallingGravityScale = 2f;
    bool isGrounded;

    void Awake()
    {
        playerbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        switch (playerbody.velocity.x)
        {
            case 1f:
                transform.rotation = new Quaternion(0f, 0f, 0f, 1f); break;
            case -1f:
                transform.rotation = new Quaternion(0, 180f, 0f, 1f); break;
        }
    }

    void FixedUpdate()
    {
        isGrounded = GetComponent<PlayerJump>().CheckGround(); ;
        Move();
        AirGravity();
    }

    void Move()
    {
        float speedScale = Convert.ToInt32(isGrounded) * speed
                         + Convert.ToInt32(!isGrounded) * airSpeed;
        playerbody.velocity = new Vector2(input.x * speedScale, playerbody.velocity.y);
    }

    void AirGravity()
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
}
