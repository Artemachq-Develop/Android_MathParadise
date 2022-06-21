using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource musicManager;
    public AudioClip toHellButtonSound;
    public AudioClip toParadiseButtonSound;
    public AudioClip correctSound;
    public AudioClip multiXCorrectSound_lvl1;
    public AudioClip multiXCorrectSound_lvl2;
    public AudioClip incorrectSound;
    public AudioClip healSound;
    public AudioClip bladeAttackSound;
    public AudioClip heartBeatSound;
    public AudioClip clearTextSound;
    public AudioClip[] gameOverRu;
    public AudioClip[] gameOverEng;

    public bool isSound;
    public Game_SaveLoad saveLoad;

    //ARRAY
    public AudioClip[] buttonClickSound;

    //EasterEggSounds
    public AudioClip[] easternEgg666Sound;

    public void PlaySound(AudioClip soundLoad)
    {
        if (isSound)
        {
            audioSource.PlayOneShot(soundLoad);
        }
    }

    public void RandomClickButtonSound()
    {
        PlaySound(buttonClickSound[Random.Range(0, buttonClickSound.Length)]);
    }

    public void ToHellButtonSound(bool isHell)
    {
        if (isHell)
        {
            PlaySound(toHellButtonSound);
        }
        else
        {
            PlaySound(toParadiseButtonSound);
        }
    }
    
    public void TurnOffSoundButton()
    {
        isSound = false;
        RandomClickButtonSound();
        musicManager.enabled = false;
    }
    
    public void TurnOnSoundButton()
    {
        isSound = true;
        RandomClickButtonSound();
        musicManager.enabled = true;
    }
}