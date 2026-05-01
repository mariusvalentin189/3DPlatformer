using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;

    [SerializeField] GameObject pauseMenuPannel,settingsPanel,controlsPanel,loadingBarPanel;
    [SerializeField] LoadingBar loadingBar;
    [SerializeField] Image loadingScreenImage;
    [SerializeField] Sprite lobbyLevelSprite;
    [SerializeField] Sprite mainMenuSprite;
    [SerializeField] SettingsButtons settingsButtons;
    bool isPaused, isInSettings, isInControls;

    public bool IsPaused {  get { return isPaused; } }

    private void Awake()
    {
        instance = this;
        transform.SetParent(null);
    }

    void Start()
    {
        pauseMenuPannel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused == false)
            {
                pauseMenuPannel.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
                isPaused = true;
            }
            else if(isPaused==true)
            {
                if(isInControls || isInSettings)
                {
                    BackToPauseMenu();
                }
                else
                {
                    pauseMenuPannel.SetActive(false);
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    Time.timeScale = 1;
                    isPaused = false;
                }
            }
        }
    }
    public void ResumeGame()
    {
        pauseMenuPannel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        isPaused = false;
        if (isInSettings)
        {
            settingsPanel.SetActive(false);
            isInSettings = false;
        }
        if(isInControls)
        {
            controlsPanel.SetActive(false);
            isInControls = false;
        }
    }
    public void GoSoSettings()
    {
        settingsPanel.SetActive(true);
        isInSettings = true;
    }
    public void GoToControls()
    {
        controlsPanel.SetActive(true);
        settingsButtons.ChangeButtonsToDefault();
        isInControls = true;
    }
    public void MainMenu()
    {
        Time.timeScale = 1;
        loadingBarPanel.SetActive(true);
        loadingScreenImage.sprite = mainMenuSprite;
        loadingBar.SetLevelName("MAIN MENU");
        loadingBar.operation = SceneManager.LoadSceneAsync("MainMenu");
    }
    public void BackToPauseMenu()
    {
        if (isInSettings)
        {
            settingsPanel.SetActive(false);
            pauseMenuPannel.SetActive(true);
            isInSettings = false;
        }
        else if (isInControls)
        {
            controlsPanel.SetActive(false);
            pauseMenuPannel.SetActive(true);
            isInControls = false;
        }
    }
    public void NextLevel()
    { 
        Time.timeScale = 1;
        loadingBarPanel.SetActive(true);
        loadingScreenImage.sprite = lobbyLevelSprite;
        loadingBar.operation=SceneManager.LoadSceneAsync("LobbyLevel");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void RestoreCheckpoint()
    {
        ResumeGame();
        RespawnTrigger.instance.RestoreCheckpoint();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
