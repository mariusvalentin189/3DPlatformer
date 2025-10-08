using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyBindButton : MonoBehaviour
{
    [SerializeField] KeyCode currentKey;
    [SerializeField] TMP_Text currentKeyText;
    public bool WasChanged { get; set; }
    public KeyCode CurrentKey { get { return currentKey; } }
    public TMP_Text CurrentKeyText { get { return currentKeyText; } }   
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
    public void SetCurrentKey(KeyCode key)
    {
        currentKey = key;
    }
    public void SetCurrentKeyText(string key)
    {
        currentKeyText.text += key;
    }
}
