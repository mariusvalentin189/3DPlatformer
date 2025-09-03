using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopButtons : MonoBehaviour
{
    [SerializeField] private GameObject loadingBarPanel;
    [SerializeField] private LoadingBar loadingBar;
    [SerializeField] private Image loadingScreenImage;
    [SerializeField] private Sprite lobbyLevelSprite;
    public int coinsCount;
    public int diamondsCount;
    public static ShopButtons instance;
    private void Awake()
    {
        instance = this;
        LoadStats(); 
    }
    public void Back()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        loadingBarPanel.SetActive(true);
        loadingScreenImage.sprite = lobbyLevelSprite;
        loadingBar.operation=SceneManager.LoadSceneAsync("LobbyLevel");
        loadingBar.levelName = "LOBBY";
    }
    public void LoadStats()
    {
        if (PlayerPrefs.HasKey("CoinsCount"))
            coinsCount = PlayerPrefs.GetInt("CoinsCount");
        if (PlayerPrefs.HasKey("DiamondsCount"))
            diamondsCount = PlayerPrefs.GetInt("DiamondsCount");
    }
}
