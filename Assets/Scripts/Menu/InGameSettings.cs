using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class InGameSettings : MonoBehaviour
{
    public static InGameSettings instance;
    private void Awake()
    {
        instance = this;
    }
    private float sensitivity;
    private float soundVolume;
    private float musicVolume;
    [SerializeField] CameraController cameraController;

    void Start()
    {
        //Set frame rate
        Application.targetFrameRate = 60;

        SettingsManager.LoadSettings();
        sensitivity = SettingsManager.sensitivity;
        soundVolume = SettingsManager.soundVolume;
        musicVolume = SettingsManager.musicVolume;
        SetSensitivity();
        SetVolume();
    }

    void SetSensitivity()
    {
        cameraController.SetSensitivity(sensitivity);
    }
    public void SetVolume()
    {
        AudioManager.instance.SetSoundVolume(soundVolume);
        AudioManager.instance.SetMusicVolume(musicVolume);
    }
    public void SetChangesInGame()
    {
        sensitivity = SettingsManager.sensitivity;
        soundVolume = SettingsManager.soundVolume;
        musicVolume = SettingsManager.musicVolume;
        SetSensitivity();
        SetVolume();
    }
}
