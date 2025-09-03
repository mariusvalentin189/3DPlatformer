using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject coinPrefab;

    [Header("Loading Scenes")]
    [SerializeField] private Image loadingScreenImage;
    [SerializeField] private GameObject loadingBarPanel;
    [SerializeField] private LoadingBar loadingBar;

    [Header("Levels Images")]
    [SerializeField] private Sprite lobbyLevelSprite;
    private void Awake()
    {
        instance = this;
        //transform.SetParent(null);
    }
    public void StawnCoins(int ammount, Vector3 pos)
    {
        StartCoroutine(SpawnCoins(ammount, pos));
    }
    IEnumerator SpawnCoins(int ammount, Vector3 pos)
    {
        for (int i = 0; i < ammount; i++)
        {
            yield return new WaitForSeconds(0.25f);
            Instantiate(coinPrefab, pos, Quaternion.identity);
        }

        yield return null;
    }
    public void LoadLobbyLevel()
    {
        loadingBarPanel.SetActive(true);
        loadingScreenImage.sprite = lobbyLevelSprite;
        loadingBar.operation = SceneManager.LoadSceneAsync("LobbyLevel");
    }

}
