using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDownAttack : MonoBehaviour
{
    Rigidbody2D playerBody;
    PlayerMovement player;

    bool isGrounded;
    bool isWalled;

    void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        isGrounded = GetComponent<PlayerJump>().CheckGround();
        isWalled = GetComponent<PlayerJump>().CheckWall();
    }

    public void DownAttack(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        if (!isGrounded && !isWalled)
            StartCoroutine(Stun());
    }

    IEnumerator Stun()
    {
        playerBody.velocity = Vector2.zero;

        player.enabled = false;

        playerBody.velocity = Vector2.down * 22f;
        yield return new WaitForSecondsRealtime(0.1f);

        player.enabled = true;
    }
}
