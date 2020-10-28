using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    Health health;

    void Start()
    {
        health = new Health(3);
    }
}
