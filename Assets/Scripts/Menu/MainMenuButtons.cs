using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] GameObject loadingBarCanvas,mainMenuCanvas;
    [SerializeField] Image loadingScreenImage;
    [SerializeField] Sprite lobbyLevelSprite;
    [SerializeField] Sprite tutorialLevelSprite;
    [SerializeField] LoadingBar loadingBar;
    [SerializeField] Button continueLevelButton;
    [SerializeField] Level[] levels;
    [SerializeField] GameObject newGameConfirmPanel;
    [SerializeField] GameObject tutorialConfirmPanel;
    [SerializeField] Transform characterSpawnPoint;
    private void Awake()
    {
        continueLevelButton.interactable = false;
        if (PlayerPrefs.HasKey("LevelCompleted"+0))
            if(PlayerPrefs.GetInt("LevelCompleted"+0)==1)
                continueLevelButton.interactable = true;

    }
    public void NewGame()
    {
        if (continueLevelButton.interactable == true)
        {
            if (newGameConfirmPanel.activeSelf == false)
            {
                mainMenuCanvas.SetActive(false);
                newGameConfirmPanel.SetActive(true);
                return;
            }
        }
        PlayerPrefs.SetInt("LivesCount", 5);
        PlayerPrefs.SetInt("DiamondsCount", 0);
        PlayerPrefs.SetInt("CoinsCount", 0);
	    PlayerPrefs.SetInt("Weapon", 0);
        PlayerPrefs.SetInt("LevelUnlocked" + 0, 1);
        PlayerPrefs.SetInt("LevelCompleted" + 0, 0);
        for(int i=1;i<levels.Length;i++)
        {
            PlayerPrefs.SetInt("LevelUnlocked" + i, 0);
            PlayerPrefs.SetInt("LevelCompleted" + i, 0);
        }
        loadingBarCanvas.SetActive(true);
        loadingScreenImage.sprite = lobbyLevelSprite;
        loadingBar.operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        loadingBar.SetLevelName("LOBBY");
        mainMenuCanvas.SetActive(false);
    }
    public void ContinueGame()
    {
        loadingBarCanvas.SetActive(true);
        loadingScreenImage.sprite = lobbyLevelSprite;
        loadingBar.operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        loadingBar.SetLevelName("LOBBY");
        mainMenuCanvas.SetActive(false);
    }
    public void Tutorial()
    {
        if(tutorialConfirmPanel.activeSelf==false)
        {
            tutorialConfirmPanel.SetActive(true);
            return;
        }
        loadingBarCanvas.SetActive(true);
        loadingScreenImage.sprite = tutorialLevelSprite;
        loadingBar.operation = SceneManager.LoadSceneAsync("TutorialLevel");
        loadingBar.SetLevelName("TUTORIAL");
        mainMenuCanvas.SetActive(false);
    }
}
