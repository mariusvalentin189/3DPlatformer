using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogAnimationsTrigger : MonoBehaviour
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
