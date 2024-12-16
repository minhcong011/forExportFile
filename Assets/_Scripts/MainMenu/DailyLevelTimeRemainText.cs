using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DailyLevelTimeRemainText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI remainText;

    private void Start()
    {
        StartCoroutine(SetTimeRemainText());
    }
    IEnumerator SetTimeRemainText()
    {
        yield return AmericanTimeResetDB.GetTimeRemainString(currentAmericanTime =>
        {
            remainText.text = currentAmericanTime;
        });
    }
}
