// ILSpyBased#2
using System;
using UnityEngine;

public class CanvasBlack : MonoBehaviour
{
    public int Parametr;

    private float Speed = 16f;

    private float PositionTop;

    private float PositionBottom;

    private bool StateChapter;

    private float TimeChapter = 10f;

    private void Start()
    {
        this.Speed = (float)(Screen.height / 30);
    }

    private void Update()
    {
        this.TimeChapter -= Time.deltaTime;
        if (this.TimeChapter <= 0f && !this.StateChapter)
        {
            this.StateChapter = true;
            this.StateChapter = true;
            GameObject obj = UnityEngine.Object.Instantiate(Resources.Load("UI/Chapter1")) as GameObject;
            obj.name = "Chapter1";
            obj.transform.SetParent(GameObject.Find("CanvasBlack").transform, false);
        }
        if (Math.Abs(base.transform.position.y) > 4f)
        {
            if (this.Parametr == 1)
            {
                base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y - this.Speed * Time.deltaTime, base.transform.position.z);
            }
            if (this.Parametr == 2)
            {
                base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + this.Speed * Time.deltaTime, base.transform.position.z);
            }
        }
        else if (!this.StateChapter)
        {
            this.StateChapter = true;
            GameObject obj2 = UnityEngine.Object.Instantiate(Resources.Load("UI/Chapter1")) as GameObject;
            obj2.name = "Chapter1";
            obj2.transform.SetParent(GameObject.Find("CanvasBlack").transform, false);
        }
    }
}


