using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    Rigidbody2D playerBody;
    public float jumpPower = 9.5f;

    public Transform groundCheck;
    public LayerMask ground;
    public float radius = 0.2f;
    public bool isGrounded;

    void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = CheckGround();
    }

    public bool CheckGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, radius, ground);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.performed || !isGrounded)
            return;
        playerBody.velocity = new Vector2(playerBody.velocity.x, 0f);
        playerBody.AddForce(transform.up * jumpPower, ForceMode2D.Impulse);
    }
}
