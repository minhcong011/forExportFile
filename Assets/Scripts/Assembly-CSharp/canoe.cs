using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000034 RID: 52
public class canoe : MonoBehaviour
{
	// Token: 0x060000DE RID: 222 RVA: 0x00007AC4 File Offset: 0x00005CC4
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
	}

	// Token: 0x060000DF RID: 223 RVA: 0x00007AD8 File Offset: 0x00005CD8
	private void OnMouseDown()
	{
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 4f)
		{
			if (VariblesGlobal.SelectObject == "canoePaddle")
			{
				this.clickTime = Time.time;
				return;
			}
			if (GameObject.Find("infobox") == null)
			{
				VariblesGlobal.infoboxText = VariblesGlobalText.Text46;
				GameObject gameObject = Object.Instantiate(Resources.Load("UI/infobox")) as GameObject;
				gameObject.name = "infobox";
				gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
			}
		}
	}

	// Token: 0x060000E0 RID: 224 RVA: 0x00007B80 File Offset: 0x00005D80
	private void OnMouseUp()
	{
		if (Time.time - this.clickTime <= 0.3f && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 4f)
		{
			this.TakeObject();
		}
	}

	// Token: 0x060000E1 RID: 225 RVA: 0x00007BD0 File Offset: 0x00005DD0
	private void TakeObject()
	{
		VariblesGlobal.Achievement3 = 1;
		if (VariblesGlobal.Level <= 2)
		{
			VariblesGlobal.Level = 2;
		}
		SaveLoadG.SaveData();
		VariblesGlobal.game2_Final = 0;
		GameObject gameObject = Object.Instantiate(Resources.Load("UI/PanelLoad")) as GameObject;
		gameObject.name = "PanelLoad";
		gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
		this.SoundComplete();
		SceneManager.LoadScene("final2");
	}

	// Token: 0x060000E2 RID: 226 RVA: 0x00003EF3 File Offset: 0x000020F3
	private void SoundComplete()
	{
		Object.Instantiate(Resources.Load("Sound/SoundComplete"));
	}

	// Token: 0x0400014A RID: 330
	private GameObject Player;

	// Token: 0x0400014B RID: 331
	private GameObject set_ladderLong;

	// Token: 0x0400014C RID: 332
	private float clickTime;
}
