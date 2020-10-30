using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    Animator animator;
    PlayerCoins player;

    bool isOpened = true;
    public int coinsCost = 10;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isOpened)
        {
            animator.SetTrigger("Trigger");
            isOpened = false;
            player = collision.gameObject.GetComponent<PlayerCoins>();
            player.coinCount += coinsCost;
        }
    }
}
