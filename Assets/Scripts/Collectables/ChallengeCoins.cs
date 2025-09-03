using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeCoins : Coins
{
    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sound.PlayCoinCollectSound();
            Destroy(gameObject);
        }
    }
}
