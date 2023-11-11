using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000081 RID: 129
public class PanelSafeZone : MonoBehaviour
{
	// Token: 0x0600022F RID: 559 RVA: 0x0000F84C File Offset: 0x0000DA4C
	private void Start()
	{
		SceneManager.GetActiveScene();
		GameObject.Find("TextRateSafe").GetComponent<Text>().text = VariblesGlobalText.Text34;
		GameObject.Find("TextCloseBtn").GetComponent<Text>().text = VariblesGlobalText.Text32;
		GameObject.Find("TextRateSafe2").GetComponent<Text>().text = VariblesGlobalText.Text35;
		GameObject.Find("TextRateZone68").GetComponent<Text>().text = VariblesGlobalText.Text33;
        //Advertisements.Instance.HideBanner();
    }

	// Token: 0x06000230 RID: 560 RVA: 0x000040E0 File Offset: 0x000022E0
	public void RateIt()
	{
		Application.OpenURL("market://details?id=" + Application.identifier);
	}

	// Token: 0x06000231 RID: 561 RVA: 0x0000F8C3 File Offset: 0x0000DAC3
	public void CloseRate()
	{
		Object.Destroy(GameObject.Find("PanelSafeZone"));
	}
}
