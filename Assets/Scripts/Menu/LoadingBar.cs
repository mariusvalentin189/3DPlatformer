using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LoadingBar : MonoBehaviour
{
    public AsyncOperation operation;
    [SerializeField] string levelName;
    [SerializeField] Slider loadingBar;
    [SerializeField] TMP_Text levelNameText;
    private void Start()
    {
        levelNameText.text = levelName;
    }
    void Update()
    {
        if (loadingBar != null && operation != null)
            loadingBar.value = operation.progress;
    }
    public void SetLevelName(string name)
    {
        levelName = name;
    }
}
