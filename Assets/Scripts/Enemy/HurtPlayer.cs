using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    private Vector3 direction;
    [SerializeField] private float knockbackForce;
    [SerializeField] private Enemy enemy;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            direction = (other.transform.position - transform.position);
            other.GetComponent<HealthManager>().TakeDamage(direction, knockbackForce);
        }
    }
}
