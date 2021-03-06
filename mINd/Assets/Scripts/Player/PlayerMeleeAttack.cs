﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMeleeAttack : MonoBehaviour
{
    public Transform attackCheck;
    public LayerMask Enemy;
    public float range = 0.7f;
    Collider2D[] enemies;

    public Animator animations;

    public void Attack(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        animations.SetTrigger("attack");
        enemies = Physics2D.OverlapCircleAll(attackCheck.position, range, Enemy);
        Damage();
    }

    public void Damage()
    {
        if (enemies == null)
            return;
        foreach (Collider2D e in enemies)
        {
            if (!e.CompareTag("Enemy"))
                continue;
            Health enemyHealth = e.GetComponent<EnemyHealth>().health;
            enemyHealth.Damage(1);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackCheck.position, range);
    }
}
