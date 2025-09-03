using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyBindButton : MonoBehaviour
{
    public KeyCode currentKey;
    public TMP_Text currentKeyText;
    public bool WasChanged { get; set; }
    public void SetKey(KeyCode key,string text)
    {
        if (currentKeyText.text != text)
            WasChanged = true;
        else WasChanged = false;
        currentKey = key;
        currentKeyText.text = text;
    }
    public void SetTextColor(Color c)
    {
        currentKeyText.color = c;
    }    
}
