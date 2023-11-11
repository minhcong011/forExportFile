using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000083 RID: 131
public class pregame : MonoBehaviour
{
	// Token: 0x06000239 RID: 569 RVA: 0x0000FAC4 File Offset: 0x0000DCC4
	private void Start()
	{
		//Debug.Log("pregame0");
		//VariblesGlobal.ShowADinter = true;


		this.ClickStart = false;
		VariblesGlobal.GameMode = 1;
		GameObject.Find("TextGhost").GetComponent<Text>().text = VariblesGlobalText.Text37;
		GameObject.Find("TextEasy").GetComponent<Text>().text = VariblesGlobalText.Text55;
		GameObject.Find("TextClassic").GetComponent<Text>().text = VariblesGlobalText.Text38;
		GameObject.Find("TextHard").GetComponent<Text>().text = VariblesGlobalText.Text39;
		GameObject.Find("TextStart").GetComponent<Text>().text = VariblesGlobalText.Text25;
		GameObject.Find("TextDesc").GetComponent<Text>().text = VariblesGlobalText.Text41;
		GameObject.Find("TextTitle").GetComponent<Text>().text = VariblesGlobalText.Text43;
	}

	// Token: 0x0600023A RID: 570 RVA: 0x0000FB8D File Offset: 0x0000DD8D
	public void SelectGhost()
	{
		this.AllUnCheck("Ghost");
		VariblesGlobal.GameMode = 0;
		GameObject.Find("TextDesc").GetComponent<Text>().text = VariblesGlobalText.Text40;
	}

	// Token: 0x0600023B RID: 571 RVA: 0x0000FBB9 File Offset: 0x0000DDB9
	public void SelectEasy()
	{
		this.AllUnCheck("Easy");
		VariblesGlobal.GameMode = 3;
		GameObject.Find("TextDesc").GetComponent<Text>().text = VariblesGlobalText.Text56;
	}

	// Token: 0x0600023C RID: 572 RVA: 0x0000FBE5 File Offset: 0x0000DDE5
	public void SelectClassic()
	{
		this.AllUnCheck("Classic");
		VariblesGlobal.GameMode = 1;
		GameObject.Find("TextDesc").GetComponent<Text>().text = VariblesGlobalText.Text41;
	}

	// Token: 0x0600023D RID: 573 RVA: 0x0000FC11 File Offset: 0x0000DE11
	public void SelectHard()
	{
		this.AllUnCheck("Hard");
		VariblesGlobal.GameMode = 2;
		GameObject.Find("TextDesc").GetComponent<Text>().text = VariblesGlobalText.Text42;
	}

	// Token: 0x0600023E RID: 574 RVA: 0x0000FC40 File Offset: 0x0000DE40
	private void AllUnCheck(string CheckName)
	{
		GameObject.Find("Ghost").GetComponent<Image>().sprite = this.ImgUnCheck;
		GameObject.Find("Easy").GetComponent<Image>().sprite = this.ImgUnCheck;
		GameObject.Find("Classic").GetComponent<Image>().sprite = this.ImgUnCheck;
		GameObject.Find("Hard").GetComponent<Image>().sprite = this.ImgUnCheck;
		GameObject.Find(CheckName).GetComponent<Image>().sprite = this.ImgCheck;
	}

	// Token: 0x0600023F RID: 575 RVA: 0x0000FCCB File Offset: 0x0000DECB
	private void Update()
	{
		if (this.ClickStart)
		{
			Debug.Log("pregame");
			VariblesGlobal.LoadScene = "chapter";
			//Debug.Log(4);
			VariblesGlobal.ShowADinter = true;
			SceneManager.LoadScene("chapters");

			//VariblesGlobal.ShowADinter = true;
			//adcontroller.Instance.RequestInterstitial();
		}
	}

	// Token: 0x06000240 RID: 576 RVA: 0x0000FCF0 File Offset: 0x0000DEF0
	public void StartGame()
	{
		//Debug.Log("StartGame");
		VariblesGlobal.ShowADinter = true;

		//adcontroller.Instance.RequestInterstitial();

		GameObject gameObject = Object.Instantiate(Resources.Load("UI/PanelLoad")) as GameObject;
		gameObject.name = "PanelLoad";
		gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
		this.ClickStart = true;
	}

	// Token: 0x04000258 RID: 600
	public Sprite ImgCheck;

	// Token: 0x04000259 RID: 601
	public Sprite ImgUnCheck;

	// Token: 0x0400025A RID: 602
	private bool ClickStart;
}
