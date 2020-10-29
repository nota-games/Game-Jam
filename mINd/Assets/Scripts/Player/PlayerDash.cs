using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : MonoBehaviour
{
    Rigidbody2D playerBody;
    PlayerMovement player;

    [Tooltip("Дальность дэша в клетках")]
    float dashRange = 7.5f;
    Vector2 direction;
    public bool isActive = true;
    [Tooltip("Время отката дэша")]
    float coolDown = 3f;

    public Transform attackcheck;
    public LayerMask Enemy;
    public float radius = 0.7f;
    Collider2D[] damagedEnemies;

    void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerMovement>();
    }

    void FixedUpdate()
    {
        DealDamage();
    }

    void DealDamage()
    {
        if (!player.enabled)
            damagedEnemies = Physics2D.OverlapCircleAll(attackcheck.position, radius, Enemy);

        if (damagedEnemies == null)
            return;

        foreach (Collider2D e in damagedEnemies)
        {
            if (!e.CompareTag("Enemy"))
                continue;
            Health enemyHealth = e.GetComponent<EnemyHealth>().health;
            enemyHealth.Damage(1);
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
        playerBody.velocity = Vector2.zero;

        player.enabled = false;

        playerBody.velocity = new Vector2(direction.x, direction.y) * dashRange * 4;
        yield return new WaitForSecondsRealtime(0.25f);
        
        player.enabled = true;

        playerBody.velocity = Vector2.zero;
    }

    IEnumerator CoolDown()
    {
        isActive = false;
        yield return new WaitForSecondsRealtime(coolDown);
        isActive = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackcheck.position, radius);
    }
}
