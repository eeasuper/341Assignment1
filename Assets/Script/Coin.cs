using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Coin : MonoBehaviour
{
    private static Queue<GameObject> list = new Queue<GameObject>();

    private void Start() {
        Player.Instance.OnCoinTouched += Instance_OnCoinTouched;
    }

    private void Instance_OnCoinTouched(object sender, Player.OnCoinTouchedEventArgs e) {
        list.Enqueue(e.coin.gameObject);
        e.coin.SetActive(false);
        Invoke("makeCoinReappear", 4f);
    }
        
    private void makeCoinReappear() {
        list.Dequeue().SetActive(true);
    }

}
