using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Collectible
{
    public override void OnTriggerEnter(Collider other)
    {
        if (!collided)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.GetComponent<HealthManager>().AddLife();
                sound.PlayHeartCollectSound();
                Destroy(gameObject);
                collided = true;
            }
        }
    }
}
