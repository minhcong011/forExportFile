using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SManEmoji : MonoBehaviour
{
    [SerializeField] private Sprite[] emojiSprites;
    [SerializeField] private Image emojiImage;
    [SerializeField] private float speedChangeScale;
    [SerializeField] private float timeLive;
    private void OnEnable()
    {
        SetRandEmojiImage();
        StartCoroutine(CalculateTimeToDisable());
    }
    private IEnumerator CalculateTimeToDisable()
    {
        StartCoroutine(ChangeScale(new (0,0,0), new(1,1,1)));
        yield return new WaitForSeconds(timeLive);
        yield return ChangeScale(new(1, 1, 1), new(0, 0, 0));
    }
    private void SetRandEmojiImage()
    {
        emojiImage.sprite = emojiSprites[Random.Range(0, emojiSprites.Length)];
    }
    private IEnumerator ChangeScale(Vector3 startScale ,Vector3 targetScale)
    {
        transform.localScale = startScale;
        while(Vector3.Distance(transform.localScale, targetScale) > 0.1f)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, speedChangeScale * Time.deltaTime);

            yield return null;
        }
    }
}
