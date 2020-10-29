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
    public float isActive = 1f;
    [Tooltip("Время отката дэша")]
    float coolDown = 5f;

    public Transform attackcheck;
    public LayerMask Enemy;
    public float radius = 0.7f;
    Collider2D[] damagedEnemies;

    void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (isActive < 1)
            isActive += (1 / coolDown) * Time.deltaTime;
        isActive = Mathf.Clamp01(isActive);
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
        if (!context.performed || isActive < 1f)
            return;

        direction = player.input;
        if (direction.Equals(Vector2.zero))
            direction = transform.right;

        StartCoroutine(Stun());
    }

    IEnumerator Stun()
    {
        playerBody.velocity = Vector2.zero;

        player.enabled = false;

        playerBody.velocity = new Vector2(direction.x, direction.y / 1.5f) * dashRange * 4;
        yield return new WaitForSecondsRealtime(0.25f);
        isActive = 0;

        player.enabled = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackcheck.position, radius);
    }
}
