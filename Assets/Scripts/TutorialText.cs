using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialText : MonoBehaviour
{
    public TMP_Text moveText, jumpText, drawSwordText, attackText, attackEnemiesText, doubleJumpText, dodgeText;
    private void Start()
    {
        UpdateText();
    }
    public void UpdateText()
    {
        moveText.text = ($"Press {PlayerInput.upKey}, {PlayerInput.leftKey}, {PlayerInput.downKey}, {PlayerInput.rightKey} to move");
        jumpText.text = ($"Press {PlayerInput.jumpKey} to jump");
        drawSwordText.text = ($"Press {PlayerInput.equipWeaponKey} to draw the sword");
        attackText.text = ($"Press Left Mouse to attack. Boxes and barrels can be destroyed to earn coins");
        attackEnemiesText.text = ($"Enemies can be damaged by jumping on their head or attacking them with the sword");
        doubleJumpText.text = ($"Once double jump is unlocked, press {PlayerInput.jumpKey} while in the air to jump one more time");
        dodgeText.text = ($"Once dodge is unlocked, press {PlayerInput.dodgeKey} to dodge");

    }
}
