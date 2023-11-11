using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class SliderToggle : MonoBehaviour
{
    public float minPosX;
    public float maxPosX;

    public RectTransform point;

    public Toggle toggle;
    //public Image back;
    public float duration;
    public void Init()
    {
        EventChanged(toggle.isOn);

        toggle.onValueChanged.AddListener(x =>
        {
            EventChanged(x);
        });
    }
    private void EventChanged(bool x)
    {
        float target = x ? maxPosX : minPosX;
        point.DOAnchorPosX(target, duration)/*.OnComplete(() => { back.SetActive(x); })*/;
        //back.DOFillAmount(x ? 0.8f : 0.2f, duration);
    }
}
