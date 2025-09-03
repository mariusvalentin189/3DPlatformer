using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WispAnimationsTrigger : MonoBehaviour
{
    [SerializeField] Enemy wispEnemy;
    public void StartAttacking()
    {
        wispEnemy.StartAttack();
    }
    public void EndAttacking()
    {
        wispEnemy.EndAttack();
    }
    public void EndTakeDamage()
    {
        wispEnemy.EndTakeDamage();
    }
}
