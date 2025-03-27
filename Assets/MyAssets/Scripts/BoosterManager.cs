using System.Collections;
using UnityEngine;

public class BoosterManager : SingletonBase<BoosterManager>
{
    [SerializeField] private int amountTimeAdd;
    [SerializeField] private BoosterButton[] boosterButton;
    [SerializeField] private GameObject hammerPref;
    [SerializeField] private GameObject breakEf;
    [SerializeField] private GameObject canvas;
    [SerializeField] private Vector3 offsetHammer;
    public bool useBreakBooster;

    public void UpdateBoosterBtAmountText()
    {
        foreach(BoosterButton bt in boosterButton)
        {
            bt.UpdateBoosterAmountText();
        }
    }
    public void UseBooster(BoosterType boosterType)
    {
        Debug.Log("Use booster" + boosterType);
        switch (boosterType)
        {
            case BoosterType.AddTime:
                {
                    GameManager.Instance.AddTime(amountTimeAdd);
                    break;
                }
            case BoosterType.BreakMoveable:
                {
                    useBreakBooster = true;
                    break;
                }
            case BoosterType.Find:
                {
                    LevelLoader.Instance.ShowFindBooster();
                    break;
                }
            case BoosterType.Revice:
                {
                    GameManager.Instance.Revice();
                    break;
                }
        }
    }
    public void StartBreakMoveable(GameObject moveable)
    {
        StartCoroutine(BreakMovable(moveable));
    }
    private IEnumerator BreakMovable(GameObject moveableToBreak)
    {
        GameObject newHammer = Instantiate(hammerPref);
        newHammer.transform.SetParent(canvas.transform, false);
        newHammer.transform.position = Camera.main.WorldToScreenPoint(moveableToBreak.transform.position + offsetHammer);
        yield return new WaitForSeconds(1);
        ParticleSystem newBreakEf = Instantiate(breakEf, moveableToBreak.transform.position + new Vector3(0,0.1f,0), Quaternion.identity).GetComponent<ParticleSystem>();

        Color colorToSet = moveableToBreak.GetComponent<MovableItem>().color;
        if (colorToSet == Color.red) colorToSet = new Color(0.5647059f, 0.0509804f, 1, 1);
        if (colorToSet == Color.blue) colorToSet = new Color(0.1921569f, 0.8588236f, 0.937255f, 1);
        newBreakEf.startColor = colorToSet;

        Key key = moveableToBreak.GetComponentInChildren<Key>();

        if (key)
        {
            key.transform.parent = null;
            key.GotoLock();
        }

        if (moveableToBreak.GetComponent<MovableItem>().isLocked)
        {
            LevelLoader.Instance.RemoveAllKey();
        }

        Destroy(moveableToBreak);
        GameManager.Instance.CheckWin();
    }
    public void AddItemBooster(BoosterType boosterType, int amountToAdd)
    {
        switch (boosterType)
        {
            case BoosterType.AddTime:
                {
                    GameCache.GC.amountAddTimeBooster += amountToAdd;
                    break;
                }
            case BoosterType.BreakMoveable:
                {
                    GameCache.GC.amountBreakBooster += amountToAdd;
                    break;
                }
            case BoosterType.Find:
                {
                    GameCache.GC.amountFindBooster += amountToAdd;
                    break;
                }
        }
    }
}
