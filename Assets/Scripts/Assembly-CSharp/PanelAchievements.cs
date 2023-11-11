using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200007F RID: 127
public class PanelAchievements : MonoBehaviour
{
	// Token: 0x06000229 RID: 553 RVA: 0x0000F70C File Offset: 0x0000D90C
	private void Start()
	{
		GameObject.Find("TextAchive1").GetComponent<Text>().text = VariblesGlobalText.Text58;
		GameObject.Find("TextAchive2").GetComponent<Text>().text = VariblesGlobalText.Text60;
		GameObject.Find("TextAchive3").GetComponent<Text>().text = VariblesGlobalText.Text59;
		GameObject.Find("TextAchive4").GetComponent<Text>().text = VariblesGlobalText.Text61;
		if (VariblesGlobal.Achievement1 == 0)
		{
			Object.Destroy(GameObject.Find("Acheck1"));
		}
		if (VariblesGlobal.Achievement2 == 0)
		{
			Object.Destroy(GameObject.Find("Acheck2"));
		}
		if (VariblesGlobal.Achievement3 == 0)
		{
			Object.Destroy(GameObject.Find("Acheck3"));
		}
		if (VariblesGlobal.Achievement4 == 0)
		{
			Object.Destroy(GameObject.Find("Acheck4"));
		}
	}
}
