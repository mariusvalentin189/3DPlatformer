using System.Collections.Generic;
using UnityEngine;


public class EnemyDetector : MonoBehaviour
{
    public Enemy ClosestEnemy {  get; private set; }
    List<Enemy> enemies = new List<Enemy>();
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (!enemies.Contains(enemy))
            {
                enemies.Add(enemy);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemies.Contains(enemy))
            {
                enemies.Remove(enemy);
            }
        }
    }
    void GetClosestEnemy()
    {
        float closestDistance = transform.GetComponent<SphereCollider>().radius;
        foreach (Enemy enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                ClosestEnemy = enemy;
                closestDistance = distance;
            }
        }
    }
    private void Update()
    {
        if (enemies.Count > 0)
            GetClosestEnemy();
        else ClosestEnemy = null;
    }

    //Called by enemy ai when killed
    public void RemoveEnemy(Enemy enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
        }
    }
}
