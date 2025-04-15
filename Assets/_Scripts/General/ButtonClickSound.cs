using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonClickSound : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private AudioClip audioClip;

    private AudioSource currentAudioSource;

    public AudioClip AudioClip { get => audioClip; set => audioClip = value; }

    public void OnPointerDown(PointerEventData eventData)
    {
        currentAudioSource = AudioManager.Instance.PlaySoundLoop(AudioClip);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Destroy(currentAudioSource);
    }
}
