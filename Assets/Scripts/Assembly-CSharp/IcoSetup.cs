using UnityEngine;

// Token: 0x0200007C RID: 124
public class IcoSetup : MonoBehaviour
{
	// Token: 0x06000221 RID: 545 RVA: 0x0000F521 File Offset: 0x0000D721
	public void ShowSetup()
	{
		GameObject gameObject = Object.Instantiate(Resources.Load("UI/PanelSetup")) as GameObject;
		gameObject.name = "PanelSetup";
		gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
	}
}
