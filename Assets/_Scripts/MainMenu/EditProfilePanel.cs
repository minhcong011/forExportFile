using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EditProfilePanel : SingletonBase<EditProfilePanel>
{
    [SerializeField] private ChooseAvatarButton[] chooseAvatarButtons;

    [SerializeField] private Sprite[] avatarIcons;

    [SerializeField] private TMP_InputField nameInput;
    private void Start()
    {
        nameInput.text = GameCache.GC.userName;
        SetChooseAvatarButton();
    }
    private void SetChooseAvatarButton()
    {
        for(int i = 0; i < chooseAvatarButtons.Length; i++)
        {
            chooseAvatarButtons[i].Set(i, avatarIcons[i]);
        }
    }
    public void DeSellectButton(int id)
    {
        chooseAvatarButtons[id].SetSellect(false);
    }
    public void ChangeName()
    {
        GameCache.GC.userName = nameInput.text;
    }
}
