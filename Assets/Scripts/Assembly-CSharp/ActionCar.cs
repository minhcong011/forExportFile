using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000031 RID: 49
public class ActionCar : MonoBehaviour
{
	// Token: 0x060000CE RID: 206 RVA: 0x000074D4 File Offset: 0x000056D4
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
		this.NormalCar1_FrontLeftWheel = GameObject.Find("NormalCar1_FrontLeftWheel");
		this.NormalCar1_FrontRightWheel = GameObject.Find("NormalCar1_FrontRightWheel");
		this.NormalCar1_FrontLeftWheel.SetActive(false);
		this.NormalCar1_FrontRightWheel.SetActive(false);
		VariblesGlobal.carKoleso = 0;
		VariblesGlobal.carBenzin = 0;
	}

	// Token: 0x060000CF RID: 207 RVA: 0x00007535 File Offset: 0x00005735
	private void OnMouseDown()
	{
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 5f)
		{
			this.clickTime = Time.time;
		}
	}

	// Token: 0x060000D0 RID: 208 RVA: 0x0000756C File Offset: 0x0000576C
	private void OnMouseUp()
	{
		if (Time.time - this.clickTime <= 0.3f && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 5f)
		{
			this.TakeObject();
		}
	}

	// Token: 0x060000D1 RID: 209 RVA: 0x000075BC File Offset: 0x000057BC
	private void TakeObject()
	{
		if (VariblesGlobal.SelectObject == "wheel1")
		{
			this.NormalCar1_FrontLeftWheel.SetActive(true);
			VariblesGlobal.carKoleso++;
			VariblesGlobal.SelectObject = "";
			this.SoundComplete();
		}
		if (VariblesGlobal.SelectObject == "wheel2")
		{
			this.NormalCar1_FrontRightWheel.SetActive(true);
			VariblesGlobal.carKoleso++;
			VariblesGlobal.SelectObject = "";
			this.SoundComplete();
		}
		if (VariblesGlobal.SelectObject == "jerrycan")
		{
			VariblesGlobal.carBenzin++;
			VariblesGlobal.SelectObject = "";
			this.SoundComplete();
		}
		if (VariblesGlobal.carKoleso < 2)
		{
			if (GameObject.Find("infobox") == null)
			{
				VariblesGlobal.infoboxText = VariblesGlobalText.Text5;
				GameObject gameObject = Object.Instantiate(Resources.Load("UI/infobox")) as GameObject;
				gameObject.name = "infobox";
				gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
				return;
			}
		}
		else if (VariblesGlobal.carBenzin == 0)
		{
			if (GameObject.Find("infobox") == null)
			{
				VariblesGlobal.infoboxText = VariblesGlobalText.Text6;
				GameObject gameObject2 = Object.Instantiate(Resources.Load("UI/infobox")) as GameObject;
				gameObject2.name = "infobox";
				gameObject2.transform.SetParent(GameObject.Find("Canvas").transform, false);
				return;
			}
		}
		else
		{
			if (VariblesGlobal.SelectObject == "keycar")
			{
				if (VariblesGlobal.Level <= 1)
				{
					VariblesGlobal.Level = 1;
					SaveLoadG.SaveData();
				}
				GameObject gameObject3 = Object.Instantiate(Resources.Load("UI/PanelLoad")) as GameObject;
				gameObject3.name = "PanelLoad";
				gameObject3.transform.SetParent(GameObject.Find("Canvas").transform, false);
				this.SoundComplete();
				VariblesGlobal.Achievement1 = 1;
				SaveLoadG.SaveData();
				SceneManager.LoadScene("final");
				return;
			}
			if (GameObject.Find("infobox") == null)
			{
				VariblesGlobal.infoboxText = VariblesGlobalText.Text7;
				GameObject gameObject4 = Object.Instantiate(Resources.Load("UI/infobox")) as GameObject;
				gameObject4.name = "infobox";
				gameObject4.transform.SetParent(GameObject.Find("Canvas").transform, false);
			}
		}
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x00003EF3 File Offset: 0x000020F3
	private void SoundComplete()
	{
		Object.Instantiate(Resources.Load("Sound/SoundComplete"));
	}

	// Token: 0x0400013F RID: 319
	private GameObject Player;

	// Token: 0x04000140 RID: 320
	private GameObject NormalCar1_FrontLeftWheel;

	// Token: 0x04000141 RID: 321
	private GameObject NormalCar1_FrontRightWheel;

	// Token: 0x04000142 RID: 322
	private float clickTime;
}
