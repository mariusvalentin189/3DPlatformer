using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogAnimationsTrigger : MonoBehaviour
{
    [SerializeField] FrogEnemy frog;
    public void StartAttacking()
    {
        frog.StartAttack();
    }
    public void EndAttacking()
    {
        frog.EndAttack();
    }
    public void EndTakeDamage()
    {
        frog.EndTakeDamage();
    }
    public void CanNotMove()
    {
        frog.CanNotMove();
    }
    public void CanMove()
    {
        frog.CanMove();
    }
}
