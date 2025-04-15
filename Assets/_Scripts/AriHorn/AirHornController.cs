using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirHornController : MonoBehaviour
{
    [SerializeField] private Sprite buttonNonActiveSprite;
    [SerializeField] private Sprite buttonActiveSprite;
    [SerializeField] private Image[] buttonImages;
    [SerializeField] private AudioClip[] audios;
    [SerializeField] private ButtonClickSound airHornClickSound;

    private Image currentButtonImage;
    private void Start()
    {
        SetSound(0);
    }

    public void SetSound(int id)
    {
        airHornClickSound.AudioClip = audios[id];
        if(currentButtonImage) currentButtonImage.sprite = buttonNonActiveSprite;

        currentButtonImage = buttonImages[id];
        currentButtonImage.sprite = buttonActiveSprite;
    }
}
