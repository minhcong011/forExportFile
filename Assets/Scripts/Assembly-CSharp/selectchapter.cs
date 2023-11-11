using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000088 RID: 136
public class selectchapter : MonoBehaviour
{
	// Token: 0x06000269 RID: 617 RVA: 0x000102BC File Offset: 0x0000E4BC
	public void Chapter1()
	{
		this.SetLoading();
		this.NameChapter = "game";
	}

	// Token: 0x0600026A RID: 618 RVA: 0x000102CF File Offset: 0x0000E4CF
	public void Chapter2()
	{
		this.SetLoading();
		this.NameChapter = "game2";
	}

	// Token: 0x0600026B RID: 619 RVA: 0x000102E2 File Offset: 0x0000E4E2
	public void Chapter3()
	{
		this.SetLoading();
		this.NameChapter = "game3";
	}

	// Token: 0x0600026C RID: 620 RVA: 0x000102F8 File Offset: 0x0000E4F8
	private void Update()
	{
		if (this.NameChapter != "")
		{
			this.state = 1;
			this.timerLoad += Time.deltaTime;
		}
		if (this.timerLoad > 1f && this.state == 1)
		{
			this.state = 2;
			SceneManager.LoadScene(this.NameChapter);
		}
	}

	// Token: 0x0600026D RID: 621 RVA: 0x00010358 File Offset: 0x0000E558
	private void SetLoading()
	{
		GameObject gameObject = Object.Instantiate(Resources.Load("UI/PanelLoad")) as GameObject;
		gameObject.name = "PanelLoad";
		gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
	}

	// Token: 0x04000267 RID: 615
	private string NameChapter = "";

	// Token: 0x04000268 RID: 616
	private int state;

	// Token: 0x04000269 RID: 617
	private float timerLoad;
}
