using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D enemyBody;

    public float enemySpeed = 3f;

    public Transform edgeCheck;
    public LayerMask ground;

    void Awake()
    {
        enemyBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!Physics2D.OverlapBox(edgeCheck.position, Vector2.one, 90f, ground))
            TurnAround();
        enemyBody.velocity = transform.right * enemySpeed;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            enemySpeed = 0f;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            enemySpeed = 3f;
    }
}
