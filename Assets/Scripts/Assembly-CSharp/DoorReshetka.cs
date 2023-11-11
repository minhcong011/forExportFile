// ILSpyBased#2
using UnityEngine;

public class DoorReshetka : MonoBehaviour
{
    private GameObject Player;

    public bool Open;

    public float Angle;

    private float AngleX;

    private float clickTime;

    private void Start()
    {
        this.Player = GameObject.Find("FPSController");
        this.AngleX = base.transform.eulerAngles.x;
    }

    private void OnMouseDown()
    {
        if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 4f)
        {
            if (VariblesGlobal.SelectObject == "Crowbar")
            {
                this.clickTime = Time.time;
            }
            else if ((Object)GameObject.Find("infobox") == (Object)null)
            {
                VariblesGlobal.infoboxText = VariblesGlobalText.Text20;
                GameObject obj = UnityEngine.Object.Instantiate(Resources.Load("UI/infobox")) as GameObject;
                obj.name = "infobox";
                obj.transform.SetParent(GameObject.Find("Canvas").transform, false);
            }
        }
    }

    private void OnMouseUp()
    {
        if (Time.time - this.clickTime <= 0.3f && VariblesGlobal.SelectObject == "Crowbar" && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 4f)
        {
            this.OpenClose();
        }
    }

    private void OpenClose()
    {
        this.Open = !this.Open;
        if (this.Open)
        {
            base.transform.eulerAngles = new Vector3(this.AngleX + this.Angle, base.transform.eulerAngles.y, base.transform.eulerAngles.z);
            base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y - 0.62f, base.transform.position.z);
            base.GetComponent<BoxCollider>().enabled = false;
        }
        (UnityEngine.Object.Instantiate(Resources.Load("Sound/OpenMetal")) as GameObject).transform.position = base.gameObject.transform.position;
    }
}


