using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    [SerializeField] int damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Props"))
        {
            AudioManager.instance.HitWoord();
            Props prop = other.GetComponent<Props>();
            prop.DestroyObject();
        }
        else if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
        }
    }
}
