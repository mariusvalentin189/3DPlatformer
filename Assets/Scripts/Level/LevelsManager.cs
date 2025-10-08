using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelsManager : MonoBehaviour
{
    [SerializeField] Level[] levels;
    [SerializeField] TMP_Text coinsText, livesText, diamondsText;
    int coinsCount, livesCount, diamondsCount;
    private void Awake()
    {
        LoadLevels();
    }
    public void LoadLevels()
    {
        coinsCount = PlayerPrefs.GetInt("CoinsCount");
        livesCount = PlayerPrefs.GetInt("LivesCount");
        diamondsCount = PlayerPrefs.GetInt("DiamondsCount");
        coinsText.text = coinsCount.ToString() + "x";
        livesText.text = livesCount.ToString() + "x";
        diamondsText.text = diamondsCount.ToString() + "x";
        for(int i=0;i<levels.Length;i++)
        {
            int u = PlayerPrefs.GetInt("LevelUnlocked" + i);
            if (u == 1)
                levels[i].unlocked = true;
            else levels[i].unlocked = false;

            int c = PlayerPrefs.GetInt("LevelCompleted" + i);
            if (c == 1)
                levels[i].completedFirstTime = true;
            else levels[i].completedFirstTime = false;
        }
    }
}
