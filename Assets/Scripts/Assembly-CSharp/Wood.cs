// ILSpyBased#2
using UnityEngine;

public class Wood : MonoBehaviour
{
    private GameObject Player;

    private float clickTime;

    private GameObject gm_wood;

    private void Start()
    {
        this.Player = GameObject.Find("FPSController");
        this.gm_wood = GameObject.Find("gm_wood");
        this.gm_wood.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 8f)
        {
            if (VariblesGlobal.SelectObject == "Saw")
            {
                this.clickTime = Time.time;
            }
            else if ((Object)GameObject.Find("infobox") == (Object)null)
            {
                VariblesGlobal.infoboxText = VariblesGlobalText.Text8;
                GameObject obj = UnityEngine.Object.Instantiate(Resources.Load("UI/infobox")) as GameObject;
                obj.name = "infobox";
                obj.transform.SetParent(GameObject.Find("Canvas").transform, false);
            }
        }
    }

    private void OnMouseUp()
    {
        if (Time.time - this.clickTime <= 0.3f && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 8f)
        {
            this.TakeObject();
        }
    }

    private void TakeObject()
    {
        this.gm_wood.SetActive(true);
        UnityEngine.Object.DestroyImmediate(base.gameObject);
    }
}


