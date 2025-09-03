using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimationsTrigger : MonoBehaviour
{
    [SerializeField] Enemy spiderEnemy;
    public void StartAttacking()
    {
        spiderEnemy.StartAttack();
    }
    public void EndAttacking()
    {
        spiderEnemy.EndAttack();
    }
    public void EndTakeDamage()
    {
        spiderEnemy.EndTakeDamage();
    }
}
