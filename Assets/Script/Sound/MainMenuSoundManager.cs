using UnityEngine;

public class MainMenuSoundManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip openCaseSound;
    
    public AudioClip[] buttonClickSound;

    public bool isSound = true;
    
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
    
    public void TurnOffSoundButton()
    {
        isSound = false;
        RandomClickButtonSound();
    }
    
    public void TurnOnSoundButton()
    {
        isSound = true;
        RandomClickButtonSound();
    }
}
