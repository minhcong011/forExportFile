using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenUserProfileButton : SingletonBase<OpenUserProfileButton>
{
    [SerializeField] private Sprite[] avatarIcons;
    [SerializeField] private Image userIcon;

    private void Start()
    {
        UpdateUserIcon();
    }
    public void UpdateUserIcon()
    {
        userIcon.sprite = avatarIcons[GameCache.GC.avatarID];
    }
}
