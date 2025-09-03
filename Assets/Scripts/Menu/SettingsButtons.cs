using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SettingsButtons : MonoBehaviour
{
    [Header("Volume")]
    public Slider soundVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sensitivitySlider;
    [Header("Resolution")]
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullScreenToggle;
    [Header("Controls")]
    public KeyBindButton[] keyButtons;
    private Resolution[] resolutions;
    private int selectedResolutionIndex;
    private float soundVolume, musicVolume, sensitivity;
    private bool fullScreen;
    private int savedIndex;
    private GameObject currentPressed;
    [SerializeField] Color[] defaultColors;
    [SerializeField] Color defaultColorText;
    [SerializeField] Color[] changedColors;
    [SerializeField] Color changedColorText;
    private void Start()
    {
        SettingsManager.LoadSettings();
        soundVolumeSlider.value = SettingsManager.soundVolume;
        musicVolumeSlider.value = SettingsManager.musicVolume;
        sensitivitySlider.value = SettingsManager.sensitivity;
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> resolutionsList = new List<string>();
        int currentResolutionIndex = 0;
        for(int i=0;i<resolutions.Length;i++)
        {
            if (resolutions[i].width == SettingsManager.screenWidth && resolutions[i].height == SettingsManager.screenHeight)
                currentResolutionIndex = i;
            string res = resolutions[i].width + " x " + resolutions[i].height;
            resolutionsList.Add(res);
        }
        resolutionDropdown.AddOptions(resolutionsList);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        fullScreenToggle.isOn = SettingsManager.fullScreen;
        savedIndex = currentResolutionIndex;
        LoadInputs();
        AudioManager.instance.SetSoundVolume(soundVolume);
        AudioManager.instance.SetMusicVolume(musicVolume);
    }
    public void SetSoundVolume(float value)
    {
        soundVolume = value;
        AudioManager.instance.SetSoundVolume(soundVolume);
        SettingsManager.soundVolume = soundVolume;
    }
    public void SetMusicVolume(float value)
    {
        musicVolume = value;
        AudioManager.instance.SetMusicVolume(musicVolume);
        SettingsManager.musicVolume = musicVolume;
    }
    public void SetSensitivity(float value)
    {
        sensitivity = value;
        SettingsManager.sensitivity = sensitivity;
    }
    public void SetFullScreen(bool value)
    {
        fullScreen = value;
        SettingsManager.SetFullScreen(fullScreen);
    }
    public void SetResolution(int index)
    {
        selectedResolutionIndex = index;
        savedIndex = selectedResolutionIndex;
        SettingsManager.SetResolution(selectedResolutionIndex);
    }
    public void Back()
    {
        InGameSettings.instance.SetChangesInGame();
        SettingsManager.SaveSettings();
        EventSystem.current.SetSelectedGameObject(null);
        soundVolumeSlider.value = SettingsManager.soundVolume;
        soundVolumeSlider.value = SettingsManager.soundVolume;
        sensitivitySlider.value = SettingsManager.sensitivity;
        fullScreenToggle.isOn = SettingsManager.fullScreen;
        resolutionDropdown.value = savedIndex;
        resolutionDropdown.RefreshShownValue();
    }
    private void OnGUI()
    {
        if(currentPressed!=null)
        {
            Event k = Event.current;
            bool alreadyAKey = false;
            if(k.isKey)
            {
                Debug.Log(k.keyCode);
                for (int i = 0; i < keyButtons.Length && !alreadyAKey; i++)
                    if (keyButtons[i].currentKey == k.keyCode)
                        alreadyAKey = true;
                if (!alreadyAKey)
                {
                    currentPressed.GetComponent<KeyBindButton>().currentKey = k.keyCode;
                    ChangeButtonState(currentPressed, true);
                    currentPressed.GetComponent<KeyBindButton>().currentKeyText.text = k.keyCode.ToString();
                }
                currentPressed = null;
            }
        }
    }
    public void SetCurrent(GameObject obj)
    {
        currentPressed = obj;
    }
    void SetButtons()
    {
        keyButtons[0].SetKey(PlayerInput.upKey, PlayerInput.upKey.ToString());
        keyButtons[1].SetKey(PlayerInput.downKey, PlayerInput.downKey.ToString());
        keyButtons[2].SetKey(PlayerInput.leftKey, PlayerInput.leftKey.ToString());
        keyButtons[3].SetKey(PlayerInput.rightKey, PlayerInput.rightKey.ToString());
        keyButtons[4].SetKey(PlayerInput.jumpKey, PlayerInput.jumpKey.ToString());
        keyButtons[5].SetKey(PlayerInput.useKey, PlayerInput.useKey.ToString());
        keyButtons[6].SetKey(PlayerInput.equipWeaponKey, PlayerInput.equipWeaponKey.ToString());
        keyButtons[7].SetKey(PlayerInput.dodgeKey, PlayerInput.dodgeKey.ToString());
        foreach(KeyBindButton btn in keyButtons)
        {
            ChangeButtonState(btn.gameObject, btn.WasChanged);
        }
    }
    public void SaveInputs()
    {
        PlayerPrefs.SetString("UpKey", keyButtons[0].currentKeyText.text);
        PlayerPrefs.SetString("DownKey", keyButtons[1].currentKeyText.text);
        PlayerPrefs.SetString("LeftKey", keyButtons[2].currentKeyText.text);
        PlayerPrefs.SetString("RightKey", keyButtons[3].currentKeyText.text);
        PlayerPrefs.SetString("JumpKey", keyButtons[4].currentKeyText.text);
        PlayerPrefs.SetString("UseKey", keyButtons[5].currentKeyText.text);
        PlayerPrefs.SetString("EquipKey", keyButtons[6].currentKeyText.text);
        PlayerPrefs.SetString("DodgeKey", keyButtons[7].currentKeyText.text);
        PlayerInput.SaveKeys(keyButtons[0].currentKey, keyButtons[1].currentKey, keyButtons[2].currentKey, keyButtons[3].currentKey, keyButtons[4].currentKey, keyButtons[5].currentKey,keyButtons[6].currentKey,keyButtons[7].currentKey);
        EventSystem.current.SetSelectedGameObject(null);
    }
    public void LoadInputs()
    {
        string key;
        if(PlayerPrefs.HasKey("UpKey"))
        {
            key = PlayerPrefs.GetString("UpKey");
            PlayerInput.upKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), key);
        }
        if (PlayerPrefs.HasKey("DownKey"))
        {
            key = PlayerPrefs.GetString("DownKey");
            PlayerInput.downKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), key);
        }
        if (PlayerPrefs.HasKey("LeftKey"))
        {
            key = PlayerPrefs.GetString("LeftKey");
            PlayerInput.leftKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), key);
        }
        if (PlayerPrefs.HasKey("RightKey"))
        {
            key = PlayerPrefs.GetString("RightKey");
            PlayerInput.rightKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), key);
        }
        if (PlayerPrefs.HasKey("JumpKey"))
        {
            key = PlayerPrefs.GetString("JumpKey");
            PlayerInput.jumpKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), key);
        }
        if (PlayerPrefs.HasKey("UseKey"))
        {
            key = PlayerPrefs.GetString("UseKey");
            PlayerInput.useKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), key);
        }
        if (PlayerPrefs.HasKey("EquipKey"))
        {
            key = PlayerPrefs.GetString("EquipKey");
            PlayerInput.equipWeaponKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), key);
        }
        if (PlayerPrefs.HasKey("DodgeKey"))
        {
            key = PlayerPrefs.GetString("DodgeKey");
            PlayerInput.dodgeKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), key);
        }
        SetButtons();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void SetDefaultControls()
    {
        PlayerPrefs.SetString("UpKey", "W");
        PlayerPrefs.SetString("DownKey", "S");
        PlayerPrefs.SetString("LeftKey", "A");
        PlayerPrefs.SetString("RightKey", "D");
        PlayerPrefs.SetString("JumpKey", "Space");
        PlayerPrefs.SetString("UseKey", "E");
        PlayerPrefs.SetString("EquipKey", "H");
        PlayerPrefs.SetString("DodgeKey", "LeftShift");
        PlayerInput.SaveKeys(keyButtons[0].currentKey, keyButtons[1].currentKey, keyButtons[2].currentKey, keyButtons[3].currentKey, keyButtons[4].currentKey, keyButtons[5].currentKey, keyButtons[6].currentKey, keyButtons[7].currentKey);
        LoadInputs();
        EventSystem.current.SetSelectedGameObject(null);
    }
    public void ChangeButtonsToDefault()
    {
        foreach (KeyBindButton k in keyButtons)
        {
            ChangeButtonState(k.gameObject, false);
            k.WasChanged = false;
            ChangeButtonState(k.gameObject, false);
        }
    }
    void ChangeButtonState(GameObject obj, bool modified)
    {
        Button b = obj.GetComponent<Button>();
        ColorBlock cb = b.colors;
        if (!modified)
        {
            cb.normalColor = defaultColors[0];
            cb.highlightedColor = defaultColors[1];
            cb.pressedColor = defaultColors[2];
            cb.selectedColor = defaultColors[3];
            obj.GetComponent<KeyBindButton>().SetTextColor(defaultColorText);
        }
        else
        {
            cb.normalColor = changedColors[0];
            cb.highlightedColor = changedColors[1];
            cb.pressedColor = changedColors[2];
            cb.selectedColor = changedColors[3];
            obj.GetComponent<KeyBindButton>().SetTextColor(changedColorText);
        }
        b.colors = cb;
    }
}
