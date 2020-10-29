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

    public Transform wallCheck;
    public LayerMask wall;
    public float wallradius = 0.2f;
    public bool gdeWall;
    bool isKeyDown;

    void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        isGrounded = CheckGround();
        gdeWall = CheckWall();
        DealDamage();
        Debug.Log(transform.rotation.y);
    }

    private void FixedUpdate()
    {
        if (isKeyDown && playerBody.velocity.y > 0f)
        {
            player.fallingGravityScale = 1f;
        }
        else if(!isGrounded)
        {
            player.fallingGravityScale = 2f;
        }
    }

    public bool CheckGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, radius, ground);
    }

    public bool CheckWall()
    {
        return Physics2D.OverlapCircle(wallCheck.position, wallradius, wall);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        isKeyDown = !context.canceled;
        if (!context.performed)
            return;
        playerBody.velocity = new Vector2(playerBody.velocity.x, 0f);

        if (isGrounded)
            playerBody.AddForce(transform.up * jumpPower, ForceMode2D.Impulse);

        if (gdeWall)
            StartCoroutine(Stun());     
    }

    void DealDamage()
    {
        if (!player.enabled)
            damagedEnemies = Physics2D.OverlapCircleAll(fallAttack.position, radius, Enemy);

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

    IEnumerator Stun()
    {
        playerBody.velocity = Vector2.zero;

        player.enabled = false;

        playerBody.AddForce((transform.up - transform.right) * jumpPower, ForceMode2D.Impulse);

        if (transform.rotation.y <= 0.1f)
            transform.rotation = new Quaternion(0, 180f, 0f, 1f);
        else if (transform.rotation.y >= 0.9f)
            transform.rotation = new Quaternion(0, 0f, 0f, 1f);

        yield return new WaitForSecondsRealtime(0.25f);
        player.enabled = true;
    }   
}
