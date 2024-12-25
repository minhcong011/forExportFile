using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonBaseDontDestroyOnLoad<AudioManager>
{
    [SerializeField] private AudioSource bgMusic;

    [SerializeField] private AudioClip[] handTapSound;
    [SerializeField] private AudioClip[] hammerTapSound;
    [SerializeField] private AudioClip knifeSound;
    [SerializeField] private AudioSource fireSound;
    [SerializeField] private AudioClip boomSound;
    [SerializeField] private AudioClip levelUpSound;
    private void Start()
    {
        if (!GameCache.GC.soundOff) bgMusic.Play();
    }
    public void PlayBGMusic(bool isPlay)
    {
        if (isPlay) bgMusic.Play();
        else bgMusic.Stop();
    }
    public void PlayTapSound()
    {
        switch (GameCache.GC.currentWeaponType)
        {
            case WeaponType.Hand: PlayHandTapSound(); break;
            case WeaponType.Hammer: PlayHammerTapSound(); break;
            case WeaponType.Knife: PlayKnifeSound(); break;
            case WeaponType.Fire: PlayFireSound(); break;
            case WeaponType.Boom: PlayBoomSound(); break;

        }
    }
    private void PlayHandTapSound()
    {
        AudioClip clipToPlay = handTapSound[Random.Range(0, handTapSound.Length)];

        PlaySound(clipToPlay);
    }
    private void PlayHammerTapSound()
    {
        AudioClip clipToPlay = hammerTapSound[Random.Range(0, handTapSound.Length)];

        PlaySound(clipToPlay);
    }
    private void PlayKnifeSound()
    {
        PlaySound(knifeSound);
    }
    private void PlayFireSound()
    {
        if (!fireSound.isPlaying) fireSound.Play();
    }
    public void StopFireSound()
    {
        if(fireSound.isPlaying)
        fireSound.Stop();
    }
    private void PlayBoomSound()
    {
        PlaySound(boomSound);
        if (!fireSound.isPlaying)
            fireSound.Play();
    }
    public void PlayLevelUpSound()
    {
        PlaySound(levelUpSound);
    }
    IEnumerator PlayIncreaseVolume(AudioSource audioSource, float time, float minVolume)
    {
        float currentTime = 0;
        float maxVolume = audioSource.volume;
        audioSource.Play();
        audioSource.volume = minVolume;

        while(currentTime < time)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(minVolume, maxVolume, currentTime / time);
            yield return null;
        }
    }
    private AudioSource StopSoundWithDecreaseVolume(AudioSource audioSource)
    {
        AudioSource newAudioSource = gameObject.AddComponent<AudioSource>(); 
        newAudioSource.clip = audioSource.clip;
        newAudioSource.volume = audioSource.volume;
        newAudioSource.loop = audioSource.loop;
        AudioSource tmp = audioSource;
        StartCoroutine(StopSoundWithDecreaseVolume(tmp));
        IEnumerator StopSoundWithDecreaseVolume(AudioSource audioSource)
        {
         
            while (audioSource.volume > 0)
            {
                audioSource.volume -= Time.deltaTime * 0.5f;
                yield return null;
            }
            Destroy(audioSource);

        }
        newAudioSource.Stop();
        return newAudioSource;
    }
    
    public void PlaySound(AudioClip audioClip)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        PlaySound(audioSource);
    }
    public AudioSource PlaySound(string name)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        AudioClip audioClip = Resources.Load("Sounds/" + name) as AudioClip;
        audioSource.clip = audioClip;
        PlaySound(audioSource);
        return audioSource;
    }
    public AudioSource PlaySound(string name, float volume)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        AudioClip audioClip = Resources.Load("Sounds/" + name) as AudioClip;
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        PlaySound(audioSource);
        return audioSource;
    }
    public void PlaySoundWithPitch(string name, float pitch)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        AudioClip audioClip = Resources.Load("Sounds/" + name) as AudioClip;
        audioSource.clip = audioClip;
        audioSource.pitch = pitch;
        PlaySound(audioSource);
    }
    public void PlaySound(AudioSource audioSource)
    {
        if (GameCache.GC.soundOff) return;
        audioSource.SetScheduledEndTime(AudioSettings.dspTime + audioSource.clip.length);
        audioSource.PlayScheduled(AudioSettings.dspTime);

        StartCoroutine(RemoveAudioSource(audioSource));
    }
    //public void PlaySound(string soundName, float pitch)
    //{
    //    AudioSource audioSource = gameObject.AddComponent<AudioSource>();
    //    AudioClip audioClip = Resources.Load("Sounds/" + soundName) as AudioClip;
    //    audioSource.clip = audioClip;
    //    audioSource.pitch = pitch;
    //    audioSource.SetScheduledEndTime(AudioSettings.dspTime + audioClip.length);
    //    audioSource.PlayScheduled(AudioSettings.dspTime);

    //    StartCoroutine(RemoveAudioSource(audioSource));
    //}
    private IEnumerator RemoveAudioSource(AudioSource audioSource)
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(audioSource);
    }
}
