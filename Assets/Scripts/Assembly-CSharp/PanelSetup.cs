using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000082 RID: 130
public class PanelSetup : MonoBehaviour
{
	// Token: 0x06000233 RID: 563 RVA: 0x0000F8D4 File Offset: 0x0000DAD4
	private void Start()
	{
		this.sceneName = SceneManager.GetActiveScene();
		this.SensText = GameObject.Find("TextSensitivity");
		this.SensTitle = GameObject.Find("TextTitleSens");
		this.SensText.GetComponent<Text>().text = string.Concat(VariblesGlobal.Sensitivity);
		this.SensTitle.GetComponent<Text>().text = (VariblesGlobalText.Text24 ?? "");
		GameObject.Find("MainMenuText").GetComponent<Text>().text = (VariblesGlobalText.Text31 ?? "");
	}

	// Token: 0x06000234 RID: 564 RVA: 0x0000F96B File Offset: 0x0000DB6B
	public void CloseSetup()
	{
		UnityEngine.Object.Destroy(GameObject.Find("PanelSetup"));
	}

	// Token: 0x06000235 RID: 565 RVA: 0x0000F97C File Offset: 0x0000DB7C
	public void SensPlus()
	{
		VariblesGlobal.Sensitivity += 0.05f;
		if (VariblesGlobal.Sensitivity > 2f)
		{
			VariblesGlobal.Sensitivity = 3f;
		}
		VariblesGlobal.Sensitivity = (float)Math.Round((double)VariblesGlobal.Sensitivity, 2);
		this.SensText.GetComponent<Text>().text = string.Concat(VariblesGlobal.Sensitivity);
		SaveLoadG.SaveData();
	}

	// Token: 0x06000236 RID: 566 RVA: 0x0000F9E8 File Offset: 0x0000DBE8
	public void SensMinus()
	{
		VariblesGlobal.Sensitivity -= 0.05f;
		if (VariblesGlobal.Sensitivity <= 0f)
		{
			VariblesGlobal.Sensitivity = 0.05f;
		}
		VariblesGlobal.Sensitivity = (float)Math.Round((double)VariblesGlobal.Sensitivity, 2);
		this.SensText.GetComponent<Text>().text = string.Concat(VariblesGlobal.Sensitivity);
		SaveLoadG.SaveData();
	}

	// Token: 0x06000237 RID: 567 RVA: 0x0000FA54 File Offset: 0x0000DC54
	public void ButtonMainMenu()
	{
		if (this.sceneName.name == "main")
		{
			this.CloseSetup();
			return;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("UI/PanelLoad")) as GameObject;
		gameObject.name = "PanelLoad";
		gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
		SceneManager.LoadScene("main");
	}

	// Token: 0x04000255 RID: 597
	private GameObject SensText;

	// Token: 0x04000256 RID: 598
	private GameObject SensTitle;

	// Token: 0x04000257 RID: 599
	private Scene sceneName;
}
