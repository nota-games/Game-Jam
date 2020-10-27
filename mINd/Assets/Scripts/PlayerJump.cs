using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    Rigidbody2D playerbody;
    public float jumpPower = 9.5f;

    public Transform groundCheck;
    public LayerMask ground;
    public float radius = 0.2f;
    public bool isGrounded;

    void Awake()
    {
        playerbody = GetComponent<Rigidbody2D>();
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
        playerbody.velocity = new Vector2(playerbody.velocity.x, 0f);
        playerbody.AddForce(transform.up * jumpPower, ForceMode2D.Impulse);
    }
}
