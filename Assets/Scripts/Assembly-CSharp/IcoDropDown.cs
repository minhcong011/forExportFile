using UnityEngine;

// Token: 0x0200007A RID: 122
public class IcoDropDown : MonoBehaviour
{
	// Token: 0x0600021A RID: 538 RVA: 0x0000F4B8 File Offset: 0x0000D6B8
	public void ClickIco()
	{
		VariblesGlobal.SelectObject = "";
		VariblesGlobal.ActionDropDown = 1;
		Object.Instantiate(Resources.Load("Sound/SoundPut"));
	}
}
