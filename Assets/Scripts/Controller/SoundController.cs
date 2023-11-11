using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SoundController : MonoBehaviour
{
    public AudioSource gunSource;
    public AudioSource throwSource;
    public AudioSource explotionSource;

    public AudioSource mainMenuSource;
    public AudioSource gameplaySource;
    public AudioSource clickSource;

    public AudioSource gameoverSource;
    public void PlaySound(soundEffect soundEffect)
    {
        switch (soundEffect)
        {
            case soundEffect.throwEffect:
                throwSource.PlayOneShot(throwSource.clip);
                break;
            case soundEffect.gunShoot:
                gunSource.PlayOneShot(gunSource.clip);
                break;
            case soundEffect.explotion:
                explotionSource.PlayOneShot(explotionSource.clip);
                break;
            case soundEffect.click:
                PlayClickSound();
                break;
            case soundEffect.gameover:
                PlayGameOverSound();
                break;
            case soundEffect.start:
                PlayMainMenuMusic();
                break;
            case soundEffect.gameplay:
                PlayGameplaySource();
                break;
            default:
                break;
        }

    }


    public void MuteEffect(bool mute)
    {
        gunSource.mute = !mute;
        throwSource.mute = !mute;
        explotionSource.mute = !mute;
        clickSource.mute = !mute; 

    }
    public void MuteMusic(bool mute)
    {
        gameplaySource.mute = !mute;
        mainMenuSource.mute = !mute;
        gameoverSource.mute = !mute;
    }

    private void PlayGameplaySource()
    {
        mainMenuSource.DOFade(0, 1f);
        gameplaySource.volume = 0;
        gameplaySource.Play();
        gameplaySource.DOFade(0.2f, 3f);
    }

    private void PlayMainMenuMusic()
    {
        mainMenuSource.volume = 0; 
        mainMenuSource.Play();
        mainMenuSource.DOFade(0.2f, 3f);
    }

    private void PlayClickSound()
    {
        clickSource.Play();
    }

    private void PlayGameOverSound()
    {
        gameplaySource.DOFade(0,0.5f).OnComplete(()=> {
            gameoverSource.Play();
        }); 
    }
}
