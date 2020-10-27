using System;
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

    public Transform attackcheck;
    public LayerMask Enemy;
    public float radius = 0.2f;

    Collider2D[] damagedEnemies;

    [Tooltip("Дальность дэша в клетках")]
    float dashRange = 7.5f;

    void Awake()
    {
        playerbody = GetComponent<Rigidbody2D>();
        isActive = true;
    }

    void FixedUpdate()
    {
        isGrounded = GetComponent<PlayerMovement>().isGrounded;
        if (!GetComponent<PlayerMovement>().enabled)
            damagedEnemies = Physics2D.OverlapCircleAll(attackcheck.position, radius, Enemy) ;
        if (damagedEnemies != null)
        {
            foreach (Collider2D e in damagedEnemies)
            {
                Destroy(e.gameObject);
            }
        }
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
        //playerbody.velocity = direction * 15f;
        playerbody.velocity = new Vector2(direction.x * dashRange * 4, 0f);
        yield return new WaitForSecondsRealtime(0.25f);
        GetComponent<PlayerMovement>().enabled = true;
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSecondsRealtime(2f);
        isActive = true;
    }
}
