using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenME : MonoBehaviour
{

    public RectTransform canvas;
    // Start is called before the first frame update
    void Awake()
    {

        RectTransform rect = this.GetComponent<RectTransform>();
        rect.sizeDelta = canvas.sizeDelta;
    }


}
