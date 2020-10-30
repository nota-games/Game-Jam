using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCoins : MonoBehaviour
{
    public int coinCount;
    public Text text;
    
    void Update()
    {
        text.text = " Coins: " + coinCount;
    }
}
