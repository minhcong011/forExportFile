using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnglessTresurePanel : SingletonBase<EnglessTresurePanel>
{
    [SerializeField] private EndlessTresureItem[] endlessTresureItems;
    [SerializeField] private EndlessTresureItem currentItemLevel;

    private void Start()
    {
        SetItems();
    }
    private void SetItems()
    {
        int endlessLevel = GameCache.GC.endlessTreasureLevel;
        for (int i = 0; i < endlessLevel; i++)
        {
            endlessTresureItems[i].gameObject.SetActive(false);
        }
        SetCurrentItemLevel();
    }
    public void DeleteCurrentItemLevel()
    {
        currentItemLevel.gameObject.SetActive(false);
        GameCache.GC.endlessTreasureLevel++;
        SetCurrentItemLevel();
    }
    private void SetCurrentItemLevel()
    {
        if (GameCache.GC.endlessTreasureLevel >= endlessTresureItems.Length) return;
        currentItemLevel = endlessTresureItems[GameCache.GC.endlessTreasureLevel];
        currentItemLevel.Unlock();
    }
}
