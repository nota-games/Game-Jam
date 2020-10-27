using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : MonoBehaviour
{
    Rigidbody2D playerbody;
    PlayerMovement player;

    [Tooltip("Дальность дэша в клетках")]
    float dashRange = 7.5f;
    Vector2 direction;
    bool isActive = true;

    public Transform attackcheck;
    public LayerMask Enemy;
    public float radius = 1f;
    Collider2D[] damagedEnemies;

    void Awake()
    {
        playerbody = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerMovement>();
    }

    void FixedUpdate()
    {
        Damage();
    }

    void Damage()
    {
        if (!player.enabled)
            damagedEnemies = Physics2D.OverlapCircleAll(attackcheck.position, radius, Enemy);

        if (damagedEnemies == null)
            return;

        foreach (Collider2D e in damagedEnemies)
        {
            Destroy(e.gameObject);
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (!context.performed || !isActive)
            return;

        direction = player.input;
        if (direction.Equals(Vector2.zero))
            direction = transform.right;

        StartCoroutine(Stun());
        StartCoroutine(CoolDown());
    }

    IEnumerator Stun()
    {
        playerbody.velocity = Vector2.zero;

        player.enabled = false;

        playerbody.velocity = new Vector2(direction.x * dashRange * 4, 0f);
        yield return new WaitForSecondsRealtime(0.25f);
        isActive = false;

        player.enabled = true;
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSecondsRealtime(2f);
        isActive = true;
    }
}
