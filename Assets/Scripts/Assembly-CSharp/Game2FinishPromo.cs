using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000017 RID: 23
public class Game2FinishPromo : MonoBehaviour
{
	// Token: 0x06000051 RID: 81 RVA: 0x00003F08 File Offset: 0x00002108
	private void Start()
	{
		GameObject.Find("TextRate").GetComponent<Text>().text = VariblesGlobalText.Text30;
		GameObject.Find("TextMain").GetComponent<Text>().text = VariblesGlobalText.Text31;
		GameObject.Find("FinishText").GetComponent<Text>().text = VariblesGlobalText.Text29;
		this.State = 0;
		this.Panel1.SetActive(false);
		this.Panel2.SetActive(false);
	}

	// Token: 0x06000052 RID: 82 RVA: 0x00003F80 File Offset: 0x00002180
	private void Update()
	{
		this.Timer1 -= Time.deltaTime;
		if (this.Timer1 < 0f && this.State == 0)
		{
			this.State = 1;
			this.Panel1.SetActive(true);
		}
		if (this.State == 1)
		{
			if (GameObject.Find("Finish1") != null)
			{
				GameObject.Find("Finish1").GetComponent<AudioSource>().volume = GameObject.Find("Finish1").GetComponent<AudioSource>().volume - Time.deltaTime;
			}
			if (GameObject.Find("Finish2") != null)
			{
				GameObject.Find("Finish2").GetComponent<AudioSource>().volume = GameObject.Find("Finish2").GetComponent<AudioSource>().volume - Time.deltaTime;
			}
		}
		this.Timer2 -= Time.deltaTime;
		if (this.Timer2 < 0f && this.State == 1)
		{
			this.State = 2;
			this.Panel2.SetActive(true);
		}
	}

	// Token: 0x06000053 RID: 83 RVA: 0x00004090 File Offset: 0x00002290
	public void ButtonMainMenu()
	{
		GameObject gameObject = Object.Instantiate(Resources.Load("UI/PanelLoad")) as GameObject;
		gameObject.name = "PanelLoad";
		gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
		SceneManager.LoadScene("main");
	}

	// Token: 0x06000054 RID: 84 RVA: 0x000040E0 File Offset: 0x000022E0
	public void RateGame()
	{
		Application.OpenURL("market://details?id=" + Application.identifier);
	}

	// Token: 0x0400008F RID: 143
	public GameObject Panel1;

	// Token: 0x04000090 RID: 144
	public GameObject Panel2;

	// Token: 0x04000091 RID: 145
	private float Timer1 = 18f;

	// Token: 0x04000092 RID: 146
	private float Timer2 = 28f;

	// Token: 0x04000093 RID: 147
	private int State;
}
