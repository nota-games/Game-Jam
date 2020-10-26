using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : MonoBehaviour
{
    Rigidbody2D playerbody;
    Vector2 direction;
    bool isGrounded;
    bool isActive;

    void Awake()
    {
        playerbody = GetComponent<Rigidbody2D>();
        isActive = true;
    }

    void FixedUpdate()
    {
        isGrounded = GetComponent<PlayerMovement>().isGrounded;
    }

    public void Dash(InputAction.CallbackContext context)
    {
        //if (!context.performed)
        if (!context.performed || !isActive)
            return;
        direction = GetComponent<PlayerMovement>().input;
        if (direction == Vector2.zero)
            direction = transform.right;
        isActive = false;
        StartCoroutine(Stun());
        StartCoroutine(CoolDown());
    }

    IEnumerator Stun()
    {
        GetComponent<PlayerMovement>().enabled = false;
        playerbody.velocity = Vector2.zero;
        playerbody.velocity = direction * 15f;
        //playerbody.velocity = new Vector2(direction.x * 30f, 0f);
        yield return new WaitForSecondsRealtime(0.2f);
        GetComponent<PlayerMovement>().enabled = true;
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSecondsRealtime(2f);
        isActive = true;
    }
}
