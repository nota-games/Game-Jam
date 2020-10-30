using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    PlayerCoins player;

    public int coinsCost = 1;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            player = collision.gameObject.GetComponent<PlayerCoins>();
        player.coinCount += coinsCost;
        Destroy(gameObject);
    }
}
