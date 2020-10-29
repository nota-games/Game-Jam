using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D enemyBody;

    public float enemySpeed = 3f;
    public float enemyJumpPower = 1f;

    public Transform topCheck;
    public Transform midCheck;
    public Transform bottomCheck;
    public LayerMask ground;
    float radius = 0.4f;

    public Transform groundCheck;
    public bool isGrounded;

    Transform playerPosition;
    bool isPlayerNear;

    void Awake()
    {
        enemyBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Physics2D.OverlapCircle(bottomCheck.position, radius, ground))
            Move();
        if (!Physics2D.OverlapCircle(bottomCheck.position, radius, ground)
         || Physics2D.OverlapCircle(midCheck.position, radius, ground))
        {
            if (!Physics2D.OverlapCircle(topCheck.position, radius, ground) && Physics2D.OverlapCircle(groundCheck.position, radius, ground))
                Jump();
            else if (Physics2D.OverlapCircle(topCheck.position, radius, ground))
                TurnAround();
        }
            
    }

    void Move()
    {
        if (!isPlayerNear)
            enemyBody.velocity = new Vector2(transform.right.x * enemySpeed, enemyBody.velocity.y);
    }

    void TurnAround()
    {
        switch(transform.rotation.y)
        {
            case 0:
                transform.rotation = new Quaternion(0f, 180f, 0f, 1f); break;
            case 180:
                transform.rotation = new Quaternion(0f, 0f, 0f, 1f); break;
            default:
                transform.rotation = new Quaternion(0f, 0f, 0f, 1f); break;
        }
    }

    void Jump()
    {
        enemyBody.AddForce(transform.up * enemyJumpPower, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isPlayerNear = collision.CompareTag("Player");
        playerPosition = collision.transform;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isPlayerNear = !collision.CompareTag("Player");
        playerPosition = null;
    }
}
