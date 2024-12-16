using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseAvatarButton : MonoBehaviour
{
    [SerializeField] private GameObject sellectBg;
    [SerializeField] private GameObject unSellectBg;
    [SerializeField] private Image avatarIcon;

    private int id;
    public void Set(int buttonID, Sprite avatarSprite)
    {
        if (GameCache.GC.avatarID == buttonID) SetSellect(true);
        id = buttonID;
        avatarIcon.sprite = avatarSprite;
    }
    public void Sellect()
    {
        if (GameCache.GC.avatarID == id) return;
        EditProfilePanel.Instance.DeSellectButton(GameCache.GC.avatarID);
        GameCache.GC.avatarID = id;
        SetSellect(true);
        OpenUserProfileButton.Instance.UpdateUserIcon();
    }
    public void SetSellect(bool isSellect)
    {
        sellectBg.SetActive(isSellect);
        unSellectBg.SetActive(!isSellect);
    }
}
