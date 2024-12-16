using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyEggButton : MonoBehaviour
{
    [SerializeField] private Image eggIcon;
    [SerializeField] private Image rewardIconImage;
    [SerializeField] private Sprite[] rewardIconSprite;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject breakEf;

    public void BreakEgg()
    {
        StartCoroutine(BreakEggCoroutinue());
    }
    IEnumerator BreakEggCoroutinue()
    {
        if (DailyEggReward.Instance.FinishMatch3) yield break;
        int rand = Random.Range(0, 4);
        animator.Play("EggBreak");
        breakEf.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        eggIcon.enabled = false;
        rewardIconImage.gameObject.SetActive(true);
        rewardIconImage.sprite = rewardIconSprite[rand];
        DailyEggReward.Instance.AddRewardToCheck(rand, transform);
    }
}
