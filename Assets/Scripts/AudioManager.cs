using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private void Awake()
    {
        instance = this;

    }
    public AudioSource sound;
    public AudioSource music;
    [Header("GameSounds")]
    [SerializeField] AudioClip jump;
    [SerializeField] AudioClip jumpOnEnemy;
    [SerializeField] AudioClip coinCollect;
    [SerializeField] AudioClip diamondCollect;
    [SerializeField] AudioClip heartCollect;
    [SerializeField] AudioClip checkpoint;
    [SerializeField] AudioClip trampoline;
    [SerializeField] AudioClip[] steps;
    [SerializeField] AudioClip swordHitWood;
    private AudioClip step;

    [Header("Game Music")]
    [SerializeField] AudioClip gameMusic;

    [Header("UI Sounds")]
    [SerializeField] AudioClip buttonClick;

    public void SetSoundVolume(float volume)
    {
        sound.volume = volume;
    }
    public void SetMusicVolume(float volume)
    {
        music.volume = volume;
    }
    public void PlayJumpSound()
    {
        sound.PlayOneShot(jump);
    }
    public void PlayJumpOnEnemySound()
    {
        sound.PlayOneShot(jumpOnEnemy);
    }
    public void PlayCoinCollectSound()
    {
        sound.PlayOneShot(coinCollect);
    }
    public void PlayDiamondCollectSound()
    {
        sound.PlayOneShot(diamondCollect);
    }
    public void PlayHeartCollectSound()
    {
        sound.PlayOneShot(heartCollect);
    }
    public void PlayButtonClickSound()
    {
        sound.PlayOneShot(buttonClick);
    }
    public void PlayCheckpointSound()
    {
        sound.PlayOneShot(checkpoint);
    }
    public void PlayTrampolineSound()
    {
        sound.PlayOneShot(trampoline);
    }
    public void PlayStepSound()
    {
        sound.PlayOneShot(step);
    }
    public void ChangeStepSound(int index)
    {
	    if(step!=steps[index])
	        step=steps[index];
    }
    public void HitWoord()
    {
        sound.PlayOneShot(swordHitWood);
    }
	
}
