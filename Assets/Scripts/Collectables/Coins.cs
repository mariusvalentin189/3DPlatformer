using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : Collectible
{
    public override void OnTriggerEnter(Collider other)
    {
        if (!collided)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<HealthManager>().AddCoins(1);
                sound.PlayCoinCollectSound();
                Destroy(gameObject);
                collided = true;
            }
        }
    }
}
