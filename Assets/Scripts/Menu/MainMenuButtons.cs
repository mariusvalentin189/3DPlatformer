using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject loadingBarCanvas,mainMenuCanvas;
    [SerializeField] private Image loadingScreenImage;
    [SerializeField] private Sprite lobbyLevelSprite;
    [SerializeField] private Sprite tutorialLevelSprite;
    [SerializeField] private LoadingBar loadingBar;
    [SerializeField] private Button continueLevelButton;
    [SerializeField] private Level[] levels;
    [SerializeField] private GameObject newGameConfirmPanel;
    [SerializeField] private GameObject tutorialConfirmPanel;
    [SerializeField] private Transform characterSpawnPoint;
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
        loadingBar.levelName = "LOBBY";
        mainMenuCanvas.SetActive(false);
    }
    public void ContinueGame()
    {
        loadingBarCanvas.SetActive(true);
        loadingScreenImage.sprite = lobbyLevelSprite;
        loadingBar.operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        loadingBar.levelName = "LOBBY";
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
        loadingBar.levelName = "TUTORIAL";
        mainMenuCanvas.SetActive(false);
    }
}
