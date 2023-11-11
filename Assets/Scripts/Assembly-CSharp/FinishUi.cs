using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000086 RID: 134
public class FinishUi : MonoBehaviour
{
    // Token: 0x06000264 RID: 612 RVA: 0x00010260 File Offset: 0x0000E460
    private void Start()
    {
       // Advertisements.Instance.HideBanner();
    }
    public void ButtonMainMenu()
	{
		GameObject gameObject = Object.Instantiate(Resources.Load("UI/PanelLoad")) as GameObject;
		gameObject.name = "PanelLoad";
		gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
		SceneManager.LoadScene("main");
	}

	// Token: 0x06000265 RID: 613 RVA: 0x000040E0 File Offset: 0x000022E0
	public void RateGame()
	{
		Application.OpenURL("market://details?id=" + Application.identifier);
	}
}
