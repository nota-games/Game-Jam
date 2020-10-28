using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    Rigidbody2D playerBody;
    PlayerMovement player;
    public float jumpPower = 9.5f;

    public Transform groundCheck;
    public LayerMask ground;
    public float radius = 0.2f;
    public bool isGrounded;

    public Transform fallAttack;
    public LayerMask Enemy;
    Collider2D[] damagedEnemies;

    void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        isGrounded = CheckGround();
        DealDamage();
    }

    public bool CheckGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, radius, ground);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        playerBody.velocity = new Vector2(playerBody.velocity.x, 0f);

        if (isGrounded)
            playerBody.AddForce(transform.up * jumpPower, ForceMode2D.Impulse);
        else
        {
            StartCoroutine(Stun());
        }      
    }

    void DealDamage()
    {
        if (!player.enabled)
            damagedEnemies = Physics2D.OverlapCircleAll(fallAttack.position, radius, Enemy);

        if (damagedEnemies == null)
            return;

        foreach (Collider2D e in damagedEnemies)
        {
            Health enemyHealth = e.GetComponent<EnemyHealth>().health;
            enemyHealth.Damage(1);
        }
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
