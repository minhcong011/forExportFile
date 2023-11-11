using UnityEngine;

// Token: 0x02000064 RID: 100
public class ShowMe : MonoBehaviour
{
	// Token: 0x060001B9 RID: 441 RVA: 0x0000B0DC File Offset: 0x000092DC
	public void ShowMePls()
	{
		VariblesGlobal.PlayerHide = 0;
		this.Cam.enabled = false;
		Object.Destroy(base.gameObject);
	}

	// Token: 0x040001C5 RID: 453
	public Camera Cam;
}
