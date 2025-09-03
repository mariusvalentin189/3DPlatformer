using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput
{
    public static KeyCode upKey=KeyCode.W;
    public static KeyCode downKey=KeyCode.S;
    public static KeyCode leftKey=KeyCode.A;
    public static KeyCode rightKey=KeyCode.D;
    public static KeyCode jumpKey=KeyCode.Space;
    public static KeyCode useKey = KeyCode.E;
    public static KeyCode dodgeKey = KeyCode.LeftShift;
    public static KeyCode equipWeaponKey = KeyCode.H;
    public static void SaveKeys(KeyCode up,KeyCode down,KeyCode left, KeyCode right, KeyCode jump,KeyCode use,KeyCode dodge,KeyCode equip)
    {
        upKey = up;
        downKey = down;
        leftKey = left;
        rightKey = right;
        jumpKey = jump;
        useKey = use;
        dodgeKey = dodge;
        equipWeaponKey = equip;
    }
}
