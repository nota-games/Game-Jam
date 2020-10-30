using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public Vector2 input;
    Rigidbody2D playerBody;
    
    [Tooltip("Скорость передвижения на земле")]
    float speed = 4f;
    [Tooltip("Скорость передвижения в воздухе")]
    float airSpeed = 4.25f;
    float currentSpeed;
    [Tooltip("Гравитация в воздухе")]
    public float fallingGravityScale = 2f;
    bool isGrounded;

    public Animator animations;

    void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        animations.SetFloat("move", Mathf.Abs(playerBody.velocity.x));
        switch (input.x)
        {
            case 1f:
                transform.rotation = new Quaternion(0f, 0f, 0f, 1f); break;
            case -1f:
                transform.rotation = new Quaternion(0, 180f, 0f, 1f); break;
        }
    }

    void FixedUpdate()
    {
        isGrounded = GetComponent<PlayerJump>().CheckGround();
        currentSpeed = Mathf.Abs(playerBody.velocity.x);
        Move();
        AirGravity();
    }

    void Move()
    {
        if (!isGrounded)
            playerBody.velocity = new Vector2(input.x * airSpeed, playerBody.velocity.y);
        else if (input.x != 0f)
        {
            playerBody.velocity += new Vector2(input.x * (speed / 8), 0f);
            playerBody.velocity = new Vector2(Mathf.Clamp(playerBody.velocity.x, -speed, speed), playerBody.velocity.y);
        }   
        else if (currentSpeed <= 3.5f)
            playerBody.velocity = new Vector2(0f, playerBody.velocity.y);
    }

    void AirGravity()
    {
        playerBody.gravityScale = Convert.ToInt32(isGrounded) 
                                + Convert.ToInt32(!isGrounded) * fallingGravityScale;
    }

    public void GetAxis(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        input = context.ReadValue<Vector2>();
    }
}
