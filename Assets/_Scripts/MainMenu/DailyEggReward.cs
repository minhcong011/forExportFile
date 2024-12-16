using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DailyEggReward : SingletonBase<DailyEggReward>
{
    //0 coin
    //1 shuffle
    //2 sort
    //3 vip
    [SerializeField] private Transform center;
    [SerializeField] private float speedMoveReward;
    [SerializeField] private float speedUpScale;
    [SerializeField] private Transform clampGp;
    [SerializeField] private DailyEggBreakClampPanel clampPanel; 
    private Dictionary<int, int> amountRewardPerType = new();
    private Dictionary<int, List<Transform>> eggTranformDict = new();

    private bool finishMatch3;
    public bool FinishMatch3 { get { return finishMatch3; } }
    public void AddRewardToCheck(int rewardID, Transform eggTranform)
    {
        if (!amountRewardPerType.ContainsKey(rewardID))
        {
            amountRewardPerType.Add(rewardID, 0);
            eggTranformDict.Add(rewardID, new());
        }
        amountRewardPerType[rewardID]++;
        eggTranformDict[rewardID].Add(eggTranform);
        if (amountRewardPerType[rewardID] == 3)
        {
            StartCoroutine(ClampReward(rewardID));
        }
    }
    public IEnumerator ClampReward(int rewardID)
    {
        finishMatch3 = true;
        List<Coroutine> moveRewardCoroutinues = new ();
        foreach(Transform eggTranform in eggTranformDict[rewardID])
        {
            moveRewardCoroutinues.Add(StartCoroutine(MoveRewardToCenter(eggTranform)));
        }
        foreach (Coroutine coroutine in moveRewardCoroutinues)
        {
            yield return coroutine;
        }
        foreach(KeyValuePair<int, List<Transform>> kvp in eggTranformDict)
        {
            foreach(Transform gift in kvp.Value)
            {
                gift.gameObject.SetActive(false);
            }
        }
        clampPanel.gameObject.SetActive(true);
        clampPanel.SetGiftIcon(rewardID);
        switch (rewardID)
        {
            case 0:
                {
                    GameCache.GC.coin += 1000;
                    break;
                }
            case 1:
                {
                    GameCache.GC.shuffleBoosterAmount++;
                    break;
                }
            case 2:
                {
                    GameCache.GC.sortBoosterAmount++;
                    break;
                }
            case 3:
                {
                    GameCache.GC.vipSlotBoosterAmount++;
                    break;
                }
        }
        GameCache.GC.canBreakEggDaily = false;
    }
    IEnumerator MoveRewardToCenter(Transform rewardTranform)
    {
        Vector3 startPosition = rewardTranform.position; 
        float elapsedTime = 0;

        while (elapsedTime < speedMoveReward)
        {
            rewardTranform.localScale += new Vector3(speedUpScale, speedUpScale, speedUpScale) * Time.deltaTime;
            rewardTranform.position = Vector3.Lerp(startPosition, center.position, (elapsedTime / speedMoveReward));
            elapsedTime += Time.deltaTime;
            yield return null; 
        }
        rewardTranform.position = center.position;
    }
}
