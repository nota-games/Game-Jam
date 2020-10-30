using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    Health health;

    public Animator animation;

    void Start()
    {
        health = new Health(3);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            health.Damage(1);
            animation.SetTrigger("hurt");
        }
    }
}
