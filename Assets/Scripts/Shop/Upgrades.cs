using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Upgrades : MonoBehaviour
{
    [SerializeField] private bool diamonds;
    [SerializeField] private int coinsCost;
    [SerializeField] private int diamondsCost;
    [SerializeField] private TMP_Text buyText;
    [SerializeField] private TMP_Text ownedText;
    [SerializeField] private TMP_Text diamondsCostText;
    [SerializeField] private TMP_Text coinsCostText;
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text diamondsText;
    [SerializeField] private Button buyButton;
    public Stat statusAffected;
    public bool unlocked = false;

    private int coinsCount;
    private int diamondsCount;

    private void Start()
    {
        LoadStats();
        CheckLocked();
    }

    void CheckLocked()
    {
        if (unlocked)
        {
            ownedText.gameObject.SetActive(true);
            buyText.gameObject.SetActive(false);
            coinsCostText.gameObject.SetActive(false);
            diamondsCostText.gameObject.SetActive(false);
        }
        else
        {
            ownedText.gameObject.SetActive(false);
            buyText.gameObject.SetActive(true);
            if (diamonds)
            {
                diamondsCostText.gameObject.SetActive(true);
                coinsCostText.gameObject.SetActive(false);
                diamondsCostText.text = diamondsCost + "x";
                if (diamondsCost <= diamondsCount && coinsCost <= coinsCount)
                    buyButton.interactable = true;
                else buyButton.interactable = false;
            }
            else
            {
                coinsCostText.gameObject.SetActive(true);
                diamondsCostText.gameObject.SetActive(false);
                coinsCostText.text = coinsCost + "x";
                if (coinsCost <= coinsCount)                  
                    buyButton.interactable = true;
                else buyButton.interactable = false;
            }
        }
    }
    void LoadStats()
    {
        if (PlayerPrefs.HasKey("CoinsCount"))
            coinsCount = PlayerPrefs.GetInt("CoinsCount");
        if (PlayerPrefs.HasKey("DiamondsCount"))
            diamondsCount = PlayerPrefs.GetInt("DiamondsCount");
        int v = 0;
        if (statusAffected == Stat.DoubleJump)
        {
            if (PlayerPrefs.HasKey("JumpUnlocked"))
                v = PlayerPrefs.GetInt("JumpUnlocked");
            if (v == 0)
                unlocked = false;
            else unlocked = true;
        }
        else
        {
            if (PlayerPrefs.HasKey("DodgeUnlocked"))
                v = PlayerPrefs.GetInt("DodgeUnlocked");
            if (v == 0)
                unlocked = false;
            else unlocked = true;
        }
        UpdateUi();
    }
    void UpdateUi()
    {
        coinsText.text = coinsCount + "x";
        diamondsText.text = diamondsCount + "x";
    }
    public void Buy()
    {
        if (diamonds)
        {
            diamondsCount -= diamondsCost;
            coinsCount -= coinsCost;
            PlayerPrefs.SetInt("DiamondsCount", diamondsCount);
            PlayerPrefs.SetInt("CoinsCount", coinsCount);
        }
        else
        {
            coinsCount -= coinsCost;
            PlayerPrefs.SetInt("CoinsCount", coinsCount);
        }
        unlocked = true;
        if (statusAffected == Stat.DoubleJump)
            PlayerPrefs.SetInt("JumpUnlocked", 1);
        else PlayerPrefs.SetInt("DodgeUnlocked", 1);
        buyButton.interactable = false;
        EventSystem.current.SetSelectedGameObject(null);
        CheckLocked();
        UpdateUi();
    }
    public enum Stat
    {
        DoubleJump,
        Dodge
    }
}
