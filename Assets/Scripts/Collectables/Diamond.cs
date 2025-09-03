using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : Coins
{
    public override void OnTriggerEnter(Collider other)
    {
        if (!collided)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<HealthManager>().AddDiamonds(1);
                sound.PlayDiamondCollectSound();
                Destroy(gameObject);
                collided = true;
            }
        }
    }
}
