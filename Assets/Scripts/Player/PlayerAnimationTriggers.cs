using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    public Player player;
    public void PlayLandSound()
    {
        player.PlayJumpSound();
    }
    public void SpawnDustParticleLeft()
    {
        if (player.currentState == PlayerState.moving)
        {
            player.SpawnDustParticleLeft();
            player.PlayStepSound();
        }
    }
    public void SpawnDustParticleRight()
    {
        if (player.currentState == PlayerState.moving)
        {
            player.SpawnDustParticleRight();
            player.PlayStepSound();
        }
    }
    public void EnableSwordColldier()
    {
        player.EnableSwordColldier();
    }
    public void DisableSwordColldier()
    {
        player.DisableSwordColldier();
    }
    public void EndIdle()
    {
        player.EndIdle();
    }
    public void EndAttacking()
    {
        player.EndAttacking();
    }
    public void EndDodge()
    {
        player.EndDodge();
    }
}
