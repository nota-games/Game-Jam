using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Health health;

    void Start()
    {
        health = new Health(3);
    }

    void Update()
    {
        if (health.Current == 0)
            Destroy(gameObject);
    }
}
