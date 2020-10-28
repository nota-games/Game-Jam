using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Health health;
    public int healthPoints = 3;

    void Start()
    {
        health = new Health(healthPoints);
    }

    void Update()
    {
        if (health.Current == 0)
            Destroy(gameObject);
    }
}
