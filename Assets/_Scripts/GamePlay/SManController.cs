using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class SManController : MonoBehaviour
{
    [SerializeField] private MeshRenderer baseColor;
    [SerializeField] private GameObject emojiPref;
    [SerializeField] private Vector3 emojiOffset;
    private bool isRun;
    public int colorID;

    private int colorIDBase;
    public int ColorID { get { return colorID; } set { colorID = value; } }
    private float speedMove = 10f;
    private int step;
    public int Step { get { return step; } set { step = value; } }

    private bool isCanPlayIdleAni = true;

    public CarController carSpawn;
    public IEnumerator MoveToPos(Vector3 newPos)
    {
        if (!isRun)
        {
            isRun = true;
        }
        while (Vector3.Distance(transform.position, newPos) > 0.01f)
        {
            transform.LookAt(newPos);
            transform.position = Vector3.MoveTowards(transform.position, newPos, speedMove * Time.deltaTime);
            yield return null;
        }
        isRun = false;
        transform.position = newPos;
    }
    public void SetColor(int colorID)
    {
        this.colorID = colorID;
        colorIDBase = colorID;
        baseColor.material.color = ColorManager.Instance.carColorBase[colorID].color;
    }
    public void RandomColor()
    {
        try
        {
            Dictionary<int, Material> colorBase = ColorManager.Instance.carColorBase;
            List<int> colorID = new(colorBase.Keys);
            int rand = Random.Range(0, colorID.Count);
            baseColor.material.color = colorBase[colorID[rand]].color;
        }
        catch { }
    }
    public void ReturnColorIfError()
    {
        Dictionary<int, Material> colorBase = ColorManager.Instance.carColorBase;
        baseColor.material.color = colorBase[colorIDBase].color;
        colorID = colorIDBase;
    }
    public void PlayJoinCarAnimation(Transform seatsTranform)
    {
        StartCoroutine(PlayJoinCarAnimationCoroutinue());
        IEnumerator PlayJoinCarAnimationCoroutinue()
        {
            while (Vector3.Distance(transform.position, seatsTranform.position) > 3f)
            {
                yield return null;
            }
            isCanPlayIdleAni = false;
            yield return new WaitForSeconds(0.3f);
        }
    }

    public void SpawnEmojiOnHead()
    {
        //GameObject newEmoji = ObjectPooling.Instance.CreateUIObject(emojiPref);
        //newEmoji.transform.position = Camera.main.WorldToScreenPoint(transform.position + emojiOffset);
    }
}
