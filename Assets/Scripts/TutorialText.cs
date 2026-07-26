using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialText : MonoBehaviour
{
    public TMP_Text moveText, jumpText, attackText, attackEnemiesText, doubleJumpText, dodgeText;
    private void Start()
    {
        UpdateText();
    }
    public void UpdateText()
    {
        moveText.text = ($"Press {PlayerInput.upKey}, {PlayerInput.leftKey}, {PlayerInput.downKey}, {PlayerInput.rightKey} to move");
        jumpText.text = ($"Press {PlayerInput.jumpKey} to jump");
        attackText.text = ($"Press Left Mouse to attack. Boxes and barrels can be destroyed to earn coins");
        attackEnemiesText.text = ($"Press Left Mouse to attack the enemies");
        doubleJumpText.text = ($"Press {PlayerInput.jumpKey} while in the air to jump one more time");
        dodgeText.text = ($"Press {PlayerInput.dodgeKey} to dodge. You are invulnerable while dodging");

    }
}
